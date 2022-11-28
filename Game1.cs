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

namespace VirusJump
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //playagain gre v renderer
        public void PlayAgain(Player player, Scoring score, FakeBoard[] fakeBoards, GoneBoard[] goneBoards, MovingBoard[] movingBoards, Background background, ref int gameState)
        {

            gameState = gameRunning;
            collisionCheck = true;
            score.Check = true;
            gameover = false;
            player.PlayerPosition = new Rectangle(230, 560, 60, 60);
            boards_arr[0].BoardPosition = new Rectangle(40, 700, 60, 14);
            boards_arr[1].BoardPosition = new Rectangle(240, 640, 60, 14);
            boards_arr[2].BoardPosition = new Rectangle(400, 620, 60, 14);
            boards_arr[3].BoardPosition = new Rectangle(250, 600, 60, 14);
            boards_arr[4].BoardPosition = new Rectangle(290, 570, 60, 14);
            boards_arr[5].BoardPosition = new Rectangle(240, 550, 60, 14);
            boards_arr[6].BoardPosition = new Rectangle(200, 580, 60, 14);
            boards_arr[7].BoardPosition = new Rectangle(160, 530, 60, 14);
            boards_arr[8].BoardPosition = new Rectangle(100, 500, 60, 14);
            boards_arr[9].BoardPosition = new Rectangle(180, 460, 60, 14);
            boards_arr[10].BoardPosition = new Rectangle(270, 430, 60, 14);
            boards_arr[11].BoardPosition = new Rectangle(310, 390, 60, 14);
            boards_arr[12].BoardPosition = new Rectangle(350, 360, 60, 14);
            boards_arr[13].BoardPosition = new Rectangle(310, 320, 60, 14);
            boards_arr[14].BoardPosition = new Rectangle(310, 290, 60, 14);
            boards_arr[15].BoardPosition = new Rectangle(200, 250, 60, 14);
            boards_arr[16].BoardPosition = new Rectangle(160, 220, 60, 14);
            boards_arr[17].BoardPosition = new Rectangle(140, 180, 60, 14);
            boards_arr[18].BoardPosition = new Rectangle(400, 140, 60, 14);
            boards_arr[19].BoardPosition = new Rectangle(360, 110, 60, 14);
            boards_arr[20].BoardPosition = new Rectangle(300, 70, 60, 14);
            boards_arr[21].BoardPosition = new Rectangle(240, 40, 60, 14);
            movingBoards[0].BoardPosition = new Rectangle(100, 335, 60, 14);
            movingBoards[1].BoardPosition = new Rectangle(250, 15, 60, 14);
            movingBoards[2].BoardPosition = new Rectangle(0, -20, 60, 14);
            movingBoards[3].BoardPosition = new Rectangle(400, 630, 60, 14);
            fakeBoards[0].BoardPosition = new Rectangle(420, 435, 60, 14);
            fakeBoards[1].BoardPosition = new Rectangle(50, 120, 60, 14);
            fakeBoards[2].BoardPosition = new Rectangle(20, -35, 60, 14);
            fakeBoards[3].BoardPosition = new Rectangle(250, 525, 60, 14);
            goneBoards[0].BoardPosition = new Rectangle(100, 263, 60, 14);
            goneBoards[1].BoardPosition = new Rectangle(30, 330, 60, 14);
            goneBoards[2].BoardPosition = new Rectangle(410, -50, 60, 14);
            goneBoards[3].BoardPosition = new Rectangle(74, 660, 60, 14);
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

        public void rePosition(Board[] boards_arr, MovingBoard[] movingBoards, FakeBoard[] fakeBoards, GoneBoard[] goneBoards)
        {
            int minY = 500;
            Random rnd = new Random();
            for (int i = 0; i < boards_arr.Length; i++)
            {
                if (boards_arr[i].BoardPosition.Y > 800)
                {
                    for (int j = 0; j < boards_arr.Length; j++)
                        if (boards_arr[j].BoardPosition.Y < minY) minY = boards_arr[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (movingBoards[j].BoardPosition.Y < minY) minY = movingBoards[j].BoardPosition.Y;
                        if (fakeBoards[j].BoardPosition.Y < minY) minY = fakeBoards[j].BoardPosition.Y;
                        if (goneBoards[j].BoardPosition.Y < minY) minY = goneBoards[j].BoardPosition.Y;
                    }
                    boards_arr[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 90, minY - 40), boards_arr[i].BoardPosition.Width, boards_arr[i].BoardPosition.Height);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (movingBoards[i].BoardPosition.Y > 740)
                {
                    for (int j = 0; j < boards_arr.Length; j++)
                        if (boards_arr[j].BoardPosition.Y < minY) minY = boards_arr[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (movingBoards[j].BoardPosition.Y < minY) minY = movingBoards[j].BoardPosition.Y;
                        if (fakeBoards[j].BoardPosition.Y < minY) minY = fakeBoards[j].BoardPosition.Y;
                        if (goneBoards[j].BoardPosition.Y < minY) minY = goneBoards[j].BoardPosition.Y;
                    }
                    movingBoards[i].BoardPosition = new Rectangle(movingBoards[i].BoardPosition.X, rnd.Next(minY - 80, minY - 20), movingBoards[i].BoardPosition.Width, movingBoards[i].BoardPosition.Height);
                }
                if (fakeBoards[i].BoardPosition.Y > 740)
                {
                    for (int j = 0; j < boards_arr.Length; j++)
                        if (boards_arr[j].BoardPosition.Y < minY) minY = boards_arr[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (movingBoards[j].BoardPosition.Y < minY) minY = movingBoards[j].BoardPosition.Y;
                        if (fakeBoards[j].BoardPosition.Y < minY) minY = fakeBoards[j].BoardPosition.Y;
                        if (goneBoards[j].BoardPosition.Y < minY) minY = goneBoards[j].BoardPosition.Y;
                    }
                    fakeBoards[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 80, minY - 20), fakeBoards[i].BoardPosition.Width, fakeBoards[i].BoardPosition.Height);
                }
                if (goneBoards[i].BoardPosition.Y > 740)
                {
                    for (int j = 0; j < boards_arr.Length; j++)
                        if (boards_arr[j].BoardPosition.Y < minY) minY = boards_arr[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (movingBoards[j].BoardPosition.Y < minY) minY = movingBoards[j].BoardPosition.Y;
                        if (fakeBoards[j].BoardPosition.Y < minY) minY = fakeBoards[j].BoardPosition.Y;
                        if (goneBoards[j].BoardPosition.Y < minY) minY = goneBoards[j].BoardPosition.Y;
                    }

                    //popravi
                    goneBoards[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 80, minY - 20), goneBoards[i].BoardPosition.Width, goneBoards[i].BoardPosition.Height);
                }
            }

        }

        //tipkovnica in miska
        public KeyboardState k;
        public KeyboardState k_temp;
        public KeyboardState k_temp1;
        public MouseState m;
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
        public Board[] boards_arr = new Board[22];
        public MovingBoard[] movingBoards = new MovingBoard[4];
        public FakeBoard[] fakeBoards = new FakeBoard[4];
        public GoneBoard[] goneBoards = new GoneBoard[4];
        public cond dir;
        public Texture2D back1;
        public Background background;
        public Pointer pointer;
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
            for (int i = 0; i < boards_arr.Length; i++)
            {
                boards_arr[i] = new Board(this.Content);
            }
            for (int i = 0; i < fakeBoards.Length; i++)
            {
                fakeBoards[i] = new FakeBoard(this.Content);
                goneBoards[i] = new GoneBoard(this.Content);
                movingBoards[i] = new MovingBoard(this.Content);
            }
            movingBoards[0].Speed = 4;
            movingBoards[1].Speed = -4;
            movingBoards[2].Speed = 3;
            movingBoards[3].Speed = 2;

            player = new Player(this.Content);
            playerMenu = new Player(this.Content);

            player.PlayerPosition = new Rectangle(230, 660, 60, 60);
            player.PlayerSpeed = new Vector2(0, -13);
            playerMenu.PlayerPosition = new Rectangle(100, 520, 80, 80);
            playerMenu.PlayerSpeed = new Vector2(0, -13);
            playerMenu.Ch = 0;
            player.Degree = 0;
            player.Ch = 0;

            background = new Background(this.Content);
            background.BPosize = new Rectangle(0, -6480, 480, 7200);
            background.KPosize = new Rectangle(0, 0, 480, 720);
            background.IntroMenuPosize = new Rectangle(0, 0, 480, 720);
            background.OptionPosize = new Rectangle(0, 0, 480, 720);
            background.SOnPosize = new Rectangle(100, 330, 136, 45);
            background.SOffPosize = new Rectangle(100, 330, 136, 45);
            background.NotifPosize = new Rectangle(0, 0, 480, 60);
            background.PausePosize = new Rectangle(0, 0, 480, 720);
            background.GameOverPosize = new Rectangle(0, 0, 480, 720);
            background.HScorePosize = new Rectangle(0, 0, 480, 720);
            background.SoundCheck = true;
            background.GameStateCheck = true;

            score = new Scoring(this.Content);
            score.ScoringPosition = new Vector2(15f, 4f);
            score.SNevem = 0;
            score.BestS = "";
            score.Check = true;

            pointer = new Pointer(this.Content);
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            m = Mouse.GetState();
            k = Keyboard.GetState();
            pointer.PointerPosition = new Rectangle(m.X - 10, m.Y - 10, pointer.PointerPosition.Width, pointer.PointerPosition.Height);
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
                            movingBoards[i].Move();

                        if (player.PlayerPosition.Y < 300) //to move boards_list and background
                        {
                            int speed = (int)player.PlayerSpeed.Y;
                            player.PlayerPosition = new Rectangle(player.PlayerPosition.X, player.PlayerPosition.Y - (int)player.PlayerSpeed.Y, player.PlayerPosition.Width, player.PlayerPosition.Height);
                            Board[] temp = boards_arr.ToArray();
                            for (int i = 0; i < boards_arr.Length; i++)
                                temp[i].BoardPosition = new Rectangle(temp[i].BoardPosition.X, temp[i].BoardPosition.Y - speed, temp[i].BoardPosition.Width, temp[i].BoardPosition.Height);
                            
                            boards_arr = temp.ToArray();
                            for (int i = 0; i < 4; i++)
                            {
                                movingBoards[i].BoardPosition = new Rectangle(movingBoards[i].BoardPosition.X, movingBoards[i].BoardPosition.Y - speed, movingBoards[i].BoardPosition.Width, movingBoards[i].BoardPosition.Height);
                                fakeBoards[i].BoardPosition = new Rectangle(fakeBoards[i].BoardPosition.X, fakeBoards[i].BoardPosition.Y - speed, fakeBoards[i].BoardPosition.Width, fakeBoards[i].BoardPosition.Height);
                                goneBoards[i].BoardPosition = new Rectangle(goneBoards[i].BoardPosition.X, goneBoards[i].BoardPosition.Y - speed, goneBoards[i].BoardPosition.Width, goneBoards[i].BoardPosition.Height);
                            }

                            if (background.BPosize.Y < 0)
                                background.BPosize = new Rectangle(background.BPosize.X, background.BPosize.Y - (speed / 2), background.BPosize.Width, background.BPosize.Height);
                            background.SPosise1 = new Rectangle(background.SPosise1.X, background.SPosise1.Y - (speed / 2), background.SPosise1.Width, background.SPosise1.Height);
                            background.SPosise1 = new Rectangle(background.SPosise1.X, background.SPosise1.Y - (speed / 2), background.SPosise1.Width, background.SPosise1.Height);
                            score.SNevem -= speed / 2;
                        }
                        background.SideCheck();

                        rePosition(boards_arr, movingBoards, fakeBoards, goneBoards);//to re position boards_list and movable enemys

                        for (int i = 0; i < boards_arr.Length; i++)//to check boards_list coliision
                            if (boards_arr[i].Collision(player) && !gameover && collisionCheck == true)
                            {
                                player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -13);
                            }
                        for (int i = 0; i < 4; i++)
                        {
                            if (movingBoards[i].Collision(player) && !gameover && collisionCheck == true)
                            {
                                player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -13);
                            }
                            if (fakeBoards[i].Collision(player) && !gameover && collisionCheck == true)
                                fakeBoards[i].BoardPosition = new Rectangle(-100, fakeBoards[i].BoardPosition.Y, fakeBoards[i].BoardPosition.Width, fakeBoards[i].BoardPosition.Height);
                            if (goneBoards[i].Collision(player) && !gameover && collisionCheck == true)
                            {
                                player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, -13);
                                goneBoards[i].BoardPosition = new Rectangle(-100, goneBoards[i].BoardPosition.Y, goneBoards[i].BoardPosition.Width, goneBoards[i].BoardPosition.Height);
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
                        

                        m = Mouse.GetState();//check mouse state for shoot and pause menu 
                        if (m.LeftButton == ButtonState.Pressed && !(m_temp.LeftButton == ButtonState.Pressed))
                        {
                            if (m.X > 420 && m.X < 470 && m.Y > 5 && m.Y < 40)
                            {
                                gameState = pause;
                                MediaPlayer.Pause();
                            }
                            else //to shoot tir
                            {

                                if (!tirCheck && m.Y < 280)
                                {
                                    player.Degree = (float)Math.Atan((-(m.Y - player.PlayerPosition.Y - 27)) / (m.X - player.PlayerPosition.X - 30));
                                    tirCheck = true;
                                }
                            }
                            if (m.Y < 280)
                                dir = cond.Tir;
                        }
                        if (player.PlayerPosition.Y > 720)//to end and gameovering game
                            gameState = gameOver;
                    }
                    break;

                case pause:
                    if (m.LeftButton == ButtonState.Pressed)
                    {
                        if (m.X > 248 && m.X < 446)
                            if (m.Y > 510 && m.Y < 570)
                            {
                                gameState = gameRunning;
                                MediaPlayer.Resume();
                            }
                        if (m.X > 280 && m.X < 458)
                            if (m.Y > 600 && m.Y < 660)
                            {
                                gameState = introMenu;
                                dir = cond.Right;
                            }
                        if (m.X > 170 && m.X < 340)
                            if (m.Y > 420 && m.Y < 480)
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
                    if (m.LeftButton == ButtonState.Pressed)
                    {
                        if (m.X > 80 && m.X < 240)
                            if (m.Y > 592 && m.Y < 652)
                                if (background.GameStateCheck == true)
                                    gameState = introMenu;
                                else
                                    gameState = pause;

                        if (m.X > 100 && m.X < 160)
                            if (m.Y > 330 && m.Y < 375)
                                background.SoundCheck = false;
                        if (m.X > 160 && m.X < 236)
                            if (m.Y > 330 && m.Y < 375)
                                background.SoundCheck = true;
                    }
                    break;
                case introMenu:
                    m = Mouse.GetState();
                    if (m.LeftButton == ButtonState.Pressed && !(m_temp1.LeftButton == ButtonState.Pressed))
                    {
                        background.GameStateCheck = true;
                        if (m.X > 68 && m.X < 240)
                            if (m.Y > 210 && m.Y < 270)
                            {
                                m = m_temp;
                                gameState = gameRunning;
                                MediaPlayer.Resume();
                                PlayAgain(player, score, fakeBoards, goneBoards, movingBoards, background, ref gameState);
                            }
                        if (m.X > 274 && m.X < 446)
                            if (m.Y > 510 && m.Y < 570 && gameState == introMenu)
                                this.Exit();
                        if (m.X > 240 && m.X < 412)
                            if (m.Y > 415 && m.Y < 475)
                                gameState = option;
                        if (m.X > 200 && m.X < 365)
                            if (m.Y > 330 && m.Y < 395)
                                gameState = hScore;
                    }
                    playerMenu.Move();
                    if (playerMenu.PlayerPosition.Y > 550)
                        playerMenu.PlayerSpeed = new Vector2(playerMenu.PlayerSpeed.X, -13);
                    break;
                case hScore:
                    m = Mouse.GetState();
                    if (m.LeftButton == ButtonState.Pressed)
                    {
                        if (m.X > 295 && m.X < 460)
                            if (m.Y > 600 && m.Y < 660)
                                gameState = introMenu;
                    }
                    break;
                case gameOver:
                    if (m.LeftButton == ButtonState.Pressed)
                    {
                        if (m.X > 110 && m.X < 272)
                            if (m.Y > 467 && m.Y < 535)
                                PlayAgain(player, score, fakeBoards, goneBoards, movingBoards, background, ref gameState);
                        if (m.X > 240 && m.X < 416)
                            if (m.Y > 522 && m.Y < 612)
                            {
                                gameState = introMenu;
                                MediaPlayer.Pause();
                                dir = cond.Right;

                            }
                        m = m_temp1;
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
                for (int i = 0; i < boards_arr.Length; i++)
                {
                    boards_arr[i].DrawSprite(spriteBatch);
                }

                for (int i = 0; i < 4; i++)
                {
                    movingBoards[i].DrawSprite(spriteBatch);
                    fakeBoards[i].DrawSprite(spriteBatch);
                    goneBoards[i].DrawSprite(spriteBatch);
                }

                player.Draw(spriteBatch, ref dir, gameState);
            }
            if (gameState == introMenu)
                playerMenu.Draw(spriteBatch, ref dir, 1);
            background.Notifdraw(spriteBatch, gameState);
            score.Draw(spriteBatch, gameState);
            pointer.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}