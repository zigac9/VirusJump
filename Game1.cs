using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using VirusJump.Classes.Scene.Objects;
using System.Collections.Generic;
using System;
using System.Threading;
using VirusJump.Classes.Scene.Objects.Supplements;
using VirusJump.Classes.Graphics;
using VirusJump.Classes.Scene.Objects.Boards;
using VirusJump.Classes.Scene.Objects.Jumpers;
using MonoGame.Extended.Sprites;
using System.Diagnostics;
using VirusJump.Classes.Scene.Objects.Enemies;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VirusJump
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //tipkovnica in miska
        public KeyboardState k;
        public KeyboardState k_temp;
        public KeyboardState k_temp1;
        public MouseState mouseState;
        public MouseState m_temp;
        public MouseState m_temp1;

        public static gameStateEnum currentGameState;

        public enum gameStateEnum { introMenu = 0, gameRunning, pause, option, gameOver, hScore};
        public gameStateEnum gameState;

        public enum playerOrientEnum { Left = 1, Right, Tir, HeliL, HeliR, Jet, BargL, BargR } //kako bo obrnjena slika
        public static playerOrientEnum playerOrientation;

        public static Scoring score;
        public static Player player;
        public static Player playerMenu;
        public static BoardsList boardsList;
        public static Texture2D back1;
        public static Background background;
        public static Pointer pointer;
        public static Bullet bullet;
        public static Bullet bulletEnemy;


        public static Trampo trampo;
        public static Spring spring;
        public static Jetpack jetpack;

        public static List<bool> nivo;
        public static bool brisi = false;

        public static StaticEnemy staticEnemy;
        public static MovingEnemy movingEnemy;


        public static bool collisionCheck;
        public static bool gameover;

        public static bool thingsCollisionCheck;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 480;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            playerOrientation = playerOrientEnum.Right;
            collisionCheck = true;
            thingsCollisionCheck = true;
            gameover = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            nivo = new List<bool> { false, false, false, false, false };
            spriteBatch = new SpriteBatch(GraphicsDevice);
            boardsList = new BoardsList(this.Content);

            player = new Player(this.Content);
            playerMenu = new Player(this.Content);

            playerMenu.PlayerPosition = new Rectangle(60, 520, 80, 80);

            background = new Background(this.Content);
            score = new Scoring(this.Content);
            pointer = new Pointer(this.Content);
            bullet = new Bullet(this.Content);
            
            trampo = new Trampo(this.Content);
            spring = new Spring(this.Content);
            jetpack = new Jetpack(this.Content);
            bulletEnemy = new Bullet(this.Content);

            staticEnemy = new StaticEnemy(this.Content);
            movingEnemy = new MovingEnemy(this.Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            mouseState = Mouse.GetState();
            k = Keyboard.GetState();
            pointer.Position = new Vector2(mouseState.X - 10, mouseState.Y - 10);
            switch (currentGameState)
            {
                case gameStateEnum.gameRunning:
                    {
                        if (player.PlayerPosition.Y + player.PlayerPosition.Height > 720) gameover = true;

                        player.Move();
                        //to prevent from exiting from sides of screen
                        if (player.PlayerPosition.X + 10 < 0)
                        {
                            player.PlayerPosition = new Rectangle(450, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            player.ShootPosition = new Rectangle(player.PlayerPosition.X + player.PlayerPosition.Width / 2, player.PlayerPosition.Y + player.PlayerPosition.Height / 2 + 15, player.ShootPosition.Width, player.ShootPosition.Height);
                            player.FirePosition = new Vector2(player.PlayerPosition.X, player.PlayerPosition.Y + player.PlayerPosition.Height);
                        }
                        if (player.PlayerPosition.X > 451)
                        {
                            player.PlayerPosition = new Rectangle(-10, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            player.ShootPosition = new Rectangle(player.PlayerPosition.X + player.PlayerPosition.Width / 2, player.PlayerPosition.Y + player.PlayerPosition.Height / 2 + 15, player.ShootPosition.Width, player.ShootPosition.Height);
                            player.FirePosition = new Vector2(player.PlayerPosition.X, player.PlayerPosition.Y + player.PlayerPosition.Height);
                        }

                        for (int i = 0; i < 4; i++)
                            boardsList.MovingBoardList[i].Move();

                        //to move and replace tampeolines
                        if (score.Score > trampo.ScoreToMove && !trampo.Visible)
                        {
                            trampo.ScoreToMove += trampo.ScoreMoveStep;
                            do
                            {
                                Random rnd = new Random();
                                trampo.TRand = rnd.Next(0, boardsList.BoardList.Length - 1);
                            } while (boardsList.BoardList[trampo.TRand].Position.Y > 0 || boardsList.BoardList[trampo.TRand].Visible == false || (spring.SRand == trampo.TRand && (spring.SRand != -1 && trampo.TRand != -1)) || (spring.SRand == jetpack.JRand && (spring.SRand != -1 && jetpack.JRand != -1)) || trampo.TRand == jetpack.JRand && (trampo.TRand != -1 && jetpack.JRand != -1));
                            trampo.Visible = true;
                        }
                        if (trampo.TRand != -1)
                        {
                            trampo.TrampoPosition = new Rectangle(boardsList.BoardList[trampo.TRand].Position.X + 10, boardsList.BoardList[trampo.TRand].Position.Y - 15, trampo.TrampoPosition.Width, trampo.TrampoPosition.Height);
                            trampo.Visible = true;
                        }
                        if (trampo.Visible)
                        {
                            trampo.TCheck = trampo.Collision(player, collisionCheck);
                        }
                        if (trampo.TCheck && thingsCollisionCheck)
                        {
                            player.Speed = new Vector2(player.Speed.X, -32);
                            trampo.TRand = -1;
                            trampo.Visible = false;
                            trampo.TCheck = false;
                        }
                        if (trampo.TrampoPosition.Y > 690)
                        {
                            trampo.TRand = -1;
                            trampo.Visible = false;
                            trampo.TCheck = false;
                        }

                        //spring
                        if (score.Score > spring.ScoreToMove && !spring.Visible)
                        {
                            spring.ScoreToMove += spring.ScoreMoveStep;
                            do
                            {
                                Random rnd = new Random();
                                spring.SRand = rnd.Next(0, boardsList.BoardList.Length - 1);
                            } while (boardsList.BoardList[spring.SRand].Position.Y > 0 || boardsList.BoardList[spring.SRand].Visible == false || (spring.SRand == trampo.TRand && (spring.SRand != -1 && trampo.TRand != -1)) || (spring.SRand == jetpack.JRand && (spring.SRand != -1 && jetpack.JRand != -1)) || trampo.TRand == jetpack.JRand && (trampo.TRand != -1 && jetpack.JRand != -1));
                            spring.Visible = true;
                        }
                        if (spring.SRand != -1 && spring.Visible)
                        {
                            spring.SpringPosition = new Rectangle(boardsList.BoardList[spring.SRand].Position.X + 10, boardsList.BoardList[spring.SRand].Position.Y - 30, spring.SpringPosition.Width, spring.SpringPosition.Height);
                        }
                        if (spring.Visible)
                        {
                            spring.SCheck = spring.Collision(player, collisionCheck);
                        }
                        if (spring.SCheck && thingsCollisionCheck)
                        {
                            player.Speed = new Vector2(player.Speed.X, -23);
                            spring.SRand = -1;
                            spring.SCheck = false;
                            spring.Visible = false;
                        }
                        if (spring.SpringPosition.Y > 690)
                        {
                            spring.SRand = -1;
                            spring.Visible = false;
                            spring.SCheck = false;
                        }

                        //jetpack
                        if (score.Score > jetpack.ScoreToMove && !jetpack.Visible)
                        {
                            jetpack.ScoreToMove += jetpack.ScoreMoveStep;
                            do
                            {
                                Random rnd = new Random();
                                jetpack.JRand = rnd.Next(0, boardsList.BoardList.Length - 1);
                            } while (boardsList.BoardList[jetpack.JRand].Position.Y > 0 || boardsList.BoardList[jetpack.JRand].Visible == false || (spring.SRand == trampo.TRand && (spring.SRand != -1 && trampo.TRand != -1)) || (spring.SRand == jetpack.JRand && (spring.SRand != -1 && jetpack.JRand != -1)) || trampo.TRand == jetpack.JRand && (trampo.TRand != -1 && jetpack.JRand != -1));
                            jetpack.Visible = true;
                        }
                        if (jetpack.JRand != -1 && jetpack.Visible)
                        {
                            jetpack.JetPosition = new Rectangle(boardsList.BoardList[jetpack.JRand].Position.X + 10, boardsList.BoardList[jetpack.JRand].Position.Y - jetpack.JetPosition.Height, jetpack.JetPosition.Width, jetpack.JetPosition.Height);
                        }
                        if (jetpack.Visible)
                        {
                            jetpack.JCheck = jetpack.Collision(player, collisionCheck);
                        }
                        if (jetpack.JCheck && thingsCollisionCheck)
                        {
                            player.Speed = new Vector2(player.Speed.X, -60);
                            jetpack.JRand = -1;
                            jetpack.Visible = false;
                            jetpack.JCheck = false;
                            player.IsJetpack = true;
                            player.GetAnimatedSprite.Play("fire");
                        }
                        if (jetpack.JetPosition.Y > 690)
                        {
                            jetpack.JRand = -1;
                            jetpack.Visible = false;
                            jetpack.JCheck = false;
                        }

                        //movingEnemy
                        movingEnemy.Move();
                        if (movingEnemy.BulletCollision(bullet))
                        {
                            movingEnemy.MvRand = true;
                            movingEnemy.MvCollision = false;
                            movingEnemy.Visible = false;

                            if (Math.Abs(movingEnemy.Position.X - player.PlayerPosition.X) < 10)
                            {
                                if (!bulletEnemy.IsCheck)
                                {
                                    movingEnemy.Degree = (float)Math.Atan((-(player.PlayerPosition.Y - 30 - movingEnemy.Position.Y)) / (player.PlayerPosition.X - 30 - movingEnemy.Position.X));
                                    bulletEnemy.Position = new Rectangle(movingEnemy.Position.X + 30, movingEnemy.Position.Y + 30, bulletEnemy.Position.Width, bulletEnemy.Position.Height);
                                    if (player.PlayerPosition.X < movingEnemy.Position.X + 30)
                                    {
                                        bulletEnemy.Speed = new Vector2(-1 * (float)Math.Cos(movingEnemy.Degree), +1 * (float)Math.Sin(movingEnemy.Degree));
                                    }
                                    else
                                    {
                                        bulletEnemy.Speed = new Vector2(1 * (float)Math.Cos(movingEnemy.Degree), -1 * (float)Math.Sin(movingEnemy.Degree));
                                    }
                                    bulletEnemy.IsCheck = true;
                                }
                            }
                        }
                        if (bulletEnemy.Position.Y > 740 || bulletEnemy.Position.X < -20 || bulletEnemy.Position.X > 500 || bulletEnemy.Position.Y < -20)
                            bulletEnemy.IsCheck = false;
                        if (bulletEnemy.IsCheck && currentGameState == gameStateEnum.gameRunning)
                            bulletEnemy.Move();

                        if (bulletEnemy.IsCheck && player.BulletCollision(bulletEnemy))
                        {
                            player.Speed = new Vector2(player.Speed.X, 0);
                            bulletEnemy.IsCheck = false;
                            collisionCheck = false;
                        }

                        //!!!!!!!!!!!!!!!if player speed je enak tok ku je od fanar in useh teh bedarij je collision check false cene true


                        //static enemy
                        if (score.Score % 430 > 400 && staticEnemy.Position.Y > 780)//to move and replace StaticEnemies
                        {
                            do
                            {
                                Random rnd = new Random();
                                staticEnemy.StRand = rnd.Next(0, boardsList.BoardList.Length - 1);
                            } while (boardsList.BoardList[staticEnemy.StRand].Position.Y > 0 || boardsList.BoardList[staticEnemy.StRand].Visible == false || (spring.SRand == trampo.TRand && (spring.SRand != -1 && trampo.TRand != -1)) || (spring.SRand == jetpack.JRand && (spring.SRand != -1 && jetpack.JRand != -1)) || trampo.TRand == jetpack.JRand && (trampo.TRand != -1 && jetpack.JRand != -1));
                        }
                        if (staticEnemy.StRand != -1)
                        {
                            staticEnemy.Position = new Rectangle(boardsList.BoardList[staticEnemy.StRand].Position.X, boardsList.BoardList[staticEnemy.StRand].Position.Y - 53, staticEnemy.Position.Width, staticEnemy.Position.Height);
                        }
                        Debug.WriteLine(thingsCollisionCheck);
                        Debug.WriteLine(staticEnemy.Collision(player, collisionCheck).ToString());
                        if (staticEnemy.Collision(player, collisionCheck) == 0 && !gameover && thingsCollisionCheck)
                        {
                            player.Speed = new Vector2(player.Speed.X, -15);
                            staticEnemy.StRand = -1;
                        }
                        else if (staticEnemy.Collision(player, collisionCheck) == 1 && !gameover && thingsCollisionCheck)
                        {
                            player.Speed = new Vector2(player.Speed.X, 0);
                            collisionCheck = false;
                        }
                        if (staticEnemy.Position.Y < 780 && staticEnemy.StRand == -1)
                            staticEnemy.Position = new Rectangle(staticEnemy.Position.X, staticEnemy.Position.Y + 11, staticEnemy.Position.Width, staticEnemy.Position.Height);

                        if (staticEnemy.Position.Y > 795 || staticEnemy.BulletCollision(bullet))
                        {
                            staticEnemy.StRand = -1;
                            staticEnemy.Position = new Rectangle(-200, 800, 60, 55);
                            bullet.IsCheck = false;
                        }

                        //to move boards_list and background with player
                        if (player.PlayerPosition.Y < 300)
                        {
                            int speed = (int)player.Speed.Y;
                            player.PlayerPosition = new Rectangle(player.PlayerPosition.X, player.PlayerPosition.Y - (int)player.Speed.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            player.ShootPosition = new Rectangle(player.PlayerPosition.X + player.PlayerPosition.Width / 2, player.PlayerPosition.Y + player.PlayerPosition.Height / 2 + 15, player.ShootPosition.Width, player.ShootPosition.Height);
                            player.FirePosition = new Vector2(player.PlayerPosition.X, player.PlayerPosition.Y + player.PlayerPosition.Height);

                            for (int i = 0; i < boardsList.BoardList.Length; i++)
                            {
                                boardsList.BoardList[i].Position = new Rectangle(boardsList.BoardList[i].Position.X, boardsList.BoardList[i].Position.Y - speed, boardsList.BoardList[i].Position.Width, boardsList.BoardList[i].Position.Height);
                            }

                            for (int i = 0; i < boardsList.MovingBoardList.Length; i++)
                            {
                                boardsList.MovingBoardList[i].Position = new Rectangle(boardsList.MovingBoardList[i].Position.X, boardsList.MovingBoardList[i].Position.Y - speed, boardsList.MovingBoardList[i].Position.Width, boardsList.MovingBoardList[i].Position.Height);
                                boardsList.FakeBoardList[i].Position = new Rectangle(boardsList.FakeBoardList[i].Position.X, boardsList.FakeBoardList[i].Position.Y - speed, boardsList.FakeBoardList[i].Position.Width, boardsList.FakeBoardList[i].Position.Height);
                                boardsList.GoneBoardList[i].Position = new Rectangle(boardsList.GoneBoardList[i].Position.X, boardsList.GoneBoardList[i].Position.Y - speed, boardsList.GoneBoardList[i].Position.Width, boardsList.GoneBoardList[i].Position.Height);
                            }
                            //if (movingEnemy.Position.Y < 790 && movingEnemy.MvRand)
                            //    movingEnemy.Position = new Rectangle(movingEnemy.Position.X, movingEnemy.Position.Y, movingEnemy.Position.Width, movingEnemy.Position.Height);

                            if (background.BPosize.Y < 0)
                                background.BPosize = new Rectangle(background.BPosize.X, background.BPosize.Y - (speed / 2), background.BPosize.Width, background.BPosize.Height);
                            background.SPosise1 = new Rectangle(background.SPosise1.X, background.SPosise1.Y - (speed / 2), background.SPosise1.Width, background.SPosise1.Height);
                            background.SPosise2 = new Rectangle(background.SPosise2.X, background.SPosise2.Y - (speed / 2), background.SPosise2.Width, background.SPosise2.Height);
                            score.Score -= speed / 2;
                        }
                        background.SideCheck();

                        GameRenderer.rePosition();//to re position boards_list and movable enemys

                        //to check boards_list coliision
                        if (thingsCollisionCheck)
                        {
                            for (int i = 0; i < boardsList.BoardList.Length; i++)
                                if (boardsList.BoardList[i].Visible && boardsList.BoardList[i].Collision(player) && !gameover && collisionCheck == true)
                                {
                                    player.Speed = new Vector2(player.Speed.X, -13);
                                }
                            for (int i = 0; i < boardsList.MovingBoardList.Length; i++)
                            {
                                if (boardsList.MovingBoardList[i].Collision(player) && !gameover && collisionCheck == true)
                                {
                                    player.Speed = new Vector2(player.Speed.X, -13);
                                }
                                if (boardsList.FakeBoardList[i].Visible && boardsList.FakeBoardList[i].Collision(player) && !gameover && collisionCheck == true)
                                {
                                    boardsList.FakeBoardList[i].Visible = false;
                                }
                                if (boardsList.GoneBoardList[i].Visible && boardsList.GoneBoardList[i].Collision(player) && !gameover && collisionCheck == true)
                                {
                                    player.Speed = new Vector2(player.Speed.X, -13);
                                    boardsList.GoneBoardList[i].Visible = false;
                                }
                            }
                        }

                        //to go to pause menue bye esc clicking
                        k_temp1 = Keyboard.GetState();
                        if (k_temp1.IsKeyDown(Keys.Escape) && !k_temp.IsKeyDown(Keys.Escape))
                        {
                            currentGameState = gameStateEnum.pause;
                            break;
                        }
                        //to move left and right
                        k_temp = k_temp1;
                        if (k.IsKeyDown(Keys.Left))
                        {
                            playerOrientation = playerOrientEnum.Left;
                            player.PlayerPosition = new Rectangle(player.PlayerPosition.X - 7, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            player.ShootPosition = new Rectangle(player.PlayerPosition.X + player.PlayerPosition.Width / 2, player.PlayerPosition.Y + player.PlayerPosition.Height / 2 + 15, player.ShootPosition.Width, player.ShootPosition.Height);
                            player.FirePosition = new Vector2(player.PlayerPosition.X, player.PlayerPosition.Y + player.PlayerPosition.Height);
                        }
                        else if (k.IsKeyDown(Keys.Right))
                        {
                            playerOrientation = playerOrientEnum.Right;
                            player.PlayerPosition = new Rectangle(player.PlayerPosition.X + 7, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            player.ShootPosition = new Rectangle(player.PlayerPosition.X + player.PlayerPosition.Width / 2, player.PlayerPosition.Y + player.PlayerPosition.Height / 2 + 15, player.ShootPosition.Width, player.ShootPosition.Height);
                            player.FirePosition = new Vector2(player.PlayerPosition.X, player.PlayerPosition.Y + player.PlayerPosition.Height);
                        }

                        //check mouse state for shoot and pause menu 
                        mouseState = Mouse.GetState();
                        if (mouseState.LeftButton == ButtonState.Pressed && !(m_temp.LeftButton == ButtonState.Pressed) && currentGameState == gameStateEnum.gameRunning)
                        {
                            if (mouseState.X > 420 && mouseState.X < 470 && mouseState.Y > 5 && mouseState.Y < 40)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                currentGameState = gameStateEnum.pause;
                            }
                            //to shoot tir
                            else
                            {
                                if (!bullet.IsCheck)
                                {
                                    player.Degree = (float)Math.Atan((-(mouseState.Y - player.PlayerPosition.Y - 27)) / (mouseState.X - player.PlayerPosition.X - 30));
                                    bullet.Position = new Rectangle(player.PlayerPosition.X + 30, player.PlayerPosition.Y + 27, bullet.Position.Width, bullet.Position.Height);
                                    pointer.GetAnimatedSprite.Play("shoot");
                                    if (mouseState.X < player.PlayerPosition.X + 30)
                                    {
                                        bullet.Speed = new Vector2(-25 * (float)Math.Cos(player.Degree), +25 * (float)Math.Sin(player.Degree));
                                    }
                                    else
                                    {
                                        bullet.Speed = new Vector2(25 * (float)Math.Cos(player.Degree), -25 * (float)Math.Sin(player.Degree));
                                    }
                                    bullet.IsCheck = true;
                                }
                            }
                        }
                        if (bullet.Position.Y > 740 || bullet.Position.X < -20 || bullet.Position.X > 500 || bullet.Position.Y < -20)
                            bullet.IsCheck = false;
                        if (bullet.IsCheck && currentGameState == gameStateEnum.gameRunning)
                            bullet.Move();

                        MouseState mouseControl = Mouse.GetState();
                        player.ShootDegree = -(float)Math.Atan2(mouseControl.X - player.PlayerPosition.X, mouseControl.Y - player.PlayerPosition.Y);

                        //popravi collision ko je jetpack
                        if (player.Speed.Y > -12)
                        {
                            thingsCollisionCheck = true;
                            player.IsJetpack = false;
                        }
                        else thingsCollisionCheck = false;

                        //to end and gameovering game
                        if (player.PlayerPosition.Y > 720)
                            currentGameState = gameStateEnum.gameOver;
                    }
                    break;
                case gameStateEnum.pause:
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.X > 131 && mouseState.X < 251)
                            if (mouseState.Y > 372 && mouseState.Y < 428)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                currentGameState = gameStateEnum.gameRunning;
                            }
                        if (mouseState.X > 215 && mouseState.X < 335)
                            if (mouseState.Y > 454 && mouseState.Y < 510)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                currentGameState = gameStateEnum.introMenu;
                                playerOrientation = playerOrientEnum.Right;
                                Thread.Sleep(100);
                            }
                        if (mouseState.X > 66 && mouseState.X < 186)
                            if (mouseState.Y > 282 && mouseState.Y < 338)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                currentGameState = gameStateEnum.option;
                                playerOrientation = playerOrientEnum.Right;
                            }
                    }
                    k_temp = Keyboard.GetState();
                    if (k_temp.IsKeyDown(Keys.Escape) && !k_temp1.IsKeyDown(Keys.Escape))
                        currentGameState = gameStateEnum.gameRunning;
                    k_temp1 = k_temp;
                    background.GameStateCheck = false;
                    break;
                case gameStateEnum.option:
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.X > 297 && mouseState.X < 415)
                            if (mouseState.Y > 530 && mouseState.Y < 584)
                                if (background.GameStateCheck == true)
                                {
                                    pointer.GetAnimatedSprite.Play("shoot");
                                    currentGameState = gameStateEnum.introMenu;
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    pointer.GetAnimatedSprite.Play("shoot");
                                    currentGameState = gameStateEnum.pause;
                                    Thread.Sleep(100);
                                }
                        if (mouseState.X > 210 && mouseState.X < 278)
                            if (mouseState.Y > 405 && mouseState.Y < 462)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                background.SoundCheck = true;
                            }
                        if (mouseState.X > 99 && mouseState.X < 176)
                            if (mouseState.Y > 407 && mouseState.Y < 461)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                background.SoundCheck = false;
                            }
                    }
                    break;
                case gameStateEnum.introMenu:
                    mouseState = Mouse.GetState();
                    if (mouseState.LeftButton == ButtonState.Pressed && !(m_temp1.LeftButton == ButtonState.Pressed))
                    {
                        background.GameStateCheck = true;
                        if (mouseState.X > 67 && mouseState.X < 185)
                            if (mouseState.Y > 283 && mouseState.Y < 337)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                mouseState = m_temp;
                                currentGameState = gameStateEnum.gameRunning;
                                GameRenderer.PlayAgain(player, score, background);
                                Thread.Sleep(100);
                            }
                        if (mouseState.X > 292 && mouseState.X < 410)
                            if (mouseState.Y > 528 && mouseState.Y < 582 && currentGameState == gameStateEnum.introMenu)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                this.Exit();
                            }
                        if (mouseState.X > 217 && mouseState.X < 335)
                            if (mouseState.Y > 454 && mouseState.Y < 508)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                currentGameState = gameStateEnum.option;
                                Thread.Sleep(100);
                            }
                        if (mouseState.X > 130 && mouseState.X < 248)
                            if (mouseState.Y > 373 && mouseState.Y < 427)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                currentGameState = gameStateEnum.hScore;
                                Thread.Sleep(100);
                            }
                    }
                    playerMenu.Move();
                    if (playerMenu.PlayerPosition.Y > 550)
                        playerMenu.Speed = new Vector2(playerMenu.Speed.X, -13);
                    break;
                case gameStateEnum.hScore:
                    mouseState = Mouse.GetState();
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.X > 296 && mouseState.X < 415)
                            if (mouseState.Y > 529 && mouseState.Y < 584)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                currentGameState = gameStateEnum.introMenu;
                                Thread.Sleep(100);
                            }
                    }
                    break;
                case gameStateEnum.gameOver:
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.X > 88 && mouseState.X < 271)
                            if (mouseState.Y > 438 && mouseState.Y < 500)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                GameRenderer.PlayAgain(player, score, background);
                                Thread.Sleep(100);
                            }
                        if (mouseState.X > 284 && mouseState.X < 404)
                            if (mouseState.Y > 504 && mouseState.Y < 559)
                            {
                                pointer.GetAnimatedSprite.Play("shoot");
                                currentGameState = gameStateEnum.introMenu;
                                playerOrientation = playerOrientEnum.Right;
                                Thread.Sleep(100);
                            }
                        mouseState = m_temp1;
                    }
                    break;
            }

            pointer.GetAnimatedSprite.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            background.Draw(spriteBatch, currentGameState, score);

            if (currentGameState == gameStateEnum.gameRunning)
            {
                background.ScoreDraw(spriteBatch, currentGameState);
                score.Draw(spriteBatch, currentGameState);
                for (int i = 0; i < boardsList.BoardList.Length; i++)
                {
                    if(boardsList.BoardList[i].Visible && boardsList.BoardList[i].DrawVisible)
                    {
                        boardsList.BoardList[i].DrawSprite(spriteBatch);
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    boardsList.MovingBoardList[i].DrawSprite(spriteBatch);
                    if (boardsList.FakeBoardList[i].Visible && boardsList.FakeBoardList[i].DrawVisible)
                    {
                        boardsList.FakeBoardList[i].DrawSprite(spriteBatch);
                    }
                    if (boardsList.GoneBoardList[i].Visible && boardsList.GoneBoardList[i].DrawVisible)
                    {
                        boardsList.GoneBoardList[i].DrawSprite(spriteBatch);
                    }
                }

                if (trampo.Visible)
                {
                    trampo.Draw(spriteBatch);
                }
                if (spring.Visible)
                {
                    spring.Draw(spriteBatch);
                }
                if (jetpack.Visible)
                {
                    jetpack.Draw(spriteBatch);
                }
                staticEnemy.Draw(spriteBatch);
                if(movingEnemy.Visible)
                {
                    movingEnemy.Draw(spriteBatch);
                }

                player.Draw(spriteBatch, playerOrientation, currentGameState);
            }
            if (currentGameState == gameStateEnum.introMenu) 
            {
                playerMenu.Draw(spriteBatch, playerOrientation, gameStateEnum.introMenu);
            }
            if(bullet.IsCheck)
            {
                bullet.Draw(spriteBatch, currentGameState);
            }
            if (bulletEnemy.IsCheck)
            {
                bulletEnemy.Draw(spriteBatch, currentGameState);
            }
            pointer.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}