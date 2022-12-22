using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusJump.Classes.Scene.Objects.Jumpers;

public class Trampo
{
    private readonly Dictionary<string, Texture2D> _textures;
    private Rectangle _position;

    public Trampo(Dictionary<string, Texture2D> textures)
    {
        _textures = textures;
        Initialize();
    }

    public Rectangle TrampoPosition
    {
        get => _position;
        set => _position = value;
    }

    public int TRand { get; set; }

    public bool Visible { get; set; }

    public bool Check { get; set; }

    public int ScoreToMove { get; set; }

    public int ScoreMoveStep { get; set; }

    public void Initialize()
    {
        ScoreToMove = 1000;
        ScoreMoveStep = 700;
        TRand = -1;
        Visible = false;
        Check = false;
        _position = new Rectangle(-100, 730, 40, 18);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_textures["assets/toshak"], _position, Color.White);
    }

    public bool Collision(Player player, bool collisionCheck)
    {
        if ((player.PlayerPosition.X + 10 > _position.X &&
             player.PlayerPosition.X + 10 < _position.X + player.PlayerPosition.Width) ||
            (player.PlayerPosition.X + player.PlayerPosition.Width - 10 > _position.X &&
             player.PlayerPosition.X + player.PlayerPosition.Width - 10 < _position.X + player.PlayerPosition.Width))
        {
            if (_position.Y + _position.Height - player.PlayerPosition.Y - player.PlayerPosition.Height < 5 &&
                _position.Y + _position.Height - player.PlayerPosition.Y - player.PlayerPosition.Height > -15 &&
                player.Speed.Y > 0)
            {
                if (collisionCheck)
                    return true;
                return false;
            }

            return false;
        }

        return false;
    }
}