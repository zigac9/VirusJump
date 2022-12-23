using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

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

    protected static async Task GenerateThreads(ContentManager content)
    {
        _content = content;
        var tasks = new List<Task>
        {
            //player
            Task.Run(() => LoadTexture("assets/DoodleR1", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/injection", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/manjetpack", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/manjetpackL", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/dead", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/fire.sf", LoadTextureEnum.spriteSheet)),

            //background
            Task.Run(() => LoadTexture("assets/gradient", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/kooh", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/mainMenu1", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/option", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/sOn", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/sOff", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/notif", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/pause", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/sides", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/gameOver", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/highscore", LoadTextureEnum.texture)),

            //bullet
            Task.Run(() => LoadTexture("assets/tir", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/virus", LoadTextureEnum.texture)),

            //trampo
            Task.Run(() => LoadTexture("assets/toshak", LoadTextureEnum.texture)),

            //Spring
            Task.Run(() => LoadTexture("assets/fanar", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/oFanar", LoadTextureEnum.texture)),

            //jetpack
            Task.Run(() => LoadTexture("assets/jet", LoadTextureEnum.texture)),

            //staticEnemy
            Task.Run(() => LoadTexture("assets/ena", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/sedem", LoadTextureEnum.texture)),

            //movingEnemy
            Task.Run(() => LoadTexture("assets/tri", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/stiri", LoadTextureEnum.texture)),
            Task.Run(() => LoadTexture("assets/pet", LoadTextureEnum.texture)),

            //board
            Task.Run(() => LoadTexture("assets/p1", LoadTextureEnum.texture)),

            //fakeboard
            Task.Run(() => LoadTexture("assets/p3", LoadTextureEnum.texture)),

            //goneboard
            Task.Run(() => LoadTexture("assets/p4", LoadTextureEnum.texture)),

            //movingboard
            Task.Run(() => LoadTexture("assets/p2", LoadTextureEnum.texture)),

            //pointer
            Task.Run(() => LoadTexture("assets/shoot.sf", LoadTextureEnum.spriteSheet)),

            //Sound
            Task.Run(() => LoadTexture("assets/background", LoadTextureEnum.song)),
            Task.Run(() => LoadTexture("assets/patmat", LoadTextureEnum.song)),
            Task.Run(() => LoadTexture("assets/jump", LoadTextureEnum.soundEffect)),
            Task.Run(() => LoadTexture("assets/shootPlayer", LoadTextureEnum.soundEffect)),
            Task.Run(() => LoadTexture("assets/enemyShot", LoadTextureEnum.soundEffect)),
            Task.Run(() => LoadTexture("assets/deadSound", LoadTextureEnum.soundEffect)),
            
            //ScorClass
            Task.Run(() => LoadTexture("assets/SpriteFont1", LoadTextureEnum.spriteFonts))

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
                case LoadTextureEnum.texture:
                {
                    var texture = _content.Load<Texture2D>(textureName);
                    lock (TexturesLock)
                    {
                        TexturesLoad.Add(textureName, texture);
                    }

                    break;
                }
                case LoadTextureEnum.song:
                {
                    var texture = _content.Load<Song>(textureName);
                    lock (SongsLock)
                    {
                        SongsLoad.Add(textureName, texture);
                    }

                    break;
                }
                case LoadTextureEnum.soundEffect:
                {
                    var texture = _content.Load<SoundEffect>(textureName);
                    lock (SoundEffectsLock)
                    {
                        SoundEffectsLoad.Add(textureName, texture);
                    }

                    break;
                }
                case LoadTextureEnum.spriteFonts:
                {
                    var texture = _content.Load<SpriteFont>(textureName);
                    lock (SpriteFontsLock)
                    {
                        SpriteFontsLoad.Add(textureName, texture);
                    }

                    break;
                }
                case LoadTextureEnum.spriteSheet:
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
        texture = 0,
        spriteSheet,
        song,
        soundEffect,
        spriteFonts
    }
}