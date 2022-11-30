using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using VirusJump.Classes.Scene.Objects;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection.Metadata;
using VirusJump.Classes.Scene.Objects.Supplements;
using VirusJump.Classes.Scene.Objects.Boards;
using System.Diagnostics;
using static VirusJump.Game1;

namespace VirusJump
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //playagain in reposition gre v renderer
        public void PlayAgain(Player player, Scoring score, Background background, ref int gameState)
        {
            gameState = gameRunning;
            collisionCheck = true;
            score.Check = true;
            gameover = false;
            player.Initialize(); 
            boardsList.Initialize();
            bullet.Initialize();
            background.BPosize = new Rectangle(0, -6480, 480, 7200);
            background.KPosize = new Rectangle(0, 0, 480, 720);
            background.SPosise1 = new Rectangle(0, -2880, 480, 3600);
            background.SPosise2 = new Rectangle(0, -6480, 480, 3600);
            background.BPosize = new Rectangle(background.BPosize.X, -7200 + 720, background.BPosize.Width, background.BPosize.Height);
            dir = cond.Right;
            score.SNevem = 0;
            fRnd = -1;
            tRnd = -1;
            eRnd = -1;
            background.SoundCheck = true;
            background.GameStateCheck = true;
            meRnd = true;
            mecolosion = false;
        }

        public void rePosition()
        {
            int minY = 500;
            Random rnd = new Random();
            for (int i = 0; i < boardsList.BoardList.Length; i++)
            {
                if (boardsList.BoardList[i].BoardPosition.Y > 800)
                {
                    for (int j = 0; j < boardsList.BoardList.Length; j++)
                        if (boardsList.BoardList[j].BoardPosition.Y < minY) minY = boardsList.BoardList[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                        if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                        if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                    }
                    boardsList.BoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 90, minY - 40), boardsList.BoardList[i].BoardPosition.Width, boardsList.BoardList[i].BoardPosition.Height);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (boardsList.MovingBoardList[i].BoardPosition.Y > 740)
                {
                    for (int j = 0; j < boardsList.BoardList.Length; j++)
                        if (boardsList.BoardList[j].BoardPosition.Y < minY) minY = boardsList.BoardList[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                        if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                        if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                    }
                    boardsList.MovingBoardList[i].BoardPosition = new Rectangle(boardsList.MovingBoardList[i].BoardPosition.X, rnd.Next(minY - 80, minY - 20), boardsList.MovingBoardList[i].BoardPosition.Width, boardsList.MovingBoardList[i].BoardPosition.Height);
                }
                if (boardsList.FakeBoardList[i].BoardPosition.Y > 740)
                {
                    for (int j = 0; j < boardsList.BoardList.Length; j++)
                        if (boardsList.BoardList[j].BoardPosition.Y < minY) minY = boardsList.BoardList[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                        if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                        if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                    }
                    boardsList.FakeBoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 80, minY - 20), boardsList.FakeBoardList[i].BoardPosition.Width, boardsList.FakeBoardList[i].BoardPosition.Height);
                }
                if (boardsList.GoneBoardList[i].BoardPosition.Y > 740)
                {
                    for (int j = 0; j < boardsList.BoardList.Length; j++)
                        if (boardsList.BoardList[j].BoardPosition.Y < minY) minY = boardsList.BoardList[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                        if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                        if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                    }
                    boardsList.GoneBoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 80, minY - 20), boardsList.GoneBoardList[i].BoardPosition.Width, boardsList.GoneBoardList[i].BoardPosition.Height);
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

        public int gameState = 0;
        public const int introMenu = 0;
        public const int gameRunning = 1;
        public const int pause = 2;
        public const int option = 3;
        public const int gameOver = 4;
        public const int hScore = 5;

        public enum cond { Left = 1, Right, Tir, HeliL, HeliR, JetL, jetR, BargL, BargR } //kako bo obrnjena slika
        public Scoring score;
        public Player player;
        public Player playerMenu;
        public BoardsList boardsList;
        public cond dir;
        public Texture2D back1;
        public Background background;
        public Pointer pointer;
        public Bullet bullet;
        public bool tirCheck;
        public bool fCheck;
        public bool collisionCheck;
        public bool tCheck;
        public bool gameover;
        public bool meRnd;
        public bool mecolosion;
        public int fRnd;//for checking fanars randoom
        public int tRnd;//for checking fanars randoom
        public int eRnd;//for checking StaticEnemies randoom
        public int e2Rnd;//for checking BigEnemies randoom

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
            dir = cond.Right;
            tirCheck = false;
            fCheck = false;
            tCheck = false;
            collisionCheck = true;
            mecolosion = false;
            gameover = false;
            meRnd = true;
            fRnd = -1;
            tRnd = -1;
            eRnd = -1;
            e2Rnd = -1;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            boardsList = new BoardsList(this.Content);

            player = new Player(this.Content);
            playerMenu = new Player(this.Content);

            player.PlayerPosition = new Rectangle(230, 660, 60, 60);
            player.PlayerSpeed = new Vector2(0, -13);
            playerMenu.PlayerPosition = new Rectangle(100, 520, 80, 80);
            playerMenu.PlayerSpeed = new Vector2(0, -13);
            player.Degree = 0;

            background = new Background(this.Content);
            score = new Scoring(this.Content);
            pointer = new Pointer(this.Content);
            bullet = new Bullet(this.Content);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            mouseState = Mouse.GetState();
            k = Keyboard.GetState();
            pointer.PointerPosition = new Rectangle(mouseState.X - 10, mouseState.Y - 10, pointer.PointerPosition.Width, pointer.PointerPosition.Height);
            switch (gameState)
            {
                case gameRunning:
                    {
                        if (player.PlayerPosition.Y + 60 > 720) gameover = true;
                        
                        player.Move();
                        if (player.PlayerPosition.X + 10 < 0)//to prevent from exiting from sides of screen
                            player.PlayerPosition = new Rectangle(450, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                        if (player.PlayerPosition.X > 451)
                            player.PlayerPosition = new Rectangle(-10, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);

                        for (int i = 0; i < 4; i++)
                            boardsList.MovingBoardList[i].Move();

                        if (player.PlayerPosition.Y < 300) //to move boards_list and background
                        {
                            int speed = (int)player.PlayerSpeed.Y;
                            player.PlayerPosition = new Rectangle(player.PlayerPosition.X, player.PlayerPosition.Y - (int)player.PlayerSpeed.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            
                            for (int i = 0; i < boardsList.BoardList.Length; i++)
                                boardsList.BoardList[i].BoardPosition = new Rectangle(boardsList.BoardList[i].BoardPosition.X, boardsList.BoardList[i].BoardPosition.Y - speed, boardsList.BoardList[i].BoardPosition.Width, boardsList.BoardList[i].BoardPosition.Height);
  
                            for (int i = 0; i < 4; i++)
                            {
                                boardsList.MovingBoardList[i].BoardPosition = new Rectangle(boardsList.MovingBoardList[i].BoardPosition.X, boardsList.MovingBoardList[i].BoardPosition.Y - speed, boardsList.MovingBoardList[i].BoardPosition.Width, boardsList.MovingBoardList[i].BoardPosition.Height);
                                boardsList.FakeBoardList[i].BoardPosition = new Rectangle(boardsList.FakeBoardList[i].BoardPosition.X, boardsList.FakeBoardList[i].BoardPosition.Y - speed, boardsList.FakeBoardList[i].BoardPosition.Width, boardsList.FakeBoardList[i].BoardPosition.Height);
                                boardsList.GoneBoardList[i].BoardPosition = new Rectangle(boardsList.GoneBoardList[i].BoardPosition.X, boardsList.GoneBoardList[i].BoardPosition.Y - speed, boardsList.GoneBoardList[i].BoardPosition.Width, boardsList.GoneBoardList[i].BoardPosition.Height);
                            }

                            if (background.BPosize.Y < 0)
                                background.BPosize = new Rectangle(background.BPosize.X, background.BPosize.Y - (speed / 2), background.BPosize.Width, background.BPosize.Height);
                            background.SPosise1 = new Rectangle(background.SPosise1.X, background.SPosise1.Y - (speed / 2), background.SPosise1.Width, background.SPosise1.Height);
                            background.SPosise2 = new Rectangle(background.SPosise2.X, background.SPosise2.Y - (speed / 2), background.SPosise2.Width, background.SPosise2.Height);
                            score.SNevem -= speed / 2;
                        }
                        background.SideCheck();

                        rePosition();//to re position boards_list and movable enemys

                        for (int i = 0; i < boardsList.BoardList.Length; i++)//to check boards_list coliision
                            if (boardsList.BoardList[i].Collision(player) && !gameover && collisionCheck == true)
                            {
                                player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -13);
                            }
                        for (int i = 0; i < 4; i++)
                        {
                            if (boardsList.MovingBoardList[i].Collision(player) && !gameover && collisionCheck == true)
                            {
                                player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -13);
                            }
                            if (boardsList.FakeBoardList[i].Collision(player) && !gameover && collisionCheck == true)
                                boardsList.FakeBoardList[i].BoardPosition = new Rectangle(-100, boardsList.FakeBoardList[i].BoardPosition.Y, boardsList.FakeBoardList[i].BoardPosition.Width, boardsList.FakeBoardList[i].BoardPosition.Height);
                            if (boardsList.GoneBoardList[i].Collision(player) && !gameover && collisionCheck == true)
                            {
                                player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -13);
                                boardsList.GoneBoardList[i].BoardPosition = new Rectangle(-100, boardsList.GoneBoardList[i].BoardPosition.Y, boardsList.GoneBoardList[i].BoardPosition.Width, boardsList.GoneBoardList[i].BoardPosition.Height);
                            }
                        }
                        k_temp1 = Keyboard.GetState();
                        if (k_temp1.IsKeyDown(Keys.Escape) && !k_temp.IsKeyDown(Keys.Escape))//to go to pause menue bye esc clicking
                        {
                            gameState = pause;
                            break;
                        }
                        k_temp = k_temp1;
                        if (k.IsKeyDown(Keys.Left))//to move left and right
                        {
                            dir = cond.Left;
                            player.PlayerPosition = new Rectangle(player.PlayerPosition.X - 7, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                        }
                        else if (k.IsKeyDown(Keys.Right))
                        {
                            dir = cond.Right;
                            player.PlayerPosition = new Rectangle(player.PlayerPosition.X + 7, player.PlayerPosition.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                        }
                        

                        mouseState = Mouse.GetState();//check mouse state for shoot and pause menu 
                        if (mouseState.LeftButton == ButtonState.Pressed && !(m_temp.LeftButton == ButtonState.Pressed) && gameState == 1)
                        {
                            if (mouseState.X > 420 && mouseState.X < 470 && mouseState.Y > 5 && mouseState.Y < 40)
                            {
                                gameState = pause;
                                MediaPlayer.Pause();
                            }
                            //to shoot tir
                            else
                            {
                                if (!tirCheck && mouseState.Y < 280)
                                {
                                    player.Degree = (float)Math.Atan((-(mouseState.Y - player.PlayerPosition.Y - 27)) / (mouseState.X - player.PlayerPosition.X - 30));
                                    bullet.BulletPosition = new Rectangle(player.PlayerPosition.X + 30,player.PlayerPosition.Y + 27, bullet.BulletPosition.Width, bullet.BulletPosition.Height);
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
                            if (mouseState.Y < 280)
                                dir = cond.Tir;
                        }
                        if (bullet.BulletPosition.Y > 740 || bullet.BulletPosition.X < -20 || bullet.BulletPosition.X > 500 || bullet.BulletPosition.Y < -20)
                            tirCheck = false;
                        if (tirCheck && gameState == 1)
                            bullet.Move();

                        //to end and gameovering game
                        if (player.PlayerPosition.Y > 720)
                            gameState = gameOver;
                    }
                    break;

                case pause:
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.X > 248 && mouseState.X < 446)
                            if (mouseState.Y > 510 && mouseState.Y < 570)
                            {
                                gameState = gameRunning;
                                MediaPlayer.Resume();
                            }
                        if (mouseState.X > 280 && mouseState.X < 458)
                            if (mouseState.Y > 600 && mouseState.Y < 660)
                            {
                                gameState = introMenu;
                                dir = cond.Right;
                            }
                        if (mouseState.X > 170 && mouseState.X < 340)
                            if (mouseState.Y > 420 && mouseState.Y < 480)
                            {
                                gameState = option;
                                dir = cond.Right;
                            }
                    }
                    k_temp = Keyboard.GetState();
                    if (k_temp.IsKeyDown(Keys.Escape) && !k_temp1.IsKeyDown(Keys.Escape))
                        gameState = gameRunning;
                    k_temp1 = k_temp;
                    background.GameStateCheck = false;
                    break;
                case option:
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.X > 80 && mouseState.X < 240)
                            if (mouseState.Y > 592 && mouseState.Y < 652)
                                if (background.GameStateCheck == true)
                                    gameState = introMenu;
                                else
                                    gameState = pause;

                        if (mouseState.X > 100 && mouseState.X < 160)
                            if (mouseState.Y > 330 && mouseState.Y < 375)
                                background.SoundCheck = false;
                        if (mouseState.X > 160 && mouseState.X < 236)
                            if (mouseState.Y > 330 && mouseState.Y < 375)
                                background.SoundCheck = true;
                    }
                    break;
                case introMenu:
                    mouseState = Mouse.GetState();
                    if (mouseState.LeftButton == ButtonState.Pressed && !(m_temp1.LeftButton == ButtonState.Pressed))
                    {
                        background.GameStateCheck = true;
                        if (mouseState.X > 68 && mouseState.X < 240)
                            if (mouseState.Y > 210 && mouseState.Y < 270)
                            {
                                mouseState = m_temp;
                                gameState = gameRunning;
                                MediaPlayer.Resume();
                                PlayAgain(player, score, background, ref gameState);
                            }
                        if (mouseState.X > 274 && mouseState.X < 446)
                            if (mouseState.Y > 510 && mouseState.Y < 570 && gameState == introMenu)
                                this.Exit();
                        if (mouseState.X > 240 && mouseState.X < 412)
                            if (mouseState.Y > 415 && mouseState.Y < 475)
                                gameState = option;
                        if (mouseState.X > 200 && mouseState.X < 365)
                            if (mouseState.Y > 330 && mouseState.Y < 395)
                                gameState = hScore;
                    }
                    playerMenu.Move();
                    if (playerMenu.PlayerPosition.Y > 550)
                        playerMenu.PlayerSpeed = new Vector2(playerMenu.PlayerSpeed.X, -13);
                    break;
                case hScore:
                    mouseState = Mouse.GetState();
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.X > 295 && mouseState.X < 460)
                            if (mouseState.Y > 600 && mouseState.Y < 660)
                                gameState = introMenu;
                    }
                    break;
                case gameOver:
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.X > 110 && mouseState.X < 272)
                            if (mouseState.Y > 467 && mouseState.Y < 535)
                                PlayAgain(player, score, background, ref gameState);
                        if (mouseState.X > 240 && mouseState.X < 416)
                            if (mouseState.Y > 522 && mouseState.Y < 612)
                            {
                                gameState = introMenu;
                                MediaPlayer.Pause();
                                dir = cond.Right;

                            }
                        mouseState = m_temp1;
                    }
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            background.Draw(spriteBatch, gameState, score);

            if (gameState == gameRunning)
            {
                for (int i = 0; i < boardsList.BoardList.Length; i++)
                {
                    boardsList.BoardList[i].DrawSprite(spriteBatch);
                }

                for (int i = 0; i < 4; i++)
                {
                    boardsList.MovingBoardList[i].DrawSprite(spriteBatch);
                    boardsList.FakeBoardList[i].DrawSprite(spriteBatch);
                    boardsList.GoneBoardList[i].DrawSprite(spriteBatch);
                }

                player.Draw(spriteBatch, ref dir, gameState);
            }
            if (gameState == introMenu) 
            {
                playerMenu.Draw(spriteBatch, ref dir, 1);
            }
            bullet.Draw(spriteBatch, gameState);
            background.Notifdraw(spriteBatch, gameState);
            score.Draw(spriteBatch, gameState);
            pointer.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}