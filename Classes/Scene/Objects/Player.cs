using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects
{
    public class Player
    {
        private Texture2D _playerTexture;
        private Texture2D _shootTexture;
        private Texture2D _noseTexture;
        private Rectangle _position;
        private Vector2 _speed;
        private int _accelarator;
        private int _ch;
        private float _degree;


        public Player(ContentManager content)
        {
            _accelarator = 1;
            _ch = 0;
            _speed = Vector2.Zero;
            _degree = 0;
            _shootTexture = content.Load<Texture2D>("Doodle_jumpContent/DoodleT");
            _noseTexture = content.Load<Texture2D>("Doodle_jumpContent/DoodleKH");
            _playerTexture = content.Load<Texture2D>("Doodle_jumpContent/DoodleR1");
            Initialize();
        }

        public void Initialize()
        {
            _position = new Rectangle(230, 560, 60, 60);
        }

        public void Move()
        {
            if (_ch == 0)
            {
                _speed.Y += _accelarator;
                _ch = 1;
            }
            else
                _ch = 0;

            _position.Y += (int)_speed.Y;
        }

        public void Draw(SpriteBatch s, ref cond name, int game)
        {
            if (game == gameRunning)
                switch (name)
                {
                    case cond.Left: s.Draw(_playerTexture, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f); break;
                    case cond.Right: s.Draw(_playerTexture, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f); break;
                    case cond.Tir:
                        s.Draw(_shootTexture, _position, Color.White);
                        s.Draw(_noseTexture, _position, Color.White);
                        name = cond.Left;
                        break;
                }
        }

        public Rectangle PlayerPosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 PlayerSpeed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public float Degree
        {
            get { return _degree; }
            set { _degree = value; }
        }


    }
}
