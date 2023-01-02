﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Sprites;
using VirusJump.Classes.Scene.Objects.Supplements;

namespace VirusJump.Classes.Scene.Objects.Enemies;

public class MovingEnemy
{
    private readonly List<Texture2D> _enemyList;
    private Vector2 _bullFirePosition;
    private bool _drawFire;
    private int _life;

    private Rectangle _position;
    private Vector2 _speed;

    public MovingEnemy(IReadOnlyDictionary<string, Texture2D> textures,
        IReadOnlyDictionary<string, SpriteSheet> spriteSheets)
    {
        TextureRand = 0;
        _enemyList = new List<Texture2D> { textures["assets/tri"], textures["assets/stiri"], textures["assets/pet"] };
        GetAnimatedSprite = new AnimatedSprite(spriteSheets["assets/fire.sf"]);
        Initialize();
    }

    public Rectangle Position
    {
        get => _position;
        set => _position = value;
    }

    public AnimatedSprite GetAnimatedSprite { get; }

    public bool Visible { get; set; }

    public int Start { get; set; }

    public int End { get; set; }

    public int View { get; private set; }

    public int Step { get; private set; }

    private float Degree { get; set; }

    public int TextureRand { get; set; }

    public void Initialize()
    {
        _bullFirePosition = new Vector2(0, 0);
        _drawFire = false;
        Degree = 0;
        //TODO popravi ko bo za konc
        Start = 2000;
        End = 3000;
        View = 1000;
        Step = 4000;
        _position = new Rectangle(20, -200, 80, 60);
        _speed = new Vector2(3, 0);
        Visible = false;
        _life = 2;
    }

    public void Draw(SpriteBatch s)
    {
        s.Draw(_enemyList[TextureRand], _position, null, Color.White, 0f, Vector2.Zero,
            _speed.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

        if (_drawFire)
            GetAnimatedSprite.Draw(s, _bullFirePosition, 10f, new Vector2(3, 3));
    }

    public void Move()
    {
        _position.X += (int)_speed.X;
        if (_position.X > 410)
            _speed.X *= -1;
        if (_position.X < 10)
            _speed.X *= -1;
    }

    public void Update(Bullet bullet, Bullet bulletEnemy, Sound sound, Player player,
        Game1.GameStateEnum currentGameState, ref bool collisionCheck)
    {
        if (_life > 0 && BulletCloseCollision(bullet))
        {
            bullet.IsCheck = false;
            GetAnimatedSprite.Play("fire");
            _drawFire = true;
            _bullFirePosition = new Vector2(bullet.Position.X, bullet.Position.Y);
            _life--;
        }
        else
        {
            _drawFire = false;
            if (BulletCollision(bullet))
            {
                Visible = false;
                _life = 2;
            }

            if (Math.Abs(Position.X - player.PlayerPosition.X) < 10 && Position.Y > 0 && Visible)
                if (!bulletEnemy.IsCheck)
                {
                    Degree =
                        // ReSharper disable once PossibleLossOfFraction
                        (float)Math.Atan(-(player.PlayerPosition.Y - 30 - Position.Y) /
                                         (player.PlayerPosition.X - 30 - Position.X));
                    bulletEnemy.Position = new Rectangle(Position.X + 30,
                        Position.Y + 30, bulletEnemy.Position.Width, bulletEnemy.Position.Height);
                    if (player.PlayerPosition.X < Position.X + 30)
                        bulletEnemy.Speed = new Vector2(-1 * (float)Math.Cos(Degree), (float)
                            0.5 * (float)Math.Sin(Degree));
                    else
                        bulletEnemy.Speed = new Vector2(1 * (float)Math.Cos(Degree), (float)
                            0.5 * (float)Math.Sin(Degree));

                    bulletEnemy.IsCheck = true;
                    sound.EnemyShoot.Play();
                }
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
            _life = 2;
        }
    }

    private bool BulletCollision(Bullet bullet)
    {
        if (bullet.IsCheck)
            return bullet.Position.Intersects(_position) || _position.Intersects(bullet.Position);
        return false;
    }

    private bool BulletCloseCollision(Bullet bullet)
    {
        if (bullet.Position.X > _position.X - 15 &&
            bullet.Position.X + bullet.Position.Width < _position.X + _position.Width + 15 &&
            bullet.Position.Y > _position.Y &&
            bullet.Position.Y + bullet.Position.Height < _position.Y + _position.Height + 50) return true;
        return false;
    }
}