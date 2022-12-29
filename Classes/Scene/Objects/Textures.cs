using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using VirusJump.Classes.Scene.Objects.Boards;
using VirusJump.Classes.Scene.Objects.Enemies;
using VirusJump.Classes.Scene.Objects.Jumpers;
using VirusJump.Classes.Scene.Objects.Scoring;
using VirusJump.Classes.Scene.Objects.Supplements;

namespace VirusJump.Classes.Scene.Objects;

internal interface ITexturesClasses
{
    private static ContentManager _content;

    private static readonly object TexturesLock = new();
    private static readonly object SongsLock = new();
    private static readonly object SoundEffectsLock = new();
    private static readonly object SpriteFontsLock = new();
    private static readonly object SpriteSheetsLock = new();
    private static readonly object LoadTextureLock = new();
    protected static Dictionary<string, Texture2D> TexturesLoad { get; } = new();
    protected static Dictionary<string, SpriteSheet> SpriteSheetsLoad { get; } = new();
    protected static Dictionary<string, Song> SongsLoad { get; } = new();
    protected static Dictionary<string, SoundEffect> SoundEffectsLoad { get; } = new();
    protected static Dictionary<string, SpriteFont> SpriteFontsLoad { get; } = new();

    protected static ScorClass Score { get; set; }
    protected static Player Player { get; set; }
    protected static Player PlayerMenu { get; set; }
    protected static BoardsList BoardsList { get; set; }
    protected static Background Background { get; set; }
    protected static Pointer Pointer { get; set; }
    protected static Bullet Bullet { get; set; }
    protected static Bullet BulletEnemy { get; set; }
    protected static Trampo Trampo { get; set; }
    protected static Spring Spring { get; set; }
    protected static Jetpack Jetpack { get; set; }
    protected static StaticEnemy StaticEnemy { get; set; }
    protected static MovingEnemy MovingEnemy { get; set; }
    protected static Sound Sound { get; set; }
    protected static ScoreManager ScoreManager { get; set; }


    protected static async Task GenerateThreadsTextures(ContentManager content)
    {
        lock (LoadTextureLock)
        {
            _content = content;
        }
        var tasks = new List<Task>
        {
            //player
            Task.Run(() => LoadTexture("assets/DoodleR1", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/injection", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/manjetpack", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/manjetpackL", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/dead", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/fire.sf", LoadTextureEnum.SpriteSheet)),

            //background
            Task.Run(() => LoadTexture("assets/gradient", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/kooh", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/mainMenu1", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/option", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/sOn", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/sOff", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/notif", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/pause", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/sides", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/gameOver", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/highscore", LoadTextureEnum.Texture)),

            //bullet
            Task.Run(() => LoadTexture("assets/tir", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/virus", LoadTextureEnum.Texture)),

            //trampo
            Task.Run(() => LoadTexture("assets/toshak", LoadTextureEnum.Texture)),

            //Spring
            Task.Run(() => LoadTexture("assets/fanar", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/oFanar", LoadTextureEnum.Texture)),

            //jetpack
            Task.Run(() => LoadTexture("assets/jet", LoadTextureEnum.Texture)),

            //staticEnemy
            Task.Run(() => LoadTexture("assets/ena", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/sedem", LoadTextureEnum.Texture)),

            //movingEnemy
            Task.Run(() => LoadTexture("assets/tri", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/stiri", LoadTextureEnum.Texture)),
            Task.Run(() => LoadTexture("assets/pet", LoadTextureEnum.Texture)),

            //board
            Task.Run(() => LoadTexture("assets/p1", LoadTextureEnum.Texture)),

            //fakeboard
            Task.Run(() => LoadTexture("assets/p3", LoadTextureEnum.Texture)),

            //goneboard
            Task.Run(() => LoadTexture("assets/p4", LoadTextureEnum.Texture)),

            //movingboard
            Task.Run(() => LoadTexture("assets/p2", LoadTextureEnum.Texture)),

            //pointer
            Task.Run(() => LoadTexture("assets/shoot.sf", LoadTextureEnum.SpriteSheet)),

            //Sound
            Task.Run(() => LoadTexture("assets/background", LoadTextureEnum.Song)),
            Task.Run(() => LoadTexture("assets/patmat", LoadTextureEnum.Song)),
            Task.Run(() => LoadTexture("assets/jump", LoadTextureEnum.SoundEffect)),
            Task.Run(() => LoadTexture("assets/shootPlayer", LoadTextureEnum.SoundEffect)),
            Task.Run(() => LoadTexture("assets/enemyShot", LoadTextureEnum.SoundEffect)),
            Task.Run(() => LoadTexture("assets/deadSound", LoadTextureEnum.SoundEffect)),

            //ScorClass
            Task.Run(() => LoadTexture("assets/SpriteFont1", LoadTextureEnum.SpriteFonts))
        };
        await Task.WhenAll(tasks);
        await GenerateThreadsClasses();
    }

    private static async Task GenerateThreadsClasses()
    {
        var tasks = new List<Task>
        {
            Task.Run(() => Score = new ScorClass(SpriteFontsLoad)),
            Task.Run(() => Player = new Player(TexturesLoad, SpriteSheetsLoad)),
            Task.Run(() => PlayerMenu = new Player(TexturesLoad, SpriteSheetsLoad)
                { PlayerPosition = new Rectangle(60, 520, 80, 80) }),
            Task.Run(() => BoardsList = new BoardsList(TexturesLoad)),
            Task.Run(() => Background = new Background(TexturesLoad)),
            Task.Run(() => Pointer = new Pointer(SpriteSheetsLoad)),
            Task.Run(() => Bullet = new Bullet(0, TexturesLoad)),
            Task.Run(() => BulletEnemy = new Bullet(1, TexturesLoad)),
            Task.Run(() => Trampo = new Trampo(TexturesLoad)),
            Task.Run(() => Spring = new Spring(TexturesLoad)),
            Task.Run(() => Jetpack = new Jetpack(TexturesLoad)),
            Task.Run(() => StaticEnemy = new StaticEnemy(TexturesLoad)),
            Task.Run(() => MovingEnemy = new MovingEnemy(TexturesLoad, SpriteSheetsLoad)),
            Task.Run(() => Sound = new Sound(SoundEffectsLoad, SongsLoad)),
            Task.Run(() => ScoreManager = ScoreManager.Load())
        };
        await Task.WhenAll(tasks);
    }

    private static void LoadTexture(string textureName, LoadTextureEnum loadTextureEnum)
    {
        // Load the texture using the ContentManager
        lock (LoadTextureLock)
        {
            switch (loadTextureEnum)
            {
                case LoadTextureEnum.Texture:
                {
                    var texture = _content.Load<Texture2D>(textureName);
                    lock (TexturesLock)
                    {
                        TexturesLoad.Add(textureName, texture);
                    }

                    break;
                }
                case LoadTextureEnum.Song:
                {
                    var texture = _content.Load<Song>(textureName);
                    lock (SongsLock)
                    {
                        SongsLoad.Add(textureName, texture);
                    }

                    break;
                }
                case LoadTextureEnum.SoundEffect:
                {
                    var texture = _content.Load<SoundEffect>(textureName);
                    lock (SoundEffectsLock)
                    {
                        SoundEffectsLoad.Add(textureName, texture);
                    }

                    break;
                }
                case LoadTextureEnum.SpriteFonts:
                {
                    var texture = _content.Load<SpriteFont>(textureName);
                    lock (SpriteFontsLock)
                    {
                        SpriteFontsLoad.Add(textureName, texture);
                    }

                    break;
                }
                case LoadTextureEnum.SpriteSheet:
                {
                    var texture = _content.Load<SpriteSheet>(textureName, new JsonContentLoader());
                    lock (SpriteSheetsLock)
                    {
                        SpriteSheetsLoad.Add(textureName, texture);
                    }

                    break;
                }
            }
        }
    }

    private enum LoadTextureEnum
    {
        Texture = 0,
        SpriteSheet,
        Song,
        SoundEffect,
        SpriteFonts
    }
}