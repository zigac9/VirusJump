using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects.Supplements
{
    public class Bullet
    {
        private Texture2D _texture;
        private Texture2D _texture2;
        private List<Texture2D> _shootList;

        private Rectangle _position;
        private Vector2 _speed;

        private float _accelertion;
        private bool _bullcheck;
        private int _textureRnd;

        public Bullet(ContentManager content, int rnd) 
        {
            _textureRnd = rnd;
            _texture = content.Load<Texture2D>("assets/tir");
            _texture2 = content.Load<Texture2D>("assets/virus");
            _shootList = new List<Texture2D> { _texture, _texture2 };            
            _accelertion = 0.5f;
            _speed = new Vector2(0, 0);
            Initialize();
        }

        public void Initialize()
        {
            _bullcheck = false;
            _position = new Rectangle(-50, -50, 20, 20);
        }

        public void Draw(SpriteBatch s, gameStateEnum gameState)
        {
            if (gameState == gameStateEnum.gameRunning)
                s.Draw(_shootList[_textureRnd], _position, Color.White);
        }

        public void Move()
        {
            _speed.Y += _accelertion;
            _position.Y += (int)_speed.Y;
            _position.X += (int)_speed.X;
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

        public bool IsCheck
        {
            get { return _bullcheck; }
            set { _bullcheck = value; }
        }

    }
}
