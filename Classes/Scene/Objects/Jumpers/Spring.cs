using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusJump.Classes.Scene.Objects.Jumpers;

public class Spring
{
    private readonly Dictionary<string, Texture2D> _textures;
    private Rectangle _position;

    public Spring(Dictionary<string, Texture2D> textures)
    {
        _textures = textures;
        Initialize();
    }

    public Rectangle SpringPosition
    {
        get => _position;
        set => _position = value;
    }

    public bool Visible { get; set; }

    public bool SCheck { get; set; }

    public int SRand { get; set; }

    public int ScoreToMove { get; set; }

    public int ScoreMoveStep { get; set; }

    public void Initialize()
    {
        Visible = false;
        ScoreToMove = 200;
        ScoreMoveStep = 500;
        _position = new Rectangle(-100, 730, 20, 30);
        SRand = -1;
        SCheck = false;
    }

    public void Draw(SpriteBatch s)
    {
        s.Draw(_textures["assets/fanar"], _position, Color.White);
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
                player.Speed.Y > 0) return collisionCheck;
            return false;
        }

        return false;
    }
}