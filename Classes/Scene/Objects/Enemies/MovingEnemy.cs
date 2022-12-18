using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VirusJump.Classes.Scene.Objects.Supplements;
using Microsoft.Xna.Framework.Content;

namespace VirusJump.Classes.Scene.Objects.Enemies
{
    public class MovingEnemy
    {
        private Texture2D _movingEnemy1;
        private Texture2D _movingEnemy2;
        private Texture2D _movingEnemy3;
        private List<Texture2D> _enemylist;

        private Rectangle _position;
        private Vector2 _speed;

        private bool _visible;
        private bool _mvRand;
        private bool _meCollision;

        private int _startView;
        private int _endView;
        private int _viewEnemy;
        private int _stepView;
        private int _textureint;
        private float _degree;

        public MovingEnemy(ContentManager content)
        {
            _textureint = 0;
            _movingEnemy1 = content.Load<Texture2D>("assets/tri");
            _movingEnemy2 = content.Load<Texture2D>("assets/stiri");
            _movingEnemy3 = content.Load<Texture2D>("assets/pet");
            _enemylist = new List<Texture2D> {_movingEnemy1,_movingEnemy2,_movingEnemy3 };
            Initialize();
        }

        public void Initialize()
        {
            _degree = 0;
            _startView = 100;
            _endView = 3000;
            _viewEnemy = 1000;
            _stepView = 4000;
            _meCollision = false;
            _mvRand = false;
            _position = new Rectangle(20, 800, 80, 60);
            _speed = new Vector2(3, 0);
            _visible = false;
        }

        public void Draw(SpriteBatch s)
        {
            if (_speed.X > 0)
                s.Draw(_enemylist[_textureint], _position, null, Color.White, 0f, Vector2.Zero,SpriteEffects.None, 0);
            else
                s.Draw(_enemylist[_textureint], _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
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
            if (bullet.Position.X > _position.X && bullet.Position.X + bullet.Position.Width < _position.X + _position.Width && bullet.Position.Y > _position.Y && bullet.Position.Y + bullet.Position.Height < _position.Y + _position.Height) return true;
            else return false;
        }

        public Rectangle Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public bool MvRand
        {
            get { return _mvRand; }
            set { _mvRand = value; }
        }

        public bool MvCollision
        {
            get { return _meCollision; }
            set { _meCollision = value; }
        }

        public int Start
        {
            get { return _startView; }
            set { _startView = value; }
        }

        public int End
        {
            get { return _endView; }
            set { _endView = value; }
        }

        public int View
        {
            get { return _viewEnemy; }
            set { _viewEnemy = value; }
        }

        public int Step
        {
            get { return _stepView; }
            set { _stepView = value; }
        }

        public float Degree
        {
            get { return _degree; }
            set { _degree = value; }
        }
        public int TextureRand
        {
            get { return _textureint; }
            set { _textureint = value; }
        }
    }
}
