using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects
{
    public class Player
    {
        private Texture2D _playerTexture;
        private Texture2D _shootTexture;
        private Rectangle _shootPosition;

        private Texture2D _noseTexture;
        private Rectangle _position;
        private Vector2 _speed;
        private int _accelarator;
        private int _ch;
        private float _degree;

        private float _shootDegree;


        public Player(ContentManager content)
        {
            _accelarator = 1;
            _ch = 0;
            _shootTexture = content.Load<Texture2D>("Doodle_jumpContent/DoodleT");
            _noseTexture = content.Load<Texture2D>("Doodle_jumpContent/DoodleKH");
            _playerTexture = content.Load<Texture2D>("Doodle_jumpContent/DoodleR1");
            _shootTexture = content.Load<Texture2D>("Doodle_jumpContent/injection");
            Initialize();
        }

        public void Initialize()
        {
            _degree = 0;
            _speed = new Vector2(0, -13); 
            _position = new Rectangle(230, 560, 60, 60);
            _shootPosition = new Rectangle(_position.X + _position.Width/2, _position.Y + _position.Height/2 + 15, 12, 55);
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
            _shootPosition.Y = _position.Y + _position.Height/2 + 15;
        }

        public void Draw(SpriteBatch s, playerOrientEnum name, gameStateEnum game)
        {
            if (game == gameStateEnum.gameRunning)
            {
                switch (name)
                {
                    case playerOrientEnum.Left: s.Draw(_playerTexture, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f); break;
                    case playerOrientEnum.Right: s.Draw(_playerTexture, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f); break;
                }
                s.Draw(_shootTexture, _shootPosition, null, Color.White, _shootDegree, new Vector2(50, 0), SpriteEffects.None, 0f);
            }
            else if(game == gameStateEnum.introMenu)
            {
                s.Draw(_playerTexture, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        public Rectangle PlayerPosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public Rectangle ShootPosition
        {
            get { return _shootPosition; }
            set { _shootPosition = value; }
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

        public float ShootDegree
        {
            get { return _shootDegree; }
            set { _shootDegree = value; }
        }


    }
}
