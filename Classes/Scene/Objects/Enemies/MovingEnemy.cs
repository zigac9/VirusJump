using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VirusJump.Classes.Scene.Objects.Supplements;

namespace VirusJump.Classes.Scene.Objects.Enemies;

public class MovingEnemy
{
    private readonly List<Texture2D> _enemylist;

    private Rectangle _position;
    private Vector2 _speed;

    public MovingEnemy(IReadOnlyDictionary<string, Texture2D> textures)
    {
        TextureRand = 0;
        _enemylist = new List<Texture2D> { textures["assets/tri"], textures["assets/stiri"], textures["assets/pet"] };
        Initialize();
    }

    public Rectangle Position
    {
        get => _position;
        set => _position = value;
    }

    public bool Visible { get; set; }

    public bool MvRand { get; set; }

    public bool MvCollision { get; set; }

    public int Start { get; set; }

    public int End { get; set; }

    public int View { get; set; }

    public int Step { get; set; }

    public float Degree { get; set; }

    public int TextureRand { get; set; }

    public void Initialize()
    {
        Degree = 0;
        Start = 2000;
        End = 3000;
        View = 1000;
        Step = 4000;
        MvCollision = false;
        MvRand = false;
        _position = new Rectangle(20, -200, 80, 60);
        _speed = new Vector2(3, 0);
        Visible = false;
    }

    public void Draw(SpriteBatch s)
    {
        if (_speed.X > 0)
            s.Draw(_enemylist[TextureRand], _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
        else
            s.Draw(_enemylist[TextureRand], _position, null, Color.White, 0f, Vector2.Zero,
                SpriteEffects.FlipHorizontally, 0);
    }

    public void Move()
    {
        _position.X += (int)_speed.X;
        if (_position.X > 410)
            _speed.X *= -1;
        if (_position.X < 10)
            _speed.X *= -1;
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