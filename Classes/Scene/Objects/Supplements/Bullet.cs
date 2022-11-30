using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VirusJump.Classes.Scene.Objects.Supplements
{
    public class Bullet
    {
        private Texture2D _texture;
        private Rectangle _position;
        private Vector2 _speed;
        private float _accelertion;

        public Bullet(ContentManager content) 
        {
            _texture = content.Load<Texture2D>("Doodle_jumpContent/tir");
            _accelertion = 0.5f;
            _speed = new Vector2(0, 0);
            Initialize();
        }

        public void Initialize()
        {
            _position = new Rectangle(-50, -50, 20, 20);
        }

        public void Draw(SpriteBatch s, int game)
        {
            if (game == 1)
                s.Draw(_texture, _position, Color.White);
        }

        public void Move()
        {
            _speed.Y += _accelertion;
            _position.Y += (int)_speed.Y;
            _position.X += (int)_speed.X;
            Debug.WriteLine(_position.ToString());
        }

        public Rectangle BulletPosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 BulletSpeed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public float BulletAcceleration
        {
            get { return _accelertion; }
            set { _accelertion = value; }
        }

    }
}
