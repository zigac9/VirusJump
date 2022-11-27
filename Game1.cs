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

namespace VirusJump
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public struct fakeRect
        {
            public Texture2D texture;
            public Rectangle posize;
            public void draw(SpriteBatch s)
            {
                s.Draw(texture, posize, Color.White);
            }

            public bool Collision(doodle s)
            {
                if ((s.posize.X + 15 > posize.X && s.posize.X + 15 < posize.X + 60) || (s.posize.X + 45 > posize.X && s.posize.X + 45 < posize.X + 60))

                    if (posize.Y - s.posize.Y - 60 < 5 && posize.Y - s.posize.Y - 60 > -20 && s.speed.Y > 0)
                        return true;
                    else return false;
                else return false;
            }
        }

        public struct goneRect
        {
            public Texture2D texture;
            public Rectangle posize;
            public void draw(SpriteBatch s)
            {
                s.Draw(texture, posize, Color.White);
            }

            public bool Collision(doodle s)
            {
                if ((s.posize.X + 15 > posize.X && s.posize.X + 15 < posize.X + 60) || (s.posize.X + 45 > posize.X && s.posize.X + 45 < posize.X + 60))

                    if (posize.Y - s.posize.Y - 60 < 5 && posize.Y - s.posize.Y - 60 > -20 && s.speed.Y > 0)
                        return true;
                    else return false;
                else return false;
            }
        }

        public struct movingRect
        {
            public Texture2D texture;
            public Rectangle posize;
            public int speed;
            public void draw(SpriteBatch s)
            {
                s.Draw(texture, posize, Color.White);
            }

            public void move()
            {
                posize.X += speed;
                if (posize.X > 420 || posize.X < 0) speed *= -1;
            }

            public bool Collision(doodle s)
            {
                if ((s.posize.X + 15 > posize.X && s.posize.X + 15 < posize.X + 60) || (s.posize.X + 45 > posize.X && s.posize.X + 45 < posize.X + 60))

                    if (posize.Y - s.posize.Y - 60 < 5 && posize.Y - s.posize.Y - 60 > -20 && s.speed.Y > 0)
                        return true;
                    else return false;
                else return false;
            }
        }

        public struct doodle
        {
            public Texture2D textureR;
            public Texture2D textureL;
            public Texture2D textureC;
            public Texture2D nose;
            public Texture2D test;
            public Rectangle posize;
            public Vector2 speed;
            public const int accelarator = +1;
            public int ch;
            public float degree;


            public void move()
            {
                if (ch == 0)
                {
                    speed.Y += accelarator;
                    ch = 1;
                }
                else
                    ch = 0;

                posize.Y += (int)speed.Y;
            }

            public void draw(SpriteBatch s, ref cond name, MouseState m, int game)
            {
                if (game == gameRunning)
                    switch (name)
                    {
                        case cond.Left: s.Draw(textureL, posize, Color.White); break;
                        case cond.Right: s.Draw(textureR, posize, Color.White); break;
                        case cond.Tir:
                            s.Draw(textureC, posize, Color.White);
                            s.Draw(nose, posize, Color.White);
                            name = cond.Left;
                            break;

                    }
            }
        }

        public struct fanar
        {
            public Texture2D fanarC;
            public Texture2D fanarO;
            public Rectangle posize;

            public void draw(SpriteBatch s, bool check)
            {
                if (!check)
                    s.Draw(fanarC, posize, Color.White);
                else
                    s.Draw(fanarO, posize, Color.White);
            }

            public bool Collision(doodle s, bool collisionCheck)
            {
                if ((s.posize.X + 10 > posize.X && s.posize.X + 10 < posize.X + 60) || (s.posize.X + 50 > posize.X && s.posize.X + 50 < posize.X + 60))
                    if (posize.Y + 17 - s.posize.Y - 60 < 5 && posize.Y + 17 - s.posize.Y - 60 > -15 && s.speed.Y > 0)
                    {
                        if (collisionCheck == true)
                            return true;
                        else
                            return false;
                    }
                    else return false;
                else return false;
            }
        }

        public struct background
        {
            public Texture2D back;
            public Texture2D kooh;
            public Texture2D sides;
            public Texture2D introMenu;
            public Texture2D option;
            public Texture2D notif;
            public Texture2D pause;
            public Texture2D sOn;
            public Texture2D sOff;
            public Texture2D gameOvre;
            public Texture2D hScore;
            public Rectangle bPosize;
            public Rectangle kPosize;
            public Rectangle sPosise1;
            public Rectangle sPosise2;
            public Rectangle introMenuposize;
            public Rectangle hScoreposize;
            public Rectangle optionposize;
            public Rectangle notifposize;
            public Rectangle pauseposize;
            public Rectangle gameOverposize;
            public Rectangle sOnposize;
            public Rectangle sOffposize;
            public int hScore1;
            public int hScore2;
            public int hScore3;
            public int hScore4;
            public int hScore5;
            public bool soundCheck;
            public bool gameStateCheck;

            public void draw(SpriteBatch s, int game, scor score)
            {
                s.Draw(back, bPosize, Color.White);
                s.Draw(kooh, kPosize, Color.White);
                s.Draw(sides, sPosise1, Color.White);
                s.Draw(sides, sPosise2, Color.White);
                if (game == 0)
                    s.Draw(introMenu, introMenuposize, Color.White);
                if (game == 2)
                    s.Draw(pause, pauseposize, Color.White);
                if (game == 3)
                {
                    s.Draw(option, optionposize, Color.White);
                    if (soundCheck == true)
                        s.Draw(sOn, sOnposize, Color.White);
                    else
                        s.Draw(sOff, sOffposize, Color.White);
                }
                if (game == 4)
                {
                    s.Draw(gameOvre, gameOverposize, Color.White);
                    s.DrawString(score.spFont, score.s.ToString(), new Vector2(308f, 245f), Color.Black);
                    s.DrawString(score.spFont, score.bestS, new Vector2(295f, 297f), Color.Black);
                }
                if (game == 5)
                {
                    s.Draw(hScore, hScoreposize, Color.White);
                    s.DrawString(score.spFont, hScore1.ToString(), new Vector2(215f, 245f), Color.Black);
                    s.DrawString(score.spFont, hScore2.ToString(), new Vector2(215f, 290f), Color.Black);
                    s.DrawString(score.spFont, hScore3.ToString(), new Vector2(215f, 335f), Color.Black);
                    s.DrawString(score.spFont, hScore4.ToString(), new Vector2(215f, 380f), Color.Black);
                    s.DrawString(score.spFont, hScore5.ToString(), new Vector2(215f, 420f), Color.Black);
                }
            }
            public void notifdraw(SpriteBatch s, int game)
            {
                if (game != 0 && game != 3 && game != 5)
                    s.Draw(notif, notifposize, Color.White);
            }

            public void sideCheck()
            {
                if (sPosise1.Y > 720)
                    sPosise1.Y = sPosise2.Y - 3600;
                if (sPosise2.Y > 720)
                    sPosise2.Y = sPosise1.Y - 3600;
            }
        }

        public void PlayAgain(ref doodle Doodle, ref scor score, ref fakeRect[] fr, ref goneRect[] gr, ref movingRect[] mr, ref fanar Fanar, ref background Backgraound, ref int gameState)
        {

            gameState = gameRunning;
            collisionCheck = true;
            score.check = true;
            gameover = false;
            Doodle.posize = new Rectangle(230, 560, 60, 60);
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
            mr[0].posize = new Rectangle(100, 335, 60, 14);
            mr[1].posize = new Rectangle(250, 15, 60, 14);
            mr[2].posize = new Rectangle(0, -20, 60, 14);
            mr[3].posize = new Rectangle(400, 630, 60, 14);
            fr[0].posize = new Rectangle(420, 435, 60, 14);
            fr[1].posize = new Rectangle(50, 120, 60, 14);
            fr[2].posize = new Rectangle(20, -35, 60, 14);
            fr[3].posize = new Rectangle(250, 525, 60, 14);
            gr[0].posize = new Rectangle(100, 263, 60, 14);
            gr[1].posize = new Rectangle(30, 330, 60, 14);
            gr[2].posize = new Rectangle(410, -50, 60, 14);
            gr[3].posize = new Rectangle(74, 660, 60, 14);
            Fanar.posize = new Rectangle(-100, 730, 20, 30);
            Backgraound.bPosize = new Rectangle(0, -6480, 480, 7200);
            Backgraound.kPosize = new Rectangle(0, 0, 480, 720);
            Backgraound.sPosise1 = new Rectangle(0, -2880, 480, 3600);
            Backgraound.sPosise2 = new Rectangle(0, -6480, 480, 3600);
            Backgraound.bPosize.Y = -7200 + 720;
            dir = cond.Right;
            score.s = 0;
            fRnd = -1;
            tRnd = -1;
            eRnd = -1;
            Backgraound.soundCheck = true;
            Backgraound.gameStateCheck = true;
            meRnd = true;
            mecolosion = false;

        }

        public struct scor
        {
            public int s;
            public string bestS;
            public Vector2 pos;
            public SpriteFont spFont;
            public bool check;
            public void draw(SpriteBatch sp, int game)
            {
                if (game != 0 && game != 3 && game != 5)
                    sp.DrawString(spFont, s.ToString(), pos, Color.White);
            }

        }

        public struct pointer
        {
            public Rectangle posize;
            public Texture2D texture;
            public void draw(SpriteBatch s)
            {
                s.Draw(texture, posize, Color.White);
            }

        }

        public void rePosition(Board[] boards_arr, ref movingRect[] mr, ref fakeRect[] fr, ref goneRect[] gr)
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
                        if (mr[j].posize.Y < minY) minY = mr[j].posize.Y;
                        if (fr[j].posize.Y < minY) minY = fr[j].posize.Y;
                        if (gr[j].posize.Y < minY) minY = gr[j].posize.Y;
                    }
                    boards_arr[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 90, minY - 40), boards_arr[i].BoardPosition.Width, boards_arr[i].BoardPosition.Height);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (mr[i].posize.Y > 740)
                {
                    for (int j = 0; j < boards_arr.Length; j++)
                        if (boards_arr[j].BoardPosition.Y < minY) minY = boards_arr[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (mr[j].posize.Y < minY) minY = mr[j].posize.Y;
                        if (fr[j].posize.Y < minY) minY = fr[j].posize.Y;
                        if (gr[j].posize.Y < minY) minY = gr[j].posize.Y;
                    }
                    
                    mr[i].posize.Y = rnd.Next(minY - 80, minY - 20);
                }
                if (fr[i].posize.Y > 740)
                {
                    for (int j = 0; j < boards_arr.Length; j++)
                        if (boards_arr[j].BoardPosition.Y < minY) minY = boards_arr[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (mr[j].posize.Y < minY) minY = mr[j].posize.Y;
                        if (fr[j].posize.Y < minY) minY = fr[j].posize.Y;
                        if (gr[j].posize.Y < minY) minY = gr[j].posize.Y;
                    }
                    
                    fr[i].posize.Y = rnd.Next(minY - 80, minY - 20);
                    fr[i].posize.X = rnd.Next(0, 420);
                }
                if (gr[i].posize.Y > 740)
                {
                    for (int j = 0; j < boards_arr.Length; j++)
                        if (boards_arr[j].BoardPosition.Y < minY) minY = boards_arr[j].BoardPosition.Y;
                    for (int j = 0; j < 4; j++)
                    {
                        if (mr[j].posize.Y < minY) minY = mr[j].posize.Y;
                        if (fr[j].posize.Y < minY) minY = fr[j].posize.Y;
                        if (gr[j].posize.Y < minY) minY = gr[j].posize.Y;
                    }
                    
                    gr[i].posize.Y = rnd.Next(minY - 80, minY - 20);
                    gr[i].posize.X = rnd.Next(0, 420);
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
        public scor score;
        public doodle Doodle;
        public doodle menuDoodle;
        public Board[] boards_arr = new Board[22];
        public movingRect[] mr = new movingRect[4];
        public fakeRect[] fr = new fakeRect[4];
        public goneRect[] gr = new goneRect[4];
        public cond dir;
        public Texture2D back1;
        public background Backgraound;
        public pointer mPoint;
        public fanar Fanar;
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

            Doodle.posize = new Rectangle(230, 660, 60, 60);
            Doodle.speed = new Vector2(0, -13);
            menuDoodle.posize = new Rectangle(100, 520, 80, 80);
            menuDoodle.speed = new Vector2(0, -13);
            menuDoodle.ch = 0;
            Backgraound.bPosize = new Rectangle(0, -6480, 480, 7200);
            Backgraound.kPosize = new Rectangle(0, 0, 480, 720);
            Backgraound.introMenuposize = new Rectangle(0, 0, 480, 720);
            Backgraound.optionposize = new Rectangle(0, 0, 480, 720);
            Backgraound.sOnposize = new Rectangle(100, 330, 136, 45);
            Backgraound.sOffposize = new Rectangle(100, 330, 136, 45);
            Backgraound.notifposize = new Rectangle(0, 0, 480, 60);
            Backgraound.pauseposize = new Rectangle(0, 0, 480, 720);
            Backgraound.gameOverposize = new Rectangle(0, 0, 480, 720);
            Backgraound.hScoreposize = new Rectangle(0, 0, 480, 720);
            mPoint.posize = new Rectangle(0, 0, 20, 20);
            score.pos = new Vector2(15f, 4f);
            Fanar.posize = new Rectangle(-100, 730, 20, 30);

        }


        protected override void Initialize()
        {
            Doodle.degree = 0;
            Doodle.ch = 0;
            dir = cond.Right;
            score.s = 0;
            score.bestS = "";
            tirCheck = false;
            fCheck = false;
            tCheck = false;
            collisionCheck = true;
            score.check = true;
            Backgraound.soundCheck = true;
            Backgraound.gameStateCheck = true;
            mecolosion = false;
            gameover = false;
            meRnd = true;
            fRnd = -1;
            tRnd = -1;
            eRnd = -1;
            e2Rnd = -1;
            mr[0].speed = 4;
            mr[1].speed = -4;
            mr[2].speed = 3;
            mr[3].speed = 2;

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            for (int i = 0; i < boards_arr.Length; i++)
            {
                boards_arr[i] = new Board(this.Content);
            }
            
            for (int i = 0; i < 4; i++)
            {
                mr[i].texture = Content.Load<Texture2D>("Doodle_jumpContent/p2");
                fr[i].texture = Content.Load<Texture2D>("Doodle_jumpContent/p3");
                gr[i].texture = Content.Load<Texture2D>("Doodle_jumpContent/p4");
            }
            Doodle.test = Content.Load<Texture2D>("Doodle_jumpContent/test");
            Doodle.textureR = Content.Load<Texture2D>("Doodle_jumpContent/DoodleR1");
            menuDoodle.textureR = Content.Load<Texture2D>("Doodle_jumpContent/DoodleR1");
            Doodle.textureL = Content.Load<Texture2D>("Doodle_jumpContent/DoodleL1");
            Backgraound.back = Content.Load<Texture2D>("Doodle_jumpContent/gradient");
            Backgraound.kooh = Content.Load<Texture2D>("Doodle_jumpContent/kooh");
            Backgraound.introMenu = Content.Load<Texture2D>("Doodle_jumpContent/mainMenu");
            Backgraound.option = Content.Load<Texture2D>("Doodle_jumpContent/option");
            Backgraound.sOn = Content.Load<Texture2D>("Doodle_jumpContent/sOn");
            Backgraound.sOff = Content.Load<Texture2D>("Doodle_jumpContent/sOff");
            Backgraound.notif = Content.Load<Texture2D>("Doodle_jumpContent/notif");
            Backgraound.pause = Content.Load<Texture2D>("Doodle_jumpContent/puase");
            Backgraound.sides = Content.Load<Texture2D>("Doodle_jumpContent/sides");
            Backgraound.gameOvre = Content.Load<Texture2D>("Doodle_jumpContent/gameOver");
            Backgraound.hScore = Content.Load<Texture2D>("Doodle_jumpContent/Hscore");
            Doodle.nose = Content.Load<Texture2D>("Doodle_jumpContent/DoodleKH");
            Doodle.textureC = Content.Load<Texture2D>("Doodle_jumpContent/DoodleT");
            mPoint.texture = Content.Load<Texture2D>("Doodle_jumpContent/pointer");
            score.spFont = Content.Load<SpriteFont>("Doodle_jumpContent/SpriteFont1");
            Fanar.fanarC = Content.Load<Texture2D>("Doodle_jumpContent/fanar");
            Fanar.fanarO = Content.Load<Texture2D>("Doodle_jumpContent/oFanar");

        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            m = Mouse.GetState();
            k = Keyboard.GetState();
            mPoint.posize.X = m.X - 10;
            mPoint.posize.Y = m.Y - 10;
            switch (gameState)
            {
                case gameRunning:
                    {
                        if (Doodle.posize.Y + 60 > 720) gameover = true;
                        
                        Doodle.move();
                        if (Doodle.posize.X + 10 < 0)//to prevent from exiting from sides of screen
                            Doodle.posize.X = 450;
                        if (Doodle.posize.X > 451)
                            Doodle.posize.X = -10;

                        for (int i = 0; i < 4; i++)
                            mr[i].move();


                        if (score.s % 130 > 100 && Fanar.posize.Y > 720)//to move and replace fanars
                        {
                            do
                            {
                                Random rnd = new Random();
                                fRnd = rnd.Next(0, boards_arr.Length - 1);
                            } while (!(boards_arr[fRnd].BoardPosition.Y < 0) && eRnd != fRnd);
                        }
                        if (fRnd != -1)
                        {
                            Fanar.posize.Y = boards_arr[fRnd].BoardPosition.Y - 30;
                            Fanar.posize.X = boards_arr[fRnd].BoardPosition.X + 10;
                        }
                        if (!fCheck)
                        {
                            fCheck = Fanar.Collision(Doodle, collisionCheck);
                            if (fCheck && !gameover) Doodle.speed.Y = -18;
                        }
                        if (Fanar.posize.Y > 720)
                        {
                            fRnd = -1;
                            Fanar.posize.X = -100;
                            Fanar.posize.Y = 730;
                            fCheck = false;
                        }

                        if (Doodle.posize.Y < 300) //to move boards_list and background
                        {
                            int speed = (int)Doodle.speed.Y;
                            Doodle.posize.Y -= (int)Doodle.speed.Y;
                            Board[] temp = boards_arr.ToArray();
                            for (int i = 0; i < boards_arr.Length; i++)
                                temp[i].BoardPosition = new Rectangle(temp[i].BoardPosition.X, temp[i].BoardPosition.Y - speed, temp[i].BoardPosition.Width, temp[i].BoardPosition.Height);
                            
                            boards_arr = temp.ToArray();
                            for (int i = 0; i < 4; i++)
                            {
                                mr[i].posize.Y -= speed;
                                fr[i].posize.Y -= speed;
                                gr[i].posize.Y -= speed;
                            }

                            if (Backgraound.bPosize.Y < 0)
                                Backgraound.bPosize.Y -= speed / 2;
                            Backgraound.sPosise1.Y -= speed / 2;
                            Backgraound.sPosise2.Y -= speed / 2;
                            score.s -= speed / 2;
                        }
                        Backgraound.sideCheck();

                        rePosition(boards_arr, ref mr, ref fr, ref gr);//to re position boards_list and movable enemys

                        for (int i = 0; i < boards_arr.Length; i++)//to check boards_list coliision
                            if (boards_arr[i].Collision(Doodle) && !gameover && collisionCheck == true)
                            {
                                Doodle.speed.Y = -13;
                            }
                        for (int i = 0; i < 4; i++)
                        {
                            if (mr[i].Collision(Doodle) && !gameover && collisionCheck == true)
                            {
                                Doodle.speed.Y = -13;
                            }
                            if (fr[i].Collision(Doodle) && !gameover && collisionCheck == true)
                                fr[i].posize.X = -100;
                            if (gr[i].Collision(Doodle) && !gameover && collisionCheck == true)
                            {
                                Doodle.speed.Y = -13;
                                gr[i].posize.X = -100;
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
                            Doodle.posize.X += -7;
                        }
                        else if (k.IsKeyDown(Keys.Right))
                        {
                            dir = cond.Right;
                            Doodle.posize.X += 7;
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
                                    Doodle.degree = (float)Math.Atan((-(m.Y - Doodle.posize.Y - 27)) / (m.X - Doodle.posize.X - 30));
                                    tirCheck = true;
                                }
                            }
                            if (m.Y < 280)
                                dir = cond.Tir;
                        }
                        if (Doodle.posize.Y > 720)//to end and gameovering game
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
                    Backgraound.gameStateCheck = false;
                    break;
                case option:
                    if (m.LeftButton == ButtonState.Pressed)
                    {
                        if (m.X > 80 && m.X < 240)
                            if (m.Y > 592 && m.Y < 652)
                                if (Backgraound.gameStateCheck == true)
                                    gameState = introMenu;
                                else
                                    gameState = pause;

                        if (m.X > 100 && m.X < 160)
                            if (m.Y > 330 && m.Y < 375)
                                Backgraound.soundCheck = false;
                        if (m.X > 160 && m.X < 236)
                            if (m.Y > 330 && m.Y < 375)
                                Backgraound.soundCheck = true;
                    }
                    break;
                case introMenu:
                    m = Mouse.GetState();
                    if (m.LeftButton == ButtonState.Pressed && !(m_temp1.LeftButton == ButtonState.Pressed))
                    {
                        Backgraound.gameStateCheck = true;
                        if (m.X > 68 && m.X < 240)
                            if (m.Y > 210 && m.Y < 270)
                            {
                                m = m_temp;
                                gameState = gameRunning;
                                MediaPlayer.Resume();
                                PlayAgain(ref Doodle, ref score, ref fr, ref gr, ref mr, ref Fanar, ref Backgraound, ref gameState);
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
                    menuDoodle.move();
                    if (menuDoodle.posize.Y > 550)
                        menuDoodle.speed.Y = -13;
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
                                PlayAgain(ref Doodle, ref score, ref fr, ref gr, ref mr, ref Fanar, ref Backgraound, ref gameState);
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
            Backgraound.draw(spriteBatch, gameState, score);
            

            if (gameState == gameRunning)
            {
                for (int i = 0; i < boards_arr.Length; i++)
                {
                    boards_arr[i].draw(spriteBatch);
                }

                for (int i = 0; i < 4; i++)
                {
                    mr[i].draw(spriteBatch);
                    fr[i].draw(spriteBatch);
                    gr[i].draw(spriteBatch);
                }
                Fanar.draw(spriteBatch, fCheck);

                Doodle.draw(spriteBatch, ref dir, m, gameState);
            }
            if (gameState == introMenu)
                menuDoodle.draw(spriteBatch, ref dir, m, 1);
            Backgraound.notifdraw(spriteBatch, gameState);
            score.draw(spriteBatch, gameState);
            mPoint.draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}