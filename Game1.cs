﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using VirusJump.Classes.Scene.Objects;
using System.Collections.Generic;
using System;
using System.Threading;
using VirusJump.Classes.Scene.Objects.Supplements;
using VirusJump.Classes.Scene.Objects.Boards;
using VirusJump.Classes.Scene.Objects.Jumpers;
using MonoGame.Extended.Sprites;
using System.Diagnostics;

namespace VirusJump
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private AnimatedSprite animateSprite;

        //playagain in reposition gre v renderer
        public void PlayAgain(Player player, Scoring score, Background background)
        {
            currentGameState = gameStateEnum.gameRunning;//menjaj 
            collisionCheck = true;
            score.Check = true;
            gameover = false;
            player.Initialize(); 
            boardsList.Initialize();
            bullet.Initialize();
            background.Initialize();
            playerOrientation = playerOrientEnum.Right;
            score.Score = 0;
            meRnd = true;
            mecolosion = false;
            trampo.Initialize();
            spring.Initialize();
            currentFrame = 1;
            jetpack.Initialize();

            //delete boards
            nivo = new List<bool> { false, false, false,false,false };
            brisi = false;
        }

        public void rePosition()
        {
            int minY = 999;
            Random rnd = new Random();
            
            for (int i = 0; i < boardsList.BoardList.Length; i++)
            {
                if (boardsList.BoardList[i].Visible)
                {
                    if (boardsList.BoardList[i].BoardPosition.Y < -28)
                    {
                        boardsList.BoardList[i].DrawVisible = false;
                    }
                    else if (boardsList.BoardList[i].BoardPosition.Y > -28)
                    {
                        boardsList.BoardList[i].DrawVisible = true;
                    }
                    if (boardsList.BoardList[i].BoardPosition.Y > 734)
                    {
                        for (int j = 0; j < boardsList.BoardList.Length; j++)
                            if (boardsList.BoardList[j].BoardPosition.Y < minY) minY = boardsList.BoardList[j].BoardPosition.Y;
                        for (int j = 0; j < 4; j++)
                        {
                            if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                            if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                            if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                        }
                        boardsList.BoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 56, minY - 28), boardsList.BoardList[i].BoardPosition.Width, boardsList.BoardList[i].BoardPosition.Height);
                    }
                }

                if (i < 4)
                {
                    if (boardsList.FakeBoardList[i].BoardPosition.Y < -28)
                    {
                        boardsList.FakeBoardList[i].DrawVisible = false;
                    }
                    else if (boardsList.FakeBoardList[i].BoardPosition.Y > -28)
                    {
                        boardsList.FakeBoardList[i].DrawVisible = true;
                    }

                    if (boardsList.GoneBoardList[i].BoardPosition.Y < -28)
                    {
                        boardsList.GoneBoardList[i].DrawVisible = false;
                    }
                    else if (boardsList.GoneBoardList[i].BoardPosition.Y > -28)
                    {
                        boardsList.GoneBoardList[i].DrawVisible = true;
                    }

                    if (boardsList.MovingBoardList[i].BoardPosition.Y > 734)
                    {
                        for (int j = 0; j < boardsList.BoardList.Length; j++)
                            if (boardsList.BoardList[j].BoardPosition.Y < minY && boardsList.BoardList[i].Visible) minY = boardsList.BoardList[j].BoardPosition.Y;
                        for (int j = 0; j < 4; j++)
                        {
                            if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                            if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                            if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                        }
                        boardsList.MovingBoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 56, minY - 28), boardsList.MovingBoardList[i].BoardPosition.Width, boardsList.MovingBoardList[i].BoardPosition.Height);
                    }
                    if (boardsList.FakeBoardList[i].BoardPosition.Y > 734)
                    {
                        for (int j = 0; j < boardsList.BoardList.Length; j++)
                            if (boardsList.BoardList[j].BoardPosition.Y < minY && boardsList.BoardList[i].Visible) minY = boardsList.BoardList[j].BoardPosition.Y;
                        for (int j = 0; j < 4; j++)
                        {
                            if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                            if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                            if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                        }
                        boardsList.FakeBoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 56, minY - 28), boardsList.FakeBoardList[i].BoardPosition.Width, boardsList.FakeBoardList[i].BoardPosition.Height);
                        boardsList.FakeBoardList[i].Visible = true;
                    }
                    if (boardsList.GoneBoardList[i].BoardPosition.Y > 734)
                    {
                        for (int j = 0; j < boardsList.BoardList.Length; j++)
                            if (boardsList.BoardList[j].BoardPosition.Y < minY && boardsList.BoardList[i].Visible) minY = boardsList.BoardList[j].BoardPosition.Y;
                        for (int j = 0; j < 4; j++)
                        {
                            if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                            if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                            if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                        }
                        boardsList.GoneBoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 56, minY - 28), boardsList.GoneBoardList[i].BoardPosition.Width, boardsList.GoneBoardList[i].BoardPosition.Height);
                        boardsList.GoneBoardList[i].Visible = true;
                    }
                }
            }

            //delete boards
            if (score.Score > 500 && !nivo[0])
            {
                nivo[0] = true;
                brisi = true;
                spring.ScoreMoveStep = 700;
                trampo.ScoreMoveStep = 1700;
                jetpack.ScoreMoveStep = 3000;
            }
            else if(score.Score > 1000 && !nivo[1])
            {
                nivo[1] = true;
                brisi = true;
                spring.ScoreMoveStep = 1000;
                trampo.ScoreMoveStep = 2000;
                jetpack.ScoreMoveStep = 5000;
            }
            else if(score.Score > 2000 && !nivo[2])
            {
                nivo[2] = true;
                brisi = true;
                spring.ScoreMoveStep = 2000;
                trampo.ScoreMoveStep = 3000;
                jetpack.ScoreMoveStep = 6000;
            }
            else if (score.Score > 3000 && !nivo[3])
            {
                nivo[3] = true;
                brisi = true;
                spring.ScoreMoveStep = 3000;
                trampo.ScoreMoveStep = 4000;
                jetpack.ScoreMoveStep = 8000;
            }
            else if (score.Score > 4000 && !nivo[4])
            {
                nivo[4] = true;
                brisi = true;
                spring.ScoreMoveStep = 4000;
                trampo.ScoreMoveStep = 6000;
                jetpack.ScoreMoveStep = 12000;
            }

            if (brisi)
            {
                brisi = false;
                int outBoard = 0;
                for (int j = 0; j < boardsList.BoardList.Length; j++)
                {
                    if (boardsList.BoardList[j].BoardPosition.Y < -20 && boardsList.BoardList[j].Visible && j != trampo.TRand && j != spring.SRand && j != jetpack.JRand)
                    {
                        boardsList.BoardList[j].Visible = false;
                        outBoard++;
                    }
                    if (outBoard == 2)
                        break;
                }
            }

        }

        //tipkovnica in miska
        public KeyboardState k;
        public KeyboardState k_temp;
        public KeyboardState k_temp1;
        public MouseState mouseState;
        public MouseState m_temp;
        public MouseState m_temp1;

        public gameStateEnum currentGameState;

        public enum gameStateEnum { introMenu = 0, gameRunning, pause, option, gameOver, hScore};
        public gameStateEnum gameState;

        public enum playerOrientEnum { Left = 1, Right, Tir, HeliL, HeliR, Jet, BargL, BargR } //kako bo obrnjena slika
        public playerOrientEnum playerOrientation;

        public Scoring score;
        public Player player;
        public Player playerMenu;
        public BoardsList boardsList;
        public Texture2D back1;
        public Background background;
        public Pointer pointer;
        public Bullet bullet;

        public Trampo trampo;
        public Spring spring;
        public Jetpack jetpack;

        public List<bool> nivo = new List<bool> { false, false,false,false,false };
        public bool brisi = false;
        
        public bool tirCheck;
        public bool collisionCheck;
        public bool gameover;
        public bool meRnd;
        public bool mecolosion;
        public int currentFrame = 1;


        public Vector2 distance;

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
            tirCheck = false;
            collisionCheck = true;
            mecolosion = false;
            gameover = false;
            meRnd = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
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

            animateSprite = new AnimatedSprite(pointer.GetSpriteSheet);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            mouseState = Mouse.GetState();
            k = Keyboard.GetState();
            pointer.PointerPosition = new Vector2(mouseState.X - 10, mouseState.Y - 10);
            switch (currentGameState)
            {
                case gameStateEnum.gameRunning:
                    {
                        if (player.PlayerPosition.Y + 60 > 720) gameover = true;
                        
                        player.Move();
                        //to prevent from exiting from sides of screen
                        if (player.PlayerPosition.X + 10 < 0)
                        {
                            player.PlayerPosition = new Rectangle(450, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            player.ShootPosition = new Rectangle(player.PlayerPosition.X + player.PlayerPosition.Width / 2, player.PlayerPosition.Y + player.PlayerPosition.Height / 2 + 15, player.ShootPosition.Width, player.ShootPosition.Height);
                        }
                        if (player.PlayerPosition.X > 451)
                        {
                            player.PlayerPosition = new Rectangle(-10, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            player.ShootPosition = new Rectangle(player.PlayerPosition.X + player.PlayerPosition.Width / 2, player.PlayerPosition.Y + player.PlayerPosition.Height / 2 + 15, player.ShootPosition.Width, player.ShootPosition.Height);
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
                            } while (boardsList.BoardList[trampo.TRand].BoardPosition.Y > 0 || boardsList.BoardList[trampo.TRand].Visible == false || (spring.SRand == trampo.TRand && (spring.SRand != -1 && trampo.TRand != -1)) || (spring.SRand == jetpack.JRand && (spring.SRand != -1 && jetpack.JRand != -1)) || trampo.TRand == jetpack.JRand && (trampo.TRand != -1 && jetpack.JRand != -1));
                            trampo.Visible = true;
                            collisionCheck = false;
                        }
                        if (trampo.TRand != -1)
                        {
                            trampo.TrampoPosition = new Rectangle(boardsList.BoardList[trampo.TRand].BoardPosition.X + 10, boardsList.BoardList[trampo.TRand].BoardPosition.Y - 15, trampo.TrampoPosition.Width, trampo.TrampoPosition.Height);
                            trampo.Visible = true;
                        }
                        if (trampo.Visible)
                        {  
                            trampo.TCheck = trampo.Collision(player, collisionCheck);
                        }
                        if (trampo.TCheck)
                        {
                            player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -32);
                            trampo.TRand = -1;
                            trampo.Visible = false;
                            collisionCheck = false;
                            trampo.TCheck = false;
                        }
                        if (trampo.TrampoPosition.Y > 690)
                        {
                            trampo.TRand = -1;
                            trampo.Visible = false;
                            trampo.TCheck = false;
                        }


                        if (score.Score > spring.ScoreToMove && !spring.Visible)
                        {
                            spring.ScoreToMove += spring.ScoreMoveStep;
                            do
                            {
                                Random rnd = new Random();
                                spring.SRand = rnd.Next(0, boardsList.BoardList.Length - 1);
                            } while (boardsList.BoardList[spring.SRand].BoardPosition.Y > 0 || boardsList.BoardList[spring.SRand].Visible == false || (spring.SRand == trampo.TRand && (spring.SRand != -1 && trampo.TRand != -1)) || (spring.SRand == jetpack.JRand && (spring.SRand != -1 && jetpack.JRand != -1)) || trampo.TRand == jetpack.JRand && (trampo.TRand != -1 && jetpack.JRand != -1));
                            spring.Visible = true;
                            spring.InOut = true;
                            collisionCheck = true;
                        }
                        if (spring.SRand != -1 && spring.Visible)
                        {
                            spring.SpringPosition = new Rectangle(boardsList.BoardList[spring.SRand].BoardPosition.X + 10, boardsList.BoardList[spring.SRand].BoardPosition.Y - 30, spring.SpringPosition.Width, spring.SpringPosition.Height);
                            spring.Visible = true;
                        }
                        if (spring.Visible)
                        {
                            spring.SCheck = spring.Collision(player, collisionCheck);
                        }
                        if (spring.SCheck)
                        {
                            player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -23);
                            spring.SRand = -1;
                            spring.SCheck = false;
                            collisionCheck = false;
                            spring.InOut = false;
                        }
                        if (spring.SpringPosition.Y > 690)
                        {
                            spring.SRand = -1;
                            spring.Visible = false;
                            spring.SCheck = false;
                            spring.InOut = true;
                        }

                        if (score.Score > jetpack.ScoreToMove && !jetpack.Visible)
                        {
                            jetpack.ScoreToMove += jetpack.ScoreMoveStep;
                            do
                            {
                                Random rnd = new Random();
                                jetpack.JRand = rnd.Next(0, boardsList.BoardList.Length - 1);
                            } while (boardsList.BoardList[jetpack.JRand].BoardPosition.Y > 0 || boardsList.BoardList[jetpack.JRand].Visible == false || (spring.SRand == trampo.TRand && (spring.SRand != -1 && trampo.TRand != -1)) || (spring.SRand == jetpack.JRand && (spring.SRand != -1 && jetpack.JRand != -1)) || trampo.TRand == jetpack.JRand && (trampo.TRand != -1 && jetpack.JRand != -1));
                            jetpack.Visible = true;
                            collisionCheck = true;
                        }
                        if (jetpack.JRand != -1 && jetpack.Visible)
                        {
                            jetpack.JetPosition = new Rectangle(boardsList.BoardList[jetpack.JRand].BoardPosition.X + 10, boardsList.BoardList[jetpack.JRand].BoardPosition.Y - jetpack.JetPosition.Height, jetpack.JetPosition.Width, jetpack.JetPosition.Height);
                            jetpack.Visible = true;
                        }
                        if (jetpack.Visible)
                        {
                            jetpack.JCheck = jetpack.Collision(player, collisionCheck);
                        }
                        if (jetpack.JCheck)
                        {
                            player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -60);
                            collisionCheck = false;
                            jetpack.JRand = -1;
                            jetpack.Visible = false;
                            jetpack.JCheck = false;
                        }
                        if (jetpack.JetPosition.Y > 690)
                        {
                            jetpack.JRand = -1;
                            jetpack.Visible = false;
                            jetpack.JCheck = false;
                        }

                        //to move boards_list and background with player
                        if (player.PlayerPosition.Y < 300) 
                        {
                            int speed = (int)player.PlayerSpeed.Y;
                            player.PlayerPosition = new Rectangle(player.PlayerPosition.X, player.PlayerPosition.Y - (int)player.PlayerSpeed.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            player.ShootPosition = new Rectangle(player.PlayerPosition.X + player.PlayerPosition.Width / 2, player.PlayerPosition.Y + player.PlayerPosition.Height / 2 + 15, player.ShootPosition.Width, player.ShootPosition.Height);

                            for (int i = 0; i < boardsList.BoardList.Length; i++)
                            {
                                boardsList.BoardList[i].BoardPosition = new Rectangle(boardsList.BoardList[i].BoardPosition.X, boardsList.BoardList[i].BoardPosition.Y - speed, boardsList.BoardList[i].BoardPosition.Width, boardsList.BoardList[i].BoardPosition.Height);
                            }

                            for (int i = 0; i < boardsList.MovingBoardList.Length; i++)
                            {
                                boardsList.MovingBoardList[i].BoardPosition = new Rectangle(boardsList.MovingBoardList[i].BoardPosition.X, boardsList.MovingBoardList[i].BoardPosition.Y - speed, boardsList.MovingBoardList[i].BoardPosition.Width, boardsList.MovingBoardList[i].BoardPosition.Height);
                                boardsList.FakeBoardList[i].BoardPosition = new Rectangle(boardsList.FakeBoardList[i].BoardPosition.X, boardsList.FakeBoardList[i].BoardPosition.Y - speed, boardsList.FakeBoardList[i].BoardPosition.Width, boardsList.FakeBoardList[i].BoardPosition.Height);
                                boardsList.GoneBoardList[i].BoardPosition = new Rectangle(boardsList.GoneBoardList[i].BoardPosition.X, boardsList.GoneBoardList[i].BoardPosition.Y - speed, boardsList.GoneBoardList[i].BoardPosition.Width, boardsList.GoneBoardList[i].BoardPosition.Height);
                            }

                            if (background.BPosize.Y < 0)
                                background.BPosize = new Rectangle(background.BPosize.X, background.BPosize.Y - (speed / 2), background.BPosize.Width, background.BPosize.Height);
                            background.SPosise1 = new Rectangle(background.SPosise1.X, background.SPosise1.Y - (speed / 2), background.SPosise1.Width, background.SPosise1.Height);
                            background.SPosise2 = new Rectangle(background.SPosise2.X, background.SPosise2.Y - (speed / 2), background.SPosise2.Width, background.SPosise2.Height);
                            score.Score -= speed / 2;
                        }
                        background.SideCheck();

                        rePosition();//to re position boards_list and movable enemys

                        //to check boards_list coliision
                        collisionCheck = true;
                        for (int i = 0; i < boardsList.BoardList.Length; i++)
                            if (boardsList.BoardList[i].Visible && boardsList.BoardList[i].Collision(player) && !gameover && collisionCheck == true)
                            {
                                player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -13);
                            }
                        for (int i = 0; i < boardsList.MovingBoardList.Length; i++)
                        {
                            if (boardsList.MovingBoardList[i].Collision(player) && !gameover && collisionCheck == true)
                            {
                                player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -13);
                            }
                            if (boardsList.FakeBoardList[i].Visible && boardsList.FakeBoardList[i].Collision(player) && !gameover && collisionCheck == true)
                            {
                                boardsList.FakeBoardList[i].Visible = false;
                            }
                            if (boardsList.GoneBoardList[i].Visible && boardsList.GoneBoardList[i].Collision(player) && !gameover && collisionCheck == true)
                            {
                                player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -13);
                                boardsList.GoneBoardList[i].Visible = false;
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
                        }
                        else if (k.IsKeyDown(Keys.Right))
                        {
                            playerOrientation = playerOrientEnum.Right;
                            player.PlayerPosition = new Rectangle(player.PlayerPosition.X + 7, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            player.ShootPosition = new Rectangle(player.PlayerPosition.X + player.PlayerPosition.Width / 2, player.PlayerPosition.Y + player.PlayerPosition.Height / 2 + 15, player.ShootPosition.Width, player.ShootPosition.Height);
                        }

                        //check mouse state for shoot and pause menu 
                        mouseState = Mouse.GetState();
                        if (mouseState.LeftButton == ButtonState.Pressed && !(m_temp.LeftButton == ButtonState.Pressed) && currentGameState == gameStateEnum.gameRunning)
                        {
                            if (mouseState.X > 420 && mouseState.X < 470 && mouseState.Y > 5 && mouseState.Y < 40)
                            {
                                animateSprite.Play("shoot");
                                currentGameState = gameStateEnum.pause;
                                MediaPlayer.Pause();
                            }
                            //to shoot tir
                            else
                            {
                                if (!tirCheck)
                                {
                                    player.Degree = (float)Math.Atan((-(mouseState.Y - player.PlayerPosition.Y - 27)) / (mouseState.X - player.PlayerPosition.X - 30));
                                    bullet.BulletPosition = new Rectangle(player.PlayerPosition.X + 30,player.PlayerPosition.Y + 27, bullet.BulletPosition.Width, bullet.BulletPosition.Height);
                                    animateSprite.Play("shoot");
                                    if (mouseState.X < player.PlayerPosition.X + 30)
                                    {
                                        bullet.BulletSpeed = new Vector2(-25 * (float)Math.Cos(player.Degree), +25 * (float)Math.Sin(player.Degree));
                                    }
                                    else
                                    {
                                        bullet.BulletSpeed = new Vector2(25 * (float)Math.Cos(player.Degree), -25 * (float)Math.Sin(player.Degree));
                                    }
                                    tirCheck = true;
                                }
                            }
                        }
                        if (bullet.BulletPosition.Y > 740 || bullet.BulletPosition.X < -20 || bullet.BulletPosition.X > 500 || bullet.BulletPosition.Y < -20)
                            tirCheck = false;
                        if (tirCheck && currentGameState == gameStateEnum.gameRunning)
                            bullet.Move();

                        MouseState mouseControl = Mouse.GetState();
                        player.ShootDegree = -(float)Math.Atan2(mouseControl.X - player.PlayerPosition.X, mouseControl.Y - player.PlayerPosition.Y);

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
                                animateSprite.Play("shoot");
                                currentGameState = gameStateEnum.gameRunning;
                                MediaPlayer.Resume();
                            }
                        if (mouseState.X > 215 && mouseState.X < 335)
                            if (mouseState.Y > 454 && mouseState.Y < 510)
                            {
                                animateSprite.Play("shoot");
                                currentGameState = gameStateEnum.introMenu;
                                playerOrientation = playerOrientEnum.Right;
                                Thread.Sleep(100);
                            }
                        if (mouseState.X > 66 && mouseState.X < 186)
                            if (mouseState.Y > 282 && mouseState.Y < 338)
                            {
                                animateSprite.Play("shoot");
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
                                    animateSprite.Play("shoot");
                                    currentGameState = gameStateEnum.introMenu;
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    animateSprite.Play("shoot");
                                    currentGameState = gameStateEnum.pause;
                                    Thread.Sleep(100);
                                }
                        if (mouseState.X > 210 && mouseState.X < 278)
                            if (mouseState.Y > 405 && mouseState.Y < 462)
                            {
                                animateSprite.Play("shoot");
                                background.SoundCheck = true;
                            }
                        if (mouseState.X > 99 && mouseState.X < 176)
                            if (mouseState.Y > 407 && mouseState.Y < 461)
                            {
                                animateSprite.Play("shoot");
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
                                animateSprite.Play("shoot");
                                mouseState = m_temp;
                                currentGameState = gameStateEnum.gameRunning;
                                MediaPlayer.Resume();
                                PlayAgain(player, score, background);
                                Thread.Sleep(100);
                            }
                        if (mouseState.X > 292 && mouseState.X < 410)
                            if (mouseState.Y > 528 && mouseState.Y < 582 && currentGameState == gameStateEnum.introMenu)
                            {
                                animateSprite.Play("shoot");
                                this.Exit();
                            }
                        if (mouseState.X > 217 && mouseState.X < 335)
                            if (mouseState.Y > 454 && mouseState.Y < 508)
                            {
                                animateSprite.Play("shoot");
                                currentGameState = gameStateEnum.option;
                                Thread.Sleep(100);
                            }
                        if (mouseState.X > 130 && mouseState.X < 248)
                            if (mouseState.Y > 373 && mouseState.Y < 427)
                            {
                                animateSprite.Play("shoot");
                                currentGameState = gameStateEnum.hScore;
                                Thread.Sleep(100);
                            }
                    }
                    playerMenu.Move();
                    if (playerMenu.PlayerPosition.Y > 550)
                        playerMenu.PlayerSpeed = new Vector2(playerMenu.PlayerSpeed.X, -13);
                    break;
                case gameStateEnum.hScore:
                    mouseState = Mouse.GetState();
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.X > 296 && mouseState.X < 415)
                            if (mouseState.Y > 529 && mouseState.Y < 584)
                            {
                                animateSprite.Play("shoot");
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
                                animateSprite.Play("shoot");
                                PlayAgain(player, score, background);
                                Thread.Sleep(100);
                            }
                        if (mouseState.X > 284 && mouseState.X < 404)
                            if (mouseState.Y > 504 && mouseState.Y < 559)
                            {
                                animateSprite.Play("shoot");
                                currentGameState = gameStateEnum.introMenu;
                                MediaPlayer.Pause();
                                playerOrientation = playerOrientEnum.Right;
                                Thread.Sleep(100);
                            }
                        mouseState = m_temp1;
                    }
                    break;
            }

            animateSprite.Update(gameTime);
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
                if (jetpack.JCheck) player.IsJetpack = true;
                else player.IsJetpack = false;
                if (jetpack.Visible)
                {
                    jetpack.Draw(spriteBatch);
                }

                player.Draw(spriteBatch, playerOrientation, currentGameState);
            }
            if (currentGameState == gameStateEnum.introMenu) 
            {
                playerMenu.Draw(spriteBatch, playerOrientation, gameStateEnum.introMenu);
            }
            bullet.Draw(spriteBatch, currentGameState);
            pointer.Draw(animateSprite, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}