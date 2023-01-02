using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using VirusJump.Classes.Graphics;
using VirusJump.Classes.Scene.Objects;
using VirusJump.Classes.Scene.Objects.Boards;
using VirusJump.Classes.Scene.Objects.Scoring;
using VirusJump.Classes.Scene.Objects.Supplements;

namespace VirusJump;

public class Game1 : Game, ITexturesClasses
{
    public enum GameStateEnum
    {
        IntroMenu = 0,
        GameRunning,
        Pause,
        Option,
        GameOver,
        HScore
    }

    public enum PlayerOrientEnum
    {
        Left = 1,
        Right
    } //kako bo obrnjena slika

    public static GameStateEnum CurrentGameState;
    public static PlayerOrientEnum PlayerOrientation;

    public static ScorClass Score;
    public static Player Player;
    public static Player PlayerMenu;
    public static BoardsList BoardsList;
    public static Background Background;
    public static Pointer Pointer;
    public static Bullet Bullet;
    public static Bullet BulletEnemy;

    public static List<bool> Nivo;
    public static bool Brisi = false;

    public static ScoreManager ScoreManager;
    public static bool CollisionCheck;
    public static bool GameOver;

    public static Sound Sound;

    public static bool ThingsCollisionCheck;

    private bool _contentLoaded;

    //tipkovnica in miska
    private KeyboardState _k;
    private KeyboardState _kTemp;
    private KeyboardState _kTemp1;
    private MouseState _mouseState;
    private MouseState _mTemp;
    private MouseState _mTemp1;

    private string _playerName;
    private SpriteBatch _spriteBatch;
    public GameStateEnum GameState;
    
    private AnimatedSprite _loading;
    private bool _loadingDraw;
    private Texture2D _loadingTexture;
    private double elapsedTime;

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
        GameOver = false;
        _playerName = RandomString(10);
        Nivo = new List<bool> { false, false, false, false, false };
        _loading = new AnimatedSprite(Content.Load<SpriteSheet>("assets/looping.sf", new JsonContentLoader()));
        _loadingDraw = true;
        base.Initialize();
    }

    protected override async void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _loadingTexture = Content.Load<Texture2D>("assets/Loading");

        await ITexturesClasses.GenerateThreadsTextures(Content);

        Sound = ITexturesClasses.Sound;
        Score = ITexturesClasses.Score;
        Pointer = ITexturesClasses.Pointer;
        Player = ITexturesClasses.Player;
        PlayerMenu = ITexturesClasses.PlayerMenu;
        Background = ITexturesClasses.Background;
        Bullet = ITexturesClasses.Bullet;
        BulletEnemy = ITexturesClasses.BulletEnemy;
        BoardsList = ITexturesClasses.BoardsList;
        ScoreManager = ITexturesClasses.ScoreManager;

        _contentLoaded = true;
    }

    protected override void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        if (_contentLoaded)
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
                    if (Player.PlayerPosition.Y + Player.PlayerPosition.Height > 720) GameOver = true;

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

                    //player
                    Player.Move();
                    Player.Update();

                    for (var i = 0; i < 4; i++)
                        BoardsList.MovingBoardList[i].Move();

                    //to move and replace tampeolines
                    ITexturesClasses.Trampo.Update(Score, ITexturesClasses.Spring, ITexturesClasses.Jetpack, BoardsList,
                        Player, CollisionCheck, ThingsCollisionCheck);

                    //spring
                    ITexturesClasses.Spring.Update(Score, ITexturesClasses.Trampo, ITexturesClasses.Jetpack, BoardsList,
                        Player, CollisionCheck, ThingsCollisionCheck);

                    //jetpack
                    ITexturesClasses.Jetpack.Update(Score, ITexturesClasses.Spring, ITexturesClasses.Trampo, BoardsList,
                        Player, CollisionCheck, ThingsCollisionCheck);

                    //movingEnemy
                    ITexturesClasses.MovingEnemy.Move();
                    ITexturesClasses.MovingEnemy.Update(Bullet, BulletEnemy, Sound, Player, CurrentGameState,
                        ref CollisionCheck);

                    //static enemy
                    ITexturesClasses.StaticEnemy.Update(Bullet, BoardsList, Sound, Player, ref GameOver, ref CollisionCheck,
                        Score, ThingsCollisionCheck, ITexturesClasses.Trampo, ITexturesClasses.Jetpack,
                        ITexturesClasses.Spring);

                    //to move boards_list and background with player
                    GameRenderer.MoveWithPlayer();

                    if (Bullet.Position.Intersects(BulletEnemy.Position) ||
                        BulletEnemy.Position.Intersects(Bullet.Position))
                    {
                        BulletEnemy.IsCheck = false;
                        Bullet.IsCheck = false;
                    }
                    
                    //check side of background
                    Background.SideCheck();

                    //reposition items
                    GameRenderer.RePosition();

                    //to check boards_list coliision
                    BoardsList.Collision(ThingsCollisionCheck, CollisionCheck, GameOver, Player, Sound);

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
                        Player.PlayerPosition = new Rectangle(Player.PlayerPosition.X - 7, Player.PlayerPosition.Y,
                            Player.PlayerPosition.Width, Player.PlayerPosition.Height);
                        Player.ShootPosition = new Rectangle(Player.PlayerPosition.X + Player.PlayerPosition.Width / 2,
                            Player.PlayerPosition.Y + Player.PlayerPosition.Height / 2 + 15, Player.ShootPosition.Width,
                            Player.ShootPosition.Height);
                        Player.FirePosition = new Vector2(Player.PlayerPosition.X,
                            Player.PlayerPosition.Y + Player.PlayerPosition.Height);
                    }
                    else if (_k.IsKeyDown(Keys.Right))
                    {
                        PlayerOrientation = PlayerOrientEnum.Right;
                        Player.PlayerPosition = new Rectangle(Player.PlayerPosition.X + 7, Player.PlayerPosition.Y,
                            Player.PlayerPosition.Width, Player.PlayerPosition.Height);
                        Player.ShootPosition = new Rectangle(Player.PlayerPosition.X + Player.PlayerPosition.Width / 2,
                            Player.PlayerPosition.Y + Player.PlayerPosition.Height / 2 + 15, Player.ShootPosition.Width,
                            Player.ShootPosition.Height);
                        Player.FirePosition = new Vector2(Player.PlayerPosition.X,
                            Player.PlayerPosition.Y + Player.PlayerPosition.Height);
                    }

                    //check mouse state for shoot and pause menu 
                    _mouseState = Mouse.GetState();
                    if (_mouseState.LeftButton == ButtonState.Pressed && _mTemp.LeftButton != ButtonState.Pressed &&
                        CurrentGameState == GameStateEnum.GameRunning)
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
                                // ReSharper disable once PossibleLossOfFraction
                                Player.Degree = (float)Math.Atan(-(_mouseState.Y - Player.PlayerPosition.Y - 27) /
                                                                 (_mouseState.X - Player.PlayerPosition.X - 30));
                                Bullet.Position = new Rectangle(Player.PlayerPosition.X + 30,
                                    Player.PlayerPosition.Y + 27, Bullet.Position.Width, Bullet.Position.Height);
                                Pointer.GetAnimatedSprite.Play("shoot");
                                if (_mouseState.X < Player.PlayerPosition.X + 30)
                                    Bullet.Speed = new Vector2(-25 * (float)Math.Cos(Player.Degree),
                                        +25 * (float)Math.Sin(Player.Degree));
                                else
                                    Bullet.Speed = new Vector2(25 * (float)Math.Cos(Player.Degree),
                                        -25 * (float)Math.Sin(Player.Degree));

                                Bullet.IsCheck = true;
                                Sound.PlayerShoot.Play();
                            }
                        }
                    }

                    if (Bullet.Position.Y > 740 || Bullet.Position.X is < -20 or > 500 || Bullet.Position.Y < -20)
                        Bullet.IsCheck = false;
                    if (Bullet.IsCheck && CurrentGameState == GameStateEnum.GameRunning)
                        Bullet.Move();

                    var mouseControl = Mouse.GetState();
                    Player.ShootDegree = -(float)Math.Atan2(mouseControl.X - Player.PlayerPosition.X,
                        mouseControl.Y - Player.PlayerPosition.Y);

                    //popravi collision ko je jetpack
                    if (Player.Speed.Y > -12)
                    {
                        ThingsCollisionCheck = true;
                        Player.IsJetpack = false;
                    }
                    else
                    {
                        ThingsCollisionCheck = false;
                    }

                    //to end and gameovering game
                    if (Player.PlayerPosition.Y > 720)
                    {
                        CurrentGameState = GameStateEnum.GameOver;
                        if (CollisionCheck) Sound.Dead.Play();
                    }
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
                                Exit();
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

                    //update scores
                    Background.UpdateScores(ScoreManager, _playerName);

                    break;
                case GameStateEnum.HScore:
                    _mouseState = Mouse.GetState();
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                        if (_mouseState.X is > 296 and < 415)
                            if (_mouseState.Y is > 529 and < 584)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = GameStateEnum.IntroMenu;
                                Thread.Sleep(100);
                            }

                    break;
                case GameStateEnum.GameOver:
                    if (!Sound.PlayCheck)
                    {
                        MediaPlayer.Play(Sound.End);
                        MediaPlayer.IsRepeating = true;
                        Sound.PlayCheck = true;
                        ScoreManager.Add(new Score
                            {
                                PlayerName = _playerName,
                                Value = Score.Score
                            }
                            , _playerName);
                        ScoreManager.Save(ScoreManager);
                    }

                    //update scores
                    Background.UpdateScores(ScoreManager, _playerName);

                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (_mouseState.X is > 88 and < 271)
                            if (_mouseState.Y is > 438 and < 500)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                GameRenderer.PlayAgain();
                                MediaPlayer.Stop();
                                Thread.Sleep(100);
                            }

                        if (_mouseState.X is > 284 and < 404)
                            if (_mouseState.Y is > 504 and < 559)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = GameStateEnum.IntroMenu;
                                MediaPlayer.Stop();
                                PlayerOrientation = PlayerOrientEnum.Right;
                                Thread.Sleep(100);
                            }

                        _mouseState = _mTemp1;
                    }

                    break;
            }
            ITexturesClasses.MovingEnemy.GetAnimatedSprite.Update(gameTime);
            Player.GetAnimatedSprite.Update(gameTime);
            Pointer.GetAnimatedSprite.Update(gameTime);
        }

        if (!_contentLoaded || elapsedTime <= 5.0)
        {
            _loading.Play("rotate");
            _loading.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        GraphicsDevice.Clear(Color.CornflowerBlue);

        if (!_contentLoaded || elapsedTime <= 3.0)
        {
            _spriteBatch.Draw(_loadingTexture, new Vector2(0,0), null, Color.White);
            _loading.Draw(_spriteBatch, new Vector2(230, 500), 0f, new Vector2(1, 1));
        }
        else
        {
            Background.Draw(_spriteBatch, CurrentGameState, Score);

            if (CurrentGameState == GameStateEnum.GameRunning)
            {
                foreach (var board in BoardsList.BoardList)
                    if (board.Visible && board.DrawVisible)
                        board.DrawSprite(_spriteBatch);

                for (var i = 0; i < 4; i++)
                {
                    if (BoardsList.MovingBoardList[i].DrawVisible)
                        BoardsList.MovingBoardList[i].DrawSprite(_spriteBatch);
                    if (BoardsList.FakeBoardList[i].Visible && BoardsList.FakeBoardList[i].DrawVisible)
                        BoardsList.FakeBoardList[i].DrawSprite(_spriteBatch);

                    if (BoardsList.GoneBoardList[i].Visible && BoardsList.GoneBoardList[i].DrawVisible)
                        BoardsList.GoneBoardList[i].DrawSprite(_spriteBatch);
                }

                if (ITexturesClasses.Trampo.Visible) ITexturesClasses.Trampo.Draw(_spriteBatch);

                if (ITexturesClasses.Spring.Visible) ITexturesClasses.Spring.Draw(_spriteBatch);

                if (ITexturesClasses.Jetpack.Visible) ITexturesClasses.Jetpack.Draw(_spriteBatch);

                ITexturesClasses.StaticEnemy.Draw(_spriteBatch);
                if (ITexturesClasses.MovingEnemy.Visible) ITexturesClasses.MovingEnemy.Draw(_spriteBatch);

                Background.ScoreDraw(_spriteBatch, CurrentGameState);
                Player.Draw(_spriteBatch, PlayerOrientation, CurrentGameState, CollisionCheck);
                Score.Draw(_spriteBatch, CurrentGameState);
            }

            if (CurrentGameState == GameStateEnum.IntroMenu)
                PlayerMenu.Draw(_spriteBatch, PlayerOrientation, GameStateEnum.IntroMenu, CollisionCheck);

            if (Bullet.IsCheck) Bullet.Draw(_spriteBatch, CurrentGameState);

            if (BulletEnemy.IsCheck) BulletEnemy.Draw(_spriteBatch, CurrentGameState);

            Pointer.Draw(_spriteBatch);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private string RandomString(int length)
    {
        var random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}