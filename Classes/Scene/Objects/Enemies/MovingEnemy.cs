using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using VirusJump.Classes.Scene.Objects.Supplements;
using VirusJump.Classes.Graphics;
using VirusJump.Classes.Scene.Objects.Boards;
using VirusJump.Classes.Scene.Objects.Jumpers;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;

namespace VirusJump.Classes.Scene.Objects.Enemies
{
    public class MovingEnemy
    {

        private Texture2D _movingEnemy;
        private Rectangle _position;
        private bool _visible;
        private int _stRand;
        private Vector2 _speed;

        public MovingEnemy(ContentManager content)
        {
            _movingEnemy = content.Load<Texture2D>("assets/e3");
            Initialize();
        }

        public void Initialize()
        {
            _stRand = -1;
            _position = new Rectangle(20, 750, 60, 65);
            _speed = new Vector2(3, 0);
            _visible = false;
        }

        public void Draw(SpriteBatch s)
        {
            if (_speed.X > 0)
                s.Draw(_movingEnemy, _position, null, Color.White, 0f, Vector2.Zero,SpriteEffects.None, 0);
            else
                s.Draw(_movingEnemy, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
        }

        public void move()
        {
            _position.X += (int)_speed.X;
            if (_position.X > 410)
                _speed.X *= -1;
            if (_position.X < 10)
                _speed.X *= -1;
        }

        public int Collision(Player player, bool collisionCheck)
        {
            if (_position.Y - player.PlayerPosition.Y - 45 < 5 && _position.Y - player.PlayerPosition.Y - 45 > -15 && player.Speed.Y > 0 && ((player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + player.PlayerPosition.Width) || (player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + player.PlayerPosition.Width)))
            {
                return (0);// our enemy is dead!
            }
            else if (_position.Y - player.PlayerPosition.Y < 5 && _position.Y - player.PlayerPosition.Y > -35 && player.Speed.Y < 0 && ((player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + player.PlayerPosition.Height) || (player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + player.PlayerPosition.Height)))
            {
                collisionCheck = false;
                return (1);// you are dead!
            }
            else
            {
                collisionCheck = true;
                return (2);
            }
        }

        public bool BulletCollision(Bullet bullet)
        {
            if (bullet.Position.X > _position.X && bullet.Position.X + bullet.Position.Width < _position.X + _position.Width && bullet.Position.Y > _position.Y && bullet.Position.Y + bullet.Position.Height < _position.Y + _position.Height) return true;
            else return false;
        }

        public Rectangle Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public int StRand
        {
            get { return _stRand; }
            set { _stRand = value; }
        }
    }
}
