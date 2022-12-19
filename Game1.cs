using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using VirusJump.Classes.Scene.Objects;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework.Media;
using VirusJump.Classes.Scene.Objects.Supplements;
using VirusJump.Classes.Graphics;
using VirusJump.Classes.Scene.Objects.Boards;
using VirusJump.Classes.Scene.Objects.Jumpers;
using VirusJump.Classes.Scene.Objects.Enemies;

namespace VirusJump
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;

        //tipkovnica in miska
        private KeyboardState _k;
        private KeyboardState _kTemp;
        private KeyboardState _kTemp1;
        private MouseState _mouseState;
        private MouseState _mTemp;
        private MouseState _mTemp1;

        public static GameStateEnum CurrentGameState;

        public enum GameStateEnum { IntroMenu = 0, GameRunning, Pause, Option, GameOver, HScore};
        public GameStateEnum GameState;

        public enum PlayerOrientEnum { Left = 1, Right } //kako bo obrnjena slika
        public static PlayerOrientEnum PlayerOrientation;

        public static Scoring Score;
        public static Player Player;
        public static Player PlayerMenu;
        public static BoardsList BoardsList;
        public static Background Background;
        public static Pointer Pointer;
        public static Bullet Bullet;
        public static Bullet BulletEnemy;


        public static Trampo Trampo;
        public static Spring Spring;
        public static Jetpack Jetpack;

        public static List<bool> Nivo;
        public static bool Brisi = false;

        public static StaticEnemy StaticEnemy;
        public static MovingEnemy MovingEnemy;


        public static bool CollisionCheck;
        public static bool Gameover;

        public static Sound Sound;

        public static bool ThingsCollisionCheck;


        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 480;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            PlayerOrientation = PlayerOrientEnum.Right;
            CollisionCheck = true;
            ThingsCollisionCheck = true;
            Gameover = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Nivo = new List<bool> { false, false, false, false, false };
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            BoardsList = new BoardsList(Content);

            Player = new Player(Content);
            PlayerMenu = new Player(Content)
            {
                PlayerPosition = new Rectangle(60, 520, 80, 80)
            };

            Background = new Background(Content);
            Score = new Scoring(Content);
            Pointer = new Pointer(Content);
            Bullet = new Bullet(Content, 0);
            
            Trampo = new Trampo(Content);
            Spring = new Spring(Content);
            Jetpack = new Jetpack(Content);
            BulletEnemy = new Bullet(Content, 1);

            StaticEnemy = new StaticEnemy(Content);
            MovingEnemy = new MovingEnemy(Content);

            Sound = new Sound(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            _mouseState = Mouse.GetState();
            _k = Keyboard.GetState();
            Pointer.Position = new Vector2(_mouseState.X - 10, _mouseState.Y - 10);

            switch (CurrentGameState)
            {
                case GameStateEnum.GameRunning:
                    {
                        if (Player.PlayerPosition.Y + Player.PlayerPosition.Height > 720) Gameover = true;

                        if (Sound.PlayCheck && Background.SoundCheck)
                        {
                            MediaPlayer.Play(Sound.Background);
                            MediaPlayer.IsRepeating = true;
                            Sound.PlayCheck = false;
                        }
                        if (!Background.SoundCheck)
                        {
                            MediaPlayer.Stop();
                            Sound.PlayCheck = true;
                        }
                        
                        Player.Move();
                        //to prevent from exiting from sides of screen
                        if (Player.PlayerPosition.X + 10 < 0)
                        {
                            Player.PlayerPosition = new Rectangle(450, Player.PlayerPosition.Y, Player.PlayerPosition.Width, Player.PlayerPosition.Height);
                            Player.ShootPosition = new Rectangle(Player.PlayerPosition.X + Player.PlayerPosition.Width / 2, Player.PlayerPosition.Y + Player.PlayerPosition.Height / 2 + 15, Player.ShootPosition.Width, Player.ShootPosition.Height);
                            Player.FirePosition = new Vector2(Player.PlayerPosition.X, Player.PlayerPosition.Y + Player.PlayerPosition.Height);
                        }
                        if (Player.PlayerPosition.X > 451)
                        {
                            Player.PlayerPosition = new Rectangle(-10, Player.PlayerPosition.Y, Player.PlayerPosition.Width, Player.PlayerPosition.Height);
                            Player.ShootPosition = new Rectangle(Player.PlayerPosition.X + Player.PlayerPosition.Width / 2, Player.PlayerPosition.Y + Player.PlayerPosition.Height / 2 + 15, Player.ShootPosition.Width, Player.ShootPosition.Height);
                            Player.FirePosition = new Vector2(Player.PlayerPosition.X, Player.PlayerPosition.Y + Player.PlayerPosition.Height);
                        }

                        for (var i = 0; i < 4; i++)
                            BoardsList.MovingBoardList[i].Move();

                        //to move and replace tampeolines
                        if (Score.Score > Trampo.ScoreToMove && !Trampo.Visible)
                        {
                            Trampo.ScoreToMove += Trampo.ScoreMoveStep;
                            do
                            {
                                var rnd = new Random();
                                Trampo.TRand = rnd.Next(0, BoardsList.BoardList.Length - 1);
                            } while (BoardsList.BoardList[Trampo.TRand].Position.Y > 0 || BoardsList.BoardList[Trampo.TRand].Visible == false || (Spring.SRand == Trampo.TRand && (Spring.SRand != -1 && Trampo.TRand != -1)) || (Spring.SRand == Jetpack.JRand && (Spring.SRand != -1 && Jetpack.JRand != -1)) || Trampo.TRand == Jetpack.JRand && (Trampo.TRand != -1 && Jetpack.JRand != -1));
                            Trampo.Visible = true;
                        }
                        if (Trampo.TRand != -1)
                        {
                            Trampo.TrampoPosition = new Rectangle(BoardsList.BoardList[Trampo.TRand].Position.X + 10, BoardsList.BoardList[Trampo.TRand].Position.Y - 15, Trampo.TrampoPosition.Width, Trampo.TrampoPosition.Height);
                            Trampo.Visible = true;
                        }
                        if (Trampo.Visible)
                        {
                            Trampo.Check = Trampo.Collision(Player, CollisionCheck);
                        }
                        if (Trampo.Check && ThingsCollisionCheck)
                        {
                            Player.Speed = new Vector2(Player.Speed.X, -32);
                            Trampo.TRand = -1;
                            Trampo.Visible = false;
                            Trampo.Check = false;
                        }
                        if (Trampo.TrampoPosition.Y > 690)
                        {
                            Trampo.TRand = -1;
                            Trampo.Visible = false;
                            Trampo.Check = false;
                        }

                        //spring
                        if (Score.Score > Spring.ScoreToMove && !Spring.Visible)
                        {
                            Spring.ScoreToMove += Spring.ScoreMoveStep;
                            do
                            {
                                var rnd = new Random();
                                Spring.SRand = rnd.Next(0, BoardsList.BoardList.Length - 1);
                            } while (BoardsList.BoardList[Spring.SRand].Position.Y > 0 || BoardsList.BoardList[Spring.SRand].Visible == false || (Spring.SRand == Trampo.TRand && (Spring.SRand != -1 && Trampo.TRand != -1)) || (Spring.SRand == Jetpack.JRand && (Spring.SRand != -1 && Jetpack.JRand != -1)) || Trampo.TRand == Jetpack.JRand && (Trampo.TRand != -1 && Jetpack.JRand != -1));
                            Spring.Visible = true;
                        }
                        if (Spring.SRand != -1 && Spring.Visible)
                        {
                            Spring.SpringPosition = new Rectangle(BoardsList.BoardList[Spring.SRand].Position.X + 10, BoardsList.BoardList[Spring.SRand].Position.Y - 30, Spring.SpringPosition.Width, Spring.SpringPosition.Height);
                        }
                        if (Spring.Visible)
                        {
                            Spring.SCheck = Spring.Collision(Player, CollisionCheck);
                        }
                        if (Spring.SCheck && ThingsCollisionCheck)
                        {
                            Player.Speed = new Vector2(Player.Speed.X, -23);
                            Spring.SRand = -1;
                            Spring.SCheck = false;
                            Spring.Visible = false;
                        }
                        if (Spring.SpringPosition.Y > 690)
                        {
                            Spring.SRand = -1;
                            Spring.Visible = false;
                            Spring.SCheck = false;
                        }

                        //jetpack
                        if (Score.Score > Jetpack.ScoreToMove && !Jetpack.Visible)
                        {
                            Jetpack.ScoreToMove += Jetpack.ScoreMoveStep;
                            do
                            {
                                var rnd = new Random();
                                Jetpack.JRand = rnd.Next(0, BoardsList.BoardList.Length - 1);
                            } while (BoardsList.BoardList[Jetpack.JRand].Position.Y > 0 || BoardsList.BoardList[Jetpack.JRand].Visible == false || (Spring.SRand == Trampo.TRand && (Spring.SRand != -1 && Trampo.TRand != -1)) || (Spring.SRand == Jetpack.JRand && (Spring.SRand != -1 && Jetpack.JRand != -1)) || Trampo.TRand == Jetpack.JRand && (Trampo.TRand != -1 && Jetpack.JRand != -1));
                            Jetpack.Visible = true;
                        }
                        if (Jetpack.JRand != -1 && Jetpack.Visible)
                        {
                            Jetpack.JetPosition = new Rectangle(BoardsList.BoardList[Jetpack.JRand].Position.X + 10, BoardsList.BoardList[Jetpack.JRand].Position.Y - Jetpack.JetPosition.Height, Jetpack.JetPosition.Width, Jetpack.JetPosition.Height);
                        }
                        if (Jetpack.Visible)
                        {
                            Jetpack.JCheck = Jetpack.Collision(Player, CollisionCheck);
                        }
                        if (Jetpack.JCheck && ThingsCollisionCheck)
                        {
                            Player.Speed = new Vector2(Player.Speed.X, -60);
                            Jetpack.JRand = -1;
                            Jetpack.Visible = false;
                            Jetpack.JCheck = false;
                            Player.IsJetpack = true;
                            Player.GetAnimatedSprite.Play("fire");
                        }
                        if (Jetpack.JetPosition.Y > 690)
                        {
                            Jetpack.JRand = -1;
                            Jetpack.Visible = false;
                            Jetpack.JCheck = false;
                        }

                        //movingEnemy
                        MovingEnemy.Move();
                        if (MovingEnemy.BulletCollision(Bullet))
                        {
                            MovingEnemy.MvRand = true;
                            MovingEnemy.MvCollision = false;
                            MovingEnemy.Visible = false;
                        }

                        if (Math.Abs(MovingEnemy.Position.X - Player.PlayerPosition.X) < 10)
                        {
                            if (!BulletEnemy.IsCheck)
                            {
                                MovingEnemy.Degree = (float)Math.Atan((-(Player.PlayerPosition.Y - 30 - MovingEnemy.Position.Y)) / (Player.PlayerPosition.X - 30 - MovingEnemy.Position.X));
                                BulletEnemy.Position = new Rectangle(MovingEnemy.Position.X + 30, MovingEnemy.Position.Y + 30, BulletEnemy.Position.Width, BulletEnemy.Position.Height);
                                if (Player.PlayerPosition.X < MovingEnemy.Position.X + 30)
                                {
                                    BulletEnemy.Speed = new Vector2(-1 * (float)Math.Cos(MovingEnemy.Degree), +1 * (float)Math.Sin(MovingEnemy.Degree));
                                }
                                else
                                {
                                    BulletEnemy.Speed = new Vector2(1 * (float)Math.Cos(MovingEnemy.Degree), -1 * (float)Math.Sin(MovingEnemy.Degree));
                                }
                                BulletEnemy.IsCheck = true;
                            }
                        }
                        
                        if (BulletEnemy.Position.Y > 740 || BulletEnemy.Position.X is < -20 or > 500 || BulletEnemy.Position.Y < -20)
                            BulletEnemy.IsCheck = false;
                        if (BulletEnemy.IsCheck && CurrentGameState == GameStateEnum.GameRunning)
                            BulletEnemy.Move();

                        if (BulletEnemy.IsCheck && Player.BulletCollision(BulletEnemy))
                        {
                            Player.Speed = new Vector2(Player.Speed.X, 0);
                            BulletEnemy.IsCheck = false;
                            CollisionCheck = false;
                        }

                        //static enemy
                        if (Score.Score % 430 > 400 && StaticEnemy.Position.Y > 780)
                        {
                            var rnd = new Random();
                            do
                            {
                                StaticEnemy.StRand = rnd.Next(0, BoardsList.BoardList.Length - 1);
                            } while (BoardsList.BoardList[StaticEnemy.StRand].Position.Y > 0 || BoardsList.BoardList[StaticEnemy.StRand].Visible == false || (Spring.SRand == Trampo.TRand && (Spring.SRand != -1 && Trampo.TRand != -1)) || (Spring.SRand == Jetpack.JRand && (Spring.SRand != -1 && Jetpack.JRand != -1)) || Trampo.TRand == Jetpack.JRand && (Trampo.TRand != -1 && Jetpack.JRand != -1));
                            StaticEnemy.TextureRand = rnd.Next(0, 2);
                        }
                        if (StaticEnemy.StRand != -1)
                        {
                            StaticEnemy.Position = new Rectangle(BoardsList.BoardList[StaticEnemy.StRand].Position.X, BoardsList.BoardList[StaticEnemy.StRand].Position.Y - 53, StaticEnemy.Position.Width, StaticEnemy.Position.Height);
                        }
                        if (StaticEnemy.Collision(Player, ref CollisionCheck) == 0 && !Gameover && ThingsCollisionCheck)
                        {
                            Player.Speed = new Vector2(Player.Speed.X, -15);
                            StaticEnemy.StRand = -1;
                        }
                        else if (StaticEnemy.Collision(Player, ref CollisionCheck) == 1 && !Gameover && ThingsCollisionCheck)
                        {
                            Player.Speed = new Vector2(Player.Speed.X, 0);
                            CollisionCheck = false;
                        }
                        if (StaticEnemy.Position.Y < 780 && StaticEnemy.StRand == -1)
                            StaticEnemy.Position = new Rectangle(StaticEnemy.Position.X, StaticEnemy.Position.Y + 11, StaticEnemy.Position.Width, StaticEnemy.Position.Height);

                        if (StaticEnemy.Position.Y > 795 || StaticEnemy.BulletCollision(Bullet))
                        {
                            if (StaticEnemy.BulletCollision(Bullet))
                            {
                                Bullet.IsCheck = false;
                            }
                            StaticEnemy.StRand = -1;
                            StaticEnemy.Position = new Rectangle(-200, 800, 60, 55);
                        }

                        //to move boards_list and background with player
                        if (Player.PlayerPosition.Y < 300)
                        {
                            var speed = (int)Player.Speed.Y;
                            Player.PlayerPosition = new Rectangle(Player.PlayerPosition.X, Player.PlayerPosition.Y - (int)Player.Speed.Y, Player.PlayerPosition.Width, Player.PlayerPosition.Height);
                            Player.ShootPosition = new Rectangle(Player.PlayerPosition.X + Player.PlayerPosition.Width / 2, Player.PlayerPosition.Y + Player.PlayerPosition.Height / 2 + 15, Player.ShootPosition.Width, Player.ShootPosition.Height);
                            Player.FirePosition = new Vector2(Player.PlayerPosition.X, Player.PlayerPosition.Y + Player.PlayerPosition.Height);

                            foreach (var board in BoardsList.BoardList)
                            {
                                board.Position = new Rectangle(board.Position.X, board.Position.Y - speed, board.Position.Width, board.Position.Height);
                            }

                            for (var i = 0; i < BoardsList.MovingBoardList.Length; i++)
                            {
                                BoardsList.MovingBoardList[i].Position = new Rectangle(BoardsList.MovingBoardList[i].Position.X, BoardsList.MovingBoardList[i].Position.Y - speed, BoardsList.MovingBoardList[i].Position.Width, BoardsList.MovingBoardList[i].Position.Height);
                                BoardsList.FakeBoardList[i].Position = new Rectangle(BoardsList.FakeBoardList[i].Position.X, BoardsList.FakeBoardList[i].Position.Y - speed, BoardsList.FakeBoardList[i].Position.Width, BoardsList.FakeBoardList[i].Position.Height);
                                BoardsList.GoneBoardList[i].Position = new Rectangle(BoardsList.GoneBoardList[i].Position.X, BoardsList.GoneBoardList[i].Position.Y - speed, BoardsList.GoneBoardList[i].Position.Width, BoardsList.GoneBoardList[i].Position.Height);
                            }

                            if (Background.BPosize.Y < 0)
                                Background.BPosize = new Rectangle(Background.BPosize.X, Background.BPosize.Y - (speed / 2), Background.BPosize.Width, Background.BPosize.Height);
                            Background.SPosise1 = new Rectangle(Background.SPosise1.X, Background.SPosise1.Y - (speed / 2), Background.SPosise1.Width, Background.SPosise1.Height);
                            Background.SPosise2 = new Rectangle(Background.SPosise2.X, Background.SPosise2.Y - (speed / 2), Background.SPosise2.Width, Background.SPosise2.Height);
                            Score.Score -= speed / 2;
                        }
                        Background.SideCheck();

                        GameRenderer.RePosition();

                        //to check boards_list coliision
                        if (ThingsCollisionCheck)
                        {
                            foreach (var board in BoardsList.BoardList)
                            {
                                if (board.Visible && board.Collision(Player) && !Gameover && CollisionCheck)
                                {
                                    Player.Speed = new Vector2(Player.Speed.X, -13);
                                }
                            }
                            for (var i = 0; i < BoardsList.MovingBoardList.Length; i++)
                            {
                                if (BoardsList.MovingBoardList[i].Collision(Player) && !Gameover && CollisionCheck)
                                {
                                    Player.Speed = new Vector2(Player.Speed.X, -13);
                                }
                                if (BoardsList.FakeBoardList[i].Visible && BoardsList.FakeBoardList[i].Collision(Player) && !Gameover && CollisionCheck)
                                {
                                    BoardsList.FakeBoardList[i].Visible = false;
                                }
                                if (BoardsList.GoneBoardList[i].Visible && BoardsList.GoneBoardList[i].Collision(Player) && !Gameover && CollisionCheck)
                                {
                                    Player.Speed = new Vector2(Player.Speed.X, -13);
                                    BoardsList.GoneBoardList[i].Visible = false;
                                }
                            }
                        }

                        //to go to pause menue bye esc clicking
                        _kTemp1 = Keyboard.GetState();
                        if (_kTemp1.IsKeyDown(Keys.Escape) && !_kTemp.IsKeyDown(Keys.Escape))
                        {
                            CurrentGameState = GameStateEnum.Pause;
                            break;
                        }
                        //to move left and right
                        _kTemp = _kTemp1;
                        if (_k.IsKeyDown(Keys.Left))
                        {
                            PlayerOrientation = PlayerOrientEnum.Left;
                            Player.PlayerPosition = new Rectangle(Player.PlayerPosition.X - 7, Player.PlayerPosition.Y, Player.PlayerPosition.Width, Player.PlayerPosition.Height);
                            Player.ShootPosition = new Rectangle(Player.PlayerPosition.X + Player.PlayerPosition.Width / 2, Player.PlayerPosition.Y + Player.PlayerPosition.Height / 2 + 15, Player.ShootPosition.Width, Player.ShootPosition.Height);
                            Player.FirePosition = new Vector2(Player.PlayerPosition.X, Player.PlayerPosition.Y + Player.PlayerPosition.Height);
                        }
                        else if (_k.IsKeyDown(Keys.Right))
                        {
                            PlayerOrientation = PlayerOrientEnum.Right;
                            Player.PlayerPosition = new Rectangle(Player.PlayerPosition.X + 7, Player.PlayerPosition.Y, Player.PlayerPosition.Width, Player.PlayerPosition.Height);
                            Player.ShootPosition = new Rectangle(Player.PlayerPosition.X + Player.PlayerPosition.Width / 2, Player.PlayerPosition.Y + Player.PlayerPosition.Height / 2 + 15, Player.ShootPosition.Width, Player.ShootPosition.Height);
                            Player.FirePosition = new Vector2(Player.PlayerPosition.X, Player.PlayerPosition.Y + Player.PlayerPosition.Height);
                        }

                        //check mouse state for shoot and pause menu 
                        _mouseState = Mouse.GetState();
                        if (_mouseState.LeftButton == ButtonState.Pressed && _mTemp.LeftButton != ButtonState.Pressed && CurrentGameState == GameStateEnum.GameRunning)
                        {
                            if (_mouseState.X is > 420 and < 470 && _mouseState.Y is > 5 and < 40)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = GameStateEnum.Pause;
                                MediaPlayer.Pause();
                            }
                            //to shoot tir
                            else
                            {
                                if (!Bullet.IsCheck)
                                {
                                    Player.Degree = (float)Math.Atan((-(_mouseState.Y - Player.PlayerPosition.Y - 27)) / (_mouseState.X - Player.PlayerPosition.X - 30));
                                    Bullet.Position = new Rectangle(Player.PlayerPosition.X + 30, Player.PlayerPosition.Y + 27, Bullet.Position.Width, Bullet.Position.Height);
                                    Pointer.GetAnimatedSprite.Play("shoot");
                                    if (_mouseState.X < Player.PlayerPosition.X + 30)
                                    {
                                        Bullet.Speed = new Vector2(-25 * (float)Math.Cos(Player.Degree), +25 * (float)Math.Sin(Player.Degree));
                                    }
                                    else
                                    {
                                        Bullet.Speed = new Vector2(25 * (float)Math.Cos(Player.Degree), -25 * (float)Math.Sin(Player.Degree));
                                    }
                                    Bullet.IsCheck = true;
                                }
                            }
                        }
                        if (Bullet.Position.Y > 740 || Bullet.Position.X is < -20 or > 500 || Bullet.Position.Y < -20)
                            Bullet.IsCheck = false;
                        if (Bullet.IsCheck && CurrentGameState == GameStateEnum.GameRunning)
                            Bullet.Move();

                        var mouseControl = Mouse.GetState();
                        Player.ShootDegree = -(float)Math.Atan2(mouseControl.X - Player.PlayerPosition.X, mouseControl.Y - Player.PlayerPosition.Y);

                        //popravi collision ko je jetpack
                        if (Player.Speed.Y > -12)
                        {
                            ThingsCollisionCheck = true;
                            Player.IsJetpack = false;
                        }
                        else ThingsCollisionCheck = false;

                        //to end and gameovering game
                        if (Player.PlayerPosition.Y > 720)
                            CurrentGameState = GameStateEnum.GameOver;
                    }
                    break;
                case GameStateEnum.Pause:
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (_mouseState.X is > 131 and < 251)
                            if (_mouseState.Y is > 372 and < 428)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = GameStateEnum.GameRunning;
                                MediaPlayer.Resume();
                                Thread.Sleep(100);
                            }
                        if (_mouseState.X is > 215 and < 335)
                            if (_mouseState.Y is > 454 and < 510)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = GameStateEnum.IntroMenu;
                                PlayerOrientation = PlayerOrientEnum.Right;
                                Thread.Sleep(100);
                            }
                        if (_mouseState.X is > 66 and < 186)
                            if (_mouseState.Y is > 282 and < 338)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = GameStateEnum.Option;
                                PlayerOrientation = PlayerOrientEnum.Right;
                                Thread.Sleep(100);
                            }
                    }
                    _kTemp = Keyboard.GetState();
                    if (_kTemp.IsKeyDown(Keys.Escape) && !_kTemp1.IsKeyDown(Keys.Escape))
                        CurrentGameState = GameStateEnum.GameRunning;
                    _kTemp1 = _kTemp;
                    Background.GameStateCheck = false;
                    break;
                case GameStateEnum.Option:
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (_mouseState.X is > 297 and < 415)
                            if (_mouseState.Y is > 530 and < 584)
                                if (Background.GameStateCheck)
                                {
                                    Pointer.GetAnimatedSprite.Play("shoot");
                                    CurrentGameState = GameStateEnum.IntroMenu;
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    Pointer.GetAnimatedSprite.Play("shoot");
                                    CurrentGameState = GameStateEnum.Pause;
                                    Thread.Sleep(100);
                                }
                        if (_mouseState.X is > 210 and < 278)
                            if (_mouseState.Y is > 405 and < 462)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                Background.SoundCheck = true;
                            }
                        if (_mouseState.X is > 99 and < 176)
                            if (_mouseState.Y is > 407 and < 461)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                Background.SoundCheck = false;
                            }
                    }
                    break;
                case GameStateEnum.IntroMenu:
                    _mouseState = Mouse.GetState();
                    if (_mouseState.LeftButton == ButtonState.Pressed && _mTemp1.LeftButton != ButtonState.Pressed)
                    {
                        Background.GameStateCheck = true;
                        if (_mouseState.X is > 67 and < 185)
                            if (_mouseState.Y is > 283 and < 337)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                _mouseState = _mTemp;
                                CurrentGameState = GameStateEnum.GameRunning;
                                MediaPlayer.Resume();
                                GameRenderer.PlayAgain();
                                Thread.Sleep(100);
                            }
                        if (_mouseState.X is > 292 and < 410)
                            if (_mouseState.Y is > 528 and < 582 && CurrentGameState == GameStateEnum.IntroMenu)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                this.Exit();
                            }
                        if (_mouseState.X is > 217 and < 335)
                            if (_mouseState.Y is > 454 and < 508)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = GameStateEnum.Option;
                                Thread.Sleep(100);
                            }
                        if (_mouseState.X is > 130 and < 248)
                            if (_mouseState.Y is > 373 and < 427)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = GameStateEnum.HScore;
                                Thread.Sleep(100);
                            }
                    }
                    PlayerMenu.Move();
                    if (PlayerMenu.PlayerPosition.Y > 550)
                        PlayerMenu.Speed = new Vector2(PlayerMenu.Speed.X, -13);
                    break;
                case GameStateEnum.HScore:
                    _mouseState = Mouse.GetState();
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (_mouseState.X is > 296 and < 415)
                            if (_mouseState.Y is > 529 and < 584)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = GameStateEnum.IntroMenu;
                                Thread.Sleep(100);
                            }
                    }
                    break;
                case GameStateEnum.GameOver:
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (_mouseState.X is > 88 and < 271)
                            if (_mouseState.Y is > 438 and < 500)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                GameRenderer.PlayAgain();
                                Thread.Sleep(100);
                            }
                        if (_mouseState.X is > 284 and < 404)
                            if (_mouseState.Y is > 504 and < 559)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = GameStateEnum.IntroMenu;
                                MediaPlayer.Pause();
                                PlayerOrientation = PlayerOrientEnum.Right;
                                Thread.Sleep(100);
                            }
                        _mouseState = _mTemp1;
                    }
                    break;
            }

            Pointer.GetAnimatedSprite.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            Background.Draw(_spriteBatch, CurrentGameState, Score);

            if (CurrentGameState == GameStateEnum.GameRunning)
            {
                Background.ScoreDraw(_spriteBatch, CurrentGameState);
                foreach (var board in BoardsList.BoardList)
                {
                    if(board.Visible && board.DrawVisible)
                    {
                        board.DrawSprite(_spriteBatch);
                    }
                }

                for (var i = 0; i < 4; i++)
                {
                    BoardsList.MovingBoardList[i].DrawSprite(_spriteBatch);
                    if (BoardsList.FakeBoardList[i].Visible && BoardsList.FakeBoardList[i].DrawVisible)
                    {
                        BoardsList.FakeBoardList[i].DrawSprite(_spriteBatch);
                    }
                    if (BoardsList.GoneBoardList[i].Visible && BoardsList.GoneBoardList[i].DrawVisible)
                    {
                        BoardsList.GoneBoardList[i].DrawSprite(_spriteBatch);
                    }
                }

                if (Trampo.Visible)
                {
                    Trampo.Draw(_spriteBatch);
                }
                if (Spring.Visible)
                {
                    Spring.Draw(_spriteBatch);
                }
                if (Jetpack.Visible)
                {
                    Jetpack.Draw(_spriteBatch);
                }
                StaticEnemy.Draw(_spriteBatch);
                if(MovingEnemy.Visible)
                {
                    MovingEnemy.Draw(_spriteBatch);
                }
                Player.Draw(_spriteBatch, PlayerOrientation, CurrentGameState, CollisionCheck);
                Score.Draw(_spriteBatch, CurrentGameState);
            }
            if (CurrentGameState == GameStateEnum.IntroMenu) 
            {
                PlayerMenu.Draw(_spriteBatch, PlayerOrientation, GameStateEnum.IntroMenu, CollisionCheck);
            }
            if(Bullet.IsCheck)
            {
                Bullet.Draw(_spriteBatch, CurrentGameState);
            }
            if (BulletEnemy.IsCheck)
            {
                BulletEnemy.Draw(_spriteBatch, CurrentGameState);
            }
            Pointer.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}