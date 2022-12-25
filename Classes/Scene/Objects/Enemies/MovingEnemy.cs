using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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

    public void Update(Bullet bullet, Bullet bulletEnemy, Sound sound, Player player, Game1.GameStateEnum currentGameState, ref bool collisionCheck)
    {
        if (BulletCollision(bullet))
        {
            MvRand = true;
            MvCollision = false;
            Visible = false;
        }
        
        if (Math.Abs(Position.X - player.PlayerPosition.X) < 10 && Position.Y > 0)
            if (!bulletEnemy.IsCheck)
            {
                Degree =
                    // ReSharper disable once PossibleLossOfFraction
                    (float)Math.Atan(-(player.PlayerPosition.Y - 30 - Position.Y) /
                                     (player.PlayerPosition.X - 30 - Position.X));
                bulletEnemy.Position = new Rectangle(Position.X + 30,
                    Position.Y + 30, bulletEnemy.Position.Width, bulletEnemy.Position.Height);
                if (player.PlayerPosition.X < Position.X + 30)
                    bulletEnemy.Speed = new Vector2(-1 * (float)Math.Cos(Degree),
                        +1 * (float)Math.Sin(Degree));
                else
                    bulletEnemy.Speed = new Vector2(1 * (float)Math.Cos(Degree),
                        -1 * (float)Math.Sin(Degree));
        
                bulletEnemy.IsCheck = true;
                sound.EnemyShoot.Play();
            }
        
        if (bulletEnemy.Position.Y > 740 || bulletEnemy.Position.X is < -20 or > 500 ||
            bulletEnemy.Position.Y < -20)
            bulletEnemy.IsCheck = false;
        if (bulletEnemy.IsCheck && currentGameState == Game1.GameStateEnum.GameRunning)
            bulletEnemy.Move();
        
        if (bulletEnemy.IsCheck && player.BulletCollision(bulletEnemy))
        {
            player.Speed = new Vector2(player.Speed.X, 0);
            MediaPlayer.Stop();
            sound.Dead.Play();
            bulletEnemy.IsCheck = false;
            collisionCheck = false;
        }
    }

    private bool BulletCollision(Bullet bullet)
    {
        if (bullet.Position.X > _position.X &&
            bullet.Position.X + bullet.Position.Width < _position.X + _position.Width &&
            bullet.Position.Y > _position.Y &&
            bullet.Position.Y + bullet.Position.Height < _position.Y + _position.Height) return true;
        return false;
    }
}