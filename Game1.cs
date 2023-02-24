using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using JumperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using VirusJump.Graphics;

namespace VirusJump;

public class Game1 : Game, ITexturesClasses
{
    public static ClassEnums.GameStateEnum CurrentGameState;
    public static ClassEnums.PlayerOrientEnum PlayerOrientation;
    public static ClassEnums.GameModeEnum GameModeEnum;

    public static ScorClass Score;
    public static Player Player;
    public static Player PlayerMenu;
    public static BoardsList BoardsList;
    public static Background Background;
    public static Pointer Pointer;
    public static Bullet Bullet;
    public static Bullet BulletEnemy;

    public static Trampo Trampo;
    public static Jetpack Jetpack;
    public static Spring Spring;

    public static MovingEnemy MovingEnemy;
    public static EasyMovingEnemy EasyMovingEnemy;
    public static StaticEnemy StaticEnemy;

    public static MyInputField MyInputField;

    public static List<bool> Nivo;
    public static bool Brisi = false;

    public static ScoreManager ScoreManagerEasy;
    public static ScoreManager ScoreManagerHard;
    public static bool CollisionCheck;
    public static bool GameOver;

    public static Sound Sound;

    public static bool ThingsCollisionCheck;
    public static ClassEnums.GameModeEnum GameMode;

    public static Windowbox windowbox;

    private int _allObjects;

    private bool _contentLoaded;
    private double _elapsedTime;

    //tipkovnica in miska
    private KeyboardState _k;
    private KeyboardState _kTemp;
    private KeyboardState _kTemp1;

    private AnimatedSprite _loading;
    private bool _loadingDraw;
    private Texture2D _loadingTexture;
    private MouseState _mouseState;
    private MouseState _mTemp;
    private MouseState _mTemp1;

    private string _playerName;
    private SpriteBatch _spriteBatch;
    private Stopwatch _stopwatch;
    private int _visibleObjects;
    public ClassEnums.GameStateEnum GameState;

    public const int designedResolutionWidth = 480;
    public const int designedResolutionHeight = 720;

    public Game1()
    {
        var graphics = new GraphicsDeviceManager(this);
        graphics.IsFullScreen = false;
        graphics.PreferredBackBufferWidth = designedResolutionWidth;
        graphics.PreferredBackBufferHeight = designedResolutionHeight;
        Content.RootDirectory = "Content";
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        PlayerOrientation = ClassEnums.PlayerOrientEnum.Right;
        CollisionCheck = true;
        ThingsCollisionCheck = true;
        GameOver = false;
        //_playerName = RandomString(10);
        _playerName = "";
        Nivo = new List<bool> { false, false, false, false, false, false };
        _loading = new AnimatedSprite(Content.Load<SpriteSheet>("assets/looping.sf", new JsonContentLoader()));
        _loadingDraw = true;
        _allObjects = 0;
        _visibleObjects = 0;
        _stopwatch = new Stopwatch();
        CurrentGameState = ClassEnums.GameStateEnum.InputName;
        GameMode = ClassEnums.GameModeEnum.Easy;
        windowbox = new Windowbox(this, designedResolutionWidth, designedResolutionHeight);
        base.Initialize();
    }

    protected override async void LoadContent()
    {
        _stopwatch.Start();
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _loadingTexture = Content.Load<Texture2D>("assets/Loading");

        await ITexturesClasses.GenerateThreadsTextures(Content, GraphicsDevice);
        //ITexturesClasses.GenerateTexturesClassesNoThreads(Content, GraphicsDevice);

        Sound = ITexturesClasses.Sound;
        Score = ITexturesClasses.Score;
        Pointer = ITexturesClasses.Pointer;
        Player = ITexturesClasses.Player;
        PlayerMenu = ITexturesClasses.PlayerMenu;
        Background = ITexturesClasses.Background;
        Bullet = ITexturesClasses.Bullet;
        BulletEnemy = ITexturesClasses.BulletEnemy;
        BoardsList = ITexturesClasses.BoardsList;
        ScoreManagerEasy = ITexturesClasses.ScoreManagerEasy;
        ScoreManagerHard = ITexturesClasses.ScoreManagerHard;
        Spring = ITexturesClasses.Spring;
        Trampo = ITexturesClasses.Trampo;
        Jetpack = ITexturesClasses.Jetpack;
        StaticEnemy = ITexturesClasses.StaticEnemy;
        MovingEnemy = ITexturesClasses.MovingEnemy;
        EasyMovingEnemy = ITexturesClasses.EasyMovingEnemy;
        MyInputField = ITexturesClasses.MyInputField;

        _contentLoaded = true;
        _stopwatch.Stop();
    }

    protected override void Update(GameTime gameTime)
    {
        _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        MouseExtended.Current.GetState(gameTime);
        var _mousePos = windowbox.GetCorrectMousePos(MouseExtended.Current.CurrentState);
        if (_contentLoaded)
        {
            // Debug.WriteLine(Player.Speed.Y);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            _mouseState = Mouse.GetState();
            _k = Keyboard.GetState();
            Pointer.Position = new Vector2(_mousePos.X - 10, _mousePos.Y - 10);

            switch (CurrentGameState)
            {
                case ClassEnums.GameStateEnum.InputName:
                {
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Pointer.GetAnimatedSprite.Play("shoot");
                        if (_mousePos.X is > 150 and < 335)
                            if (_mousePos.Y is > 510 and < 565)
                                if (MyInputField.Text.Length > 0)
                                {
                                    CurrentGameState = ClassEnums.GameStateEnum.IntroMenu;
                                    _playerName = MyInputField.Text.ToString();
                                    Thread.Sleep(100);
                                }

                        if (_mousePos.X is > 295 and < 415)
                            if (_mousePos.Y is > 620 and < 675)
                                Exit();
                    }
                }
                    break;
                case ClassEnums.GameStateEnum.About:
                {
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Pointer.GetAnimatedSprite.Play("shoot");
                        if (_mousePos.X is > 291 and < 411)
                            if (_mousePos.Y is > 621 and < 678)
                            {
                                CurrentGameState = ClassEnums.GameStateEnum.IntroMenu;
                                Thread.Sleep(100);
                            }
                    }
                }
                    break;
                case ClassEnums.GameStateEnum.GameRunning:
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
                    Trampo.Update(Score, Spring, Jetpack, BoardsList,
                        Player, CollisionCheck, ThingsCollisionCheck);

                    //spring
                    Spring.Update(Score, Trampo, Jetpack, BoardsList,
                        Player, CollisionCheck, ThingsCollisionCheck);

                    //jetpack
                    Jetpack.Update(Score, Spring, Trampo, BoardsList,
                        Player, CollisionCheck, ThingsCollisionCheck);

                    //movingEnemy
                    if (GameMode == ClassEnums.GameModeEnum.Hard)
                    {
                        MovingEnemy.Move();
                        MovingEnemy.Update(Bullet, BulletEnemy, Sound, Player, CurrentGameState,
                            ref CollisionCheck, Background.SoundEffectCheck);
                    }
                    else if (GameMode == ClassEnums.GameModeEnum.Easy)
                    {
                        EasyMovingEnemy.Update(Bullet, Sound, Player, ref GameOver, ref CollisionCheck,
                            ThingsCollisionCheck, Background.SoundEffectCheck);
                    }

                    //static enemy
                    StaticEnemy.Update(Bullet, BoardsList, Sound, Player, ref GameOver, ref CollisionCheck,
                        Score, ThingsCollisionCheck, Trampo, Jetpack,
                        Spring, Background.SoundEffectCheck);

                    //to move boards_list and background with player
                    GameRenderer.MoveWithPlayer();

                    // if (Bullet.Position.Intersects(BulletEnemy.Position) ||
                    //     BulletEnemy.Position.Intersects(Bullet.Position))
                    // {
                    //     BulletEnemy.IsCheck = false;
                    //     Bullet.IsCheck = false;
                    // }

                    if (GameMode == ClassEnums.GameModeEnum.Hard)
                        if (Bullet.BulletCollision(BulletEnemy))
                        {
                            BulletEnemy.IsCheck = false;
                            Bullet.IsCheck = false;
                        }

                    //check side of background
                    Background.SideCheck();

                    //reposition items
                    GameRenderer.RePosition();

                    //to check boards_list coliision
                    BoardsList.Collision(ThingsCollisionCheck, CollisionCheck, GameOver, Player, Sound,
                        Background.SoundEffectCheck);

                    //to go to pause menue bye esc clicking
                    _kTemp1 = Keyboard.GetState();
                    if (_kTemp1.IsKeyDown(Keys.Escape) && !_kTemp.IsKeyDown(Keys.Escape))
                    {
                        CurrentGameState = ClassEnums.GameStateEnum.Pause;
                        break;
                    }

                    //to move left and right
                    _kTemp = _kTemp1;
                    if ((_k.IsKeyDown(Keys.Left) || _k.IsKeyDown(Keys.A)) && !GameOver)
                    {
                        PlayerOrientation = ClassEnums.PlayerOrientEnum.Left;
                        Player.PlayerPosition = new Rectangle(Player.PlayerPosition.X - 7, Player.PlayerPosition.Y,
                            Player.PlayerPosition.Width, Player.PlayerPosition.Height);
                        Player.ShootPosition = new Rectangle(Player.PlayerPosition.X + Player.PlayerPosition.Width / 2,
                            Player.PlayerPosition.Y + Player.PlayerPosition.Height / 2 + 15, Player.ShootPosition.Width,
                            Player.ShootPosition.Height);
                        Player.FirePosition = new Vector2(Player.PlayerPosition.X,
                            Player.PlayerPosition.Y + Player.PlayerPosition.Height);
                    }
                    else if ((_k.IsKeyDown(Keys.Right) || _k.IsKeyDown(Keys.D)) && !GameOver)
                    {
                        PlayerOrientation = ClassEnums.PlayerOrientEnum.Right;
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
                        CurrentGameState == ClassEnums.GameStateEnum.GameRunning)
                    {
                        if (_mousePos.X is > 420 and < 470 && _mousePos.Y is > 5 and < 40)
                        {
                            Pointer.GetAnimatedSprite.Play("shoot");
                            CurrentGameState = ClassEnums.GameStateEnum.Pause;
                            MediaPlayer.Pause();
                        }
                        //to shoot tir
                        else
                        {
                            if (!Bullet.IsCheck)
                            {
                                var del = _mousePos.X - Player.PlayerPosition.X - 30;
                                if (del == 0) del = 1;
                                // ReSharper disable once PossibleLossOfFraction
                                Player.Degree = (float)Math.Atan(-(_mousePos.Y - Player.PlayerPosition.Y - 27) / del);
                                Bullet.Position = new Rectangle(Player.PlayerPosition.X + 30,
                                    Player.PlayerPosition.Y + 27, Bullet.Position.Width, Bullet.Position.Height);
                                Pointer.GetAnimatedSprite.Play("shoot");
                                if (_mousePos.X < Player.PlayerPosition.X + 30)
                                    Bullet.Speed = new Vector2(-25 * (float)Math.Cos(Player.Degree),
                                        +25 * (float)Math.Sin(Player.Degree));
                                else
                                    Bullet.Speed = new Vector2(25 * (float)Math.Cos(Player.Degree),
                                        -25 * (float)Math.Sin(Player.Degree));

                                Bullet.IsCheck = true;
                                if (Background.SoundEffectCheck) Sound.PlayerShoot.Play();
                            }
                        }
                    }

                    if (Bullet.Position.Y > 740 || Bullet.Position.X is < -20 or > 500 || Bullet.Position.Y < -20)
                        Bullet.IsCheck = false;
                    if (Bullet.IsCheck && CurrentGameState == ClassEnums.GameStateEnum.GameRunning)
                        Bullet.Move();

                    var mouseControl = Mouse.GetState();
                    Player.ShootDegree = -(float)Math.Atan2(_mousePos.X - Player.PlayerPosition.X,
                        _mousePos.Y - Player.PlayerPosition.Y);

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
                        CurrentGameState = ClassEnums.GameStateEnum.GameOver;
                        if (CollisionCheck && Background.SoundEffectCheck) Sound.Dead.Play();
                    }
                }
                    break;
                case ClassEnums.GameStateEnum.Pause:
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (_mousePos.X is > 131 and < 251)
                            if (_mousePos.Y is > 372 and < 428)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = ClassEnums.GameStateEnum.GameRunning;
                                MediaPlayer.Resume();
                                Thread.Sleep(100);
                            }

                        if (_mousePos.X is > 215 and < 335)
                            if (_mousePos.Y is > 454 and < 510)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = ClassEnums.GameStateEnum.IntroMenu;
                                PlayerOrientation = ClassEnums.PlayerOrientEnum.Right;
                                Thread.Sleep(100);
                            }

                        if (_mousePos.X is > 66 and < 186)
                            if (_mousePos.Y is > 282 and < 338)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = ClassEnums.GameStateEnum.Option;
                                PlayerOrientation = ClassEnums.PlayerOrientEnum.Right;
                                Thread.Sleep(100);
                            }
                    }

                    _kTemp = Keyboard.GetState();
                    if (_kTemp.IsKeyDown(Keys.Escape) && !_kTemp1.IsKeyDown(Keys.Escape))
                        CurrentGameState = ClassEnums.GameStateEnum.GameRunning;
                    _kTemp1 = _kTemp;
                    Background.GameStateCheck = false;
                    break;
                case ClassEnums.GameStateEnum.Option:
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (_mousePos.X is > 318 and < 436)
                            if (_mousePos.Y is > 542 and < 597)
                                if (Background.GameStateCheck)
                                {
                                    Pointer.GetAnimatedSprite.Play("shoot");
                                    CurrentGameState = ClassEnums.GameStateEnum.IntroMenu;
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    Pointer.GetAnimatedSprite.Play("shoot");
                                    CurrentGameState = ClassEnums.GameStateEnum.Pause;
                                    Thread.Sleep(100);
                                }

                        if (_mousePos.X is > 268 and < 355)
                            if (_mousePos.Y is > 364 and < 416)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                Background.SoundCheck = true;
                            }

                        if (_mousePos.X is > 159 and < 246)
                            if (_mousePos.Y is > 364 and < 416)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                Background.SoundCheck = false;
                            }

                        if (_mousePos.X is > 268 and < 355)
                            if (_mousePos.Y is > 484 and < 537)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                Background.SoundEffectCheck = true;
                            }

                        if (_mousePos.X is > 159 and < 246)
                            if (_mousePos.Y is > 484 and < 537)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                Background.SoundEffectCheck = false;
                            }
                    }

                    break;
                case ClassEnums.GameStateEnum.IntroMenu:
                    _mouseState = Mouse.GetState();
                    if (_mouseState.LeftButton == ButtonState.Pressed && _mTemp1.LeftButton != ButtonState.Pressed)
                    {
                        Background.GameStateCheck = true;
                        if (_mousePos.X is > 67 and < 185)
                            if (_mousePos.Y is > 283 and < 337)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                _mouseState = _mTemp;
                                CurrentGameState = ClassEnums.GameStateEnum.GameRunning;
                                MediaPlayer.Resume();
                                GameRenderer.PlayAgain();
                                Thread.Sleep(100);
                            }

                        if (_mousePos.X is > 217 and < 335)
                            if (_mousePos.Y is > 454 and < 508)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = ClassEnums.GameStateEnum.Option;
                                Thread.Sleep(100);
                            }

                        if (_mousePos.X is > 130 and < 248)
                            if (_mousePos.Y is > 373 and < 427)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = ClassEnums.GameStateEnum.HScore;
                                Thread.Sleep(100);
                            }

                        if (_mousePos.X is > 292 and < 411)
                            if (_mousePos.Y is > 528 and < 582)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = ClassEnums.GameStateEnum.About;
                                Thread.Sleep(100);
                            }

                        //game mode
                        if (_mousePos.X is > 199 and < 295)
                            if (_mousePos.Y is > 280 and < 342)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                GameMode = ClassEnums.GameModeEnum.Easy;
                            }

                        if (_mousePos.X is > 331 and < 427)
                            if (_mousePos.Y is > 280 and < 342)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                GameMode = ClassEnums.GameModeEnum.Hard;
                            }
                    }

                    if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
                        if (_mousePos.X is > 292 and < 411)
                            if (_mousePos.Y is > 621 and < 675 &&
                                CurrentGameState == ClassEnums.GameStateEnum.IntroMenu)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                Exit();
                            }

                    PlayerMenu.Move();
                    if (PlayerMenu.PlayerPosition.Y > 550)
                        PlayerMenu.Speed = new Vector2(PlayerMenu.Speed.X, -13);

                    //update scores
                    if (GameMode == ClassEnums.GameModeEnum.Easy)
                        Background.UpdateScores(ScoreManagerEasy, _playerName);
                    else if (GameMode == ClassEnums.GameModeEnum.Hard)
                        Background.UpdateScores(ScoreManagerHard, _playerName);

                    break;
                case ClassEnums.GameStateEnum.HScore:
                    _mouseState = Mouse.GetState();
                    if (_mouseState is { LeftButton: ButtonState.Pressed } && (_mousePos.X > 296 && _mousePos.X < 415 && _mousePos.Y > 529 && _mousePos.Y < 584))
                    {
                        Pointer.GetAnimatedSprite.Play("shoot");
                        CurrentGameState = ClassEnums.GameStateEnum.IntroMenu;
                        Thread.Sleep(200);
                    }

                    break;
                case ClassEnums.GameStateEnum.GameOver:
                    if (!Sound.PlayCheck)
                    {
                        // MediaPlayer.Play(Sound.End);
                        // MediaPlayer.IsRepeating = true;
                        MediaPlayer.Stop();
                        Sound.PlayCheck = true;
                        switch (GameMode)
                        {
                            case ClassEnums.GameModeEnum.Easy:
                                ScoreManagerEasy.Add(new Score
                                    {
                                        PlayerName = _playerName,
                                        Value = Score.Score
                                    }
                                    , _playerName);
                                ScoreManager.Save(ScoreManagerEasy, "scores-easy.xml");
                                break;
                            case ClassEnums.GameModeEnum.Hard:
                                ScoreManagerHard.Add(new Score
                                    {
                                        PlayerName = _playerName,
                                        Value = Score.Score
                                    }
                                    , _playerName);
                                ScoreManager.Save(ScoreManagerHard, "scores-hard.xml");
                                break;
                        }
                    }

                    switch (GameMode)
                    {
                        //update scores
                        case ClassEnums.GameModeEnum.Easy:
                            Background.UpdateScores(ScoreManagerEasy, _playerName);
                            break;
                        case ClassEnums.GameModeEnum.Hard:
                            Background.UpdateScores(ScoreManagerHard, _playerName);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (_mousePos.X is > 88 and < 271)
                            if (_mousePos.Y is > 438 and < 500)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                GameRenderer.PlayAgain();
                                MediaPlayer.Stop();
                                Thread.Sleep(100);
                            }

                        if (_mousePos.X is > 284 and < 404)
                            if (_mousePos.Y is > 504 and < 559)
                            {
                                Pointer.GetAnimatedSprite.Play("shoot");
                                CurrentGameState = ClassEnums.GameStateEnum.IntroMenu;
                                MediaPlayer.Stop();
                                PlayerOrientation = ClassEnums.PlayerOrientEnum.Right;
                                Thread.Sleep(100);
                            }

                        _mouseState = _mTemp1;
                    }

                    break;
            }

            MovingEnemy.GetAnimatedSprite.Update(gameTime);
            Player.GetAnimatedSprite.Update(gameTime);
            Pointer.GetAnimatedSprite.Update(gameTime);
            MyInputField.Update(Keyboard.GetState(), Mouse.GetState(), _mousePos);
        }

        if (!_contentLoaded || _elapsedTime <= 5.0)
        {
            _loading.Play("rotate");
            _loading.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        windowbox.Draw(_spriteBatch, DrawAllObjects);

        base.Draw(gameTime);
    }

    private void DrawAllObjects()
    {
        //Debug.WriteLine($"Izmerjen čas: {_stopwatch.Elapsed}");
        _allObjects = 0;
        _visibleObjects = 0;

        if (!_contentLoaded || _elapsedTime <= 3.0)
        {
            _spriteBatch.Draw(_loadingTexture, new Vector2(0, 0), null, Color.White);
            _loading.Draw(_spriteBatch, new Vector2(230, 500), 0f, new Vector2(1, 1));
        }
        else if (CurrentGameState == ClassEnums.GameStateEnum.InputName)
        {
            Background.Draw(_spriteBatch, CurrentGameState, Score, GameMode);
            MyInputField.Draw(_spriteBatch);
            Pointer.Draw(_spriteBatch);
        }
        else
        {
            Background.Draw(_spriteBatch, CurrentGameState, Score, GameMode);

            if (CurrentGameState == ClassEnums.GameStateEnum.GameRunning)
            {
                foreach (var board in BoardsList.BoardList)
                {
                    _allObjects++;
                    if (board.Visible && board.DrawVisible)
                    {
                        board.DrawSprite(_spriteBatch);
                        _visibleObjects++;
                    }
                }

                for (var i = 0; i < 4; i++)
                {
                    _allObjects++;
                    if (BoardsList.MovingBoardList[i].DrawVisible)
                    {
                        _visibleObjects++;
                        BoardsList.MovingBoardList[i].DrawSprite(_spriteBatch);
                    }

                    _allObjects++;
                    if (BoardsList.FakeBoardList[i].Visible && BoardsList.FakeBoardList[i].DrawVisible)
                    {
                        _visibleObjects++;
                        BoardsList.FakeBoardList[i].DrawSprite(_spriteBatch);
                    }

                    _allObjects++;
                    if (BoardsList.GoneBoardList[i].Visible && BoardsList.GoneBoardList[i].DrawVisible)
                    {
                        _visibleObjects++;
                        BoardsList.GoneBoardList[i].DrawSprite(_spriteBatch);
                    }
                }

                _allObjects++;
                if (Trampo.Visible && Trampo.DrawVisible)
                {
                    Trampo.Draw(_spriteBatch);
                    _visibleObjects++;
                }

                _allObjects++;
                if (Spring.Visible && Spring.DrawVisible)
                {
                    Spring.Draw(_spriteBatch);
                    _visibleObjects++;
                }

                _allObjects++;
                if (Jetpack.Visible && Jetpack.DrawVisible)
                {
                    Jetpack.Draw(_spriteBatch);
                    _visibleObjects++;
                }

                _allObjects++;
                if (StaticEnemy.DrawVisible)
                {
                    StaticEnemy.Draw(_spriteBatch);
                    _visibleObjects++;
                }

                if (GameMode == ClassEnums.GameModeEnum.Hard)
                {
                    _allObjects++;
                    if (MovingEnemy.Visible)
                    {
                        MovingEnemy.LifeDraw(_spriteBatch);
                        MovingEnemy.Draw(_spriteBatch);
                        _visibleObjects++;
                    }
                }
                else if (GameMode == ClassEnums.GameModeEnum.Easy)
                {
                    _allObjects++;
                    //Debug.WriteLine(EasyMovingEnemy.Position.ToString());
                    if (EasyMovingEnemy.Visible)
                    {
                        EasyMovingEnemy.Draw(_spriteBatch);
                        _visibleObjects++;
                    }
                }

                Background.ScoreDraw(_spriteBatch, CurrentGameState);
                Player.Draw(_spriteBatch, PlayerOrientation, CurrentGameState, CollisionCheck);
                Score.Draw(_spriteBatch, CurrentGameState);
            }
            // Debug.WriteLine($"All objects number: {_allObjects}. Visible objects number: {_visibleObjects} ");

            if (CurrentGameState == ClassEnums.GameStateEnum.IntroMenu)
            {
                MyInputField.DrawName(_spriteBatch);
                PlayerMenu.Draw(_spriteBatch, PlayerOrientation, ClassEnums.GameStateEnum.IntroMenu, CollisionCheck);
            }

            if (Bullet.IsCheck) Bullet.Draw(_spriteBatch, CurrentGameState);

            if (BulletEnemy.IsCheck && GameMode == ClassEnums.GameModeEnum.Hard)
                BulletEnemy.Draw(_spriteBatch, CurrentGameState);

            Pointer.Draw(_spriteBatch);
        }
    }

    // private string RandomString(int length)
    // {
    //     var random = new Random();
    //     const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    //     return new string(Enumerable.Repeat(chars, length)
    //         .Select(s => s[random.Next(s.Length)]).ToArray());
    // }
}