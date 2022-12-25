using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VirusJump.Classes.Scene.Objects.Supplements;

namespace VirusJump.Classes.Scene.Objects.Enemies;

public class StaticEnemy
{
    private readonly List<Texture2D> _enemylist;

    private Rectangle _position;

    public StaticEnemy(IReadOnlyDictionary<string, Texture2D> textures)
    {
        TextureRand = 0;
        _enemylist = new List<Texture2D> { textures["assets/ena"], textures["assets/sedem"] };
        Initialize();
    }

    public Rectangle Position
    {
        get => _position;
        set => _position = value;
    }

    public int StRand { get; set; }

    public int TextureRand { get; set; }

    public void Initialize()
    {
        StRand = -1;
        _position = new Rectangle(-200, 800, 60, 55);
    }

    public void Draw(SpriteBatch s)
    {
        s.Draw(_enemylist[TextureRand], _position, Color.White);
    }

    public int Collision(Player player, ref bool collisionCheck)
    {
        //enemy dead
        if (_position.Y - player.PlayerPosition.Y - 45 < 5 && _position.Y - player.PlayerPosition.Y - 45 > -15 &&
            player.Speed.Y > 0 &&
            ((player.PlayerPosition.X + 15 > _position.X &&
              player.PlayerPosition.X + 15 < _position.X + player.PlayerPosition.Width) ||
             (player.PlayerPosition.X + 45 > _position.X &&
              player.PlayerPosition.X + 45 < _position.X + player.PlayerPosition.Width))) return 0;

        //player dead
        if (_position.Y - player.PlayerPosition.Y < 5 && _position.Y - player.PlayerPosition.Y > -35 &&
            player.Speed.Y < 0 &&
            ((player.PlayerPosition.X + 15 > _position.X &&
              player.PlayerPosition.X + 15 < _position.X + player.PlayerPosition.Height) ||
             (player.PlayerPosition.X + 45 > _position.X &&
              player.PlayerPosition.X + 45 < _position.X + player.PlayerPosition.Height)))
            // collisionCheck = false;
            return 1;

        // collisionCheck = true;
        return 2;
    }

    public bool BulletCollision(Bullet bullet)
    {
        if (bullet.Position.X > _position.X &&
            bullet.Position.X + bullet.Position.Width < _position.X + _position.Width &&
            bullet.Position.Y > _position.Y &&
            bullet.Position.Y + bullet.Position.Height < _position.Y + _position.Height) return true;
        return false;
    }
}