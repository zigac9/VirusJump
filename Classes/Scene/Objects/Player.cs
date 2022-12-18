using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using SharpDX.Direct2D1.Effects;
using System.Diagnostics;
using VirusJump.Classes.Scene.Objects.Supplements;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects
{
    public class Player
    {
        private Texture2D _playerTexture;
        private Texture2D _shootTexture;
        private Texture2D _jetpackL;
        private Texture2D _jetpackR;

        private Texture2D _active;

        private Texture2D _dead;

        private SpriteSheet _spriteSheet;
        private AnimatedSprite _animatedSprite;

        private Vector2 _firePosition;

        private Rectangle _shootPosition;

        private Rectangle _position;
        private Vector2 _speed;
        private int _accelarator;
        private int _ch;
        private float _degree;

        private bool _jet;

        private float _shootDegree;


        public Player(ContentManager content)
        {
            _accelarator = 1;
            _ch = 0;
            _playerTexture = content.Load<Texture2D>("assets/DoodleR1");
            _shootTexture = content.Load<Texture2D>("assets/injection");
            _jetpackR = content.Load<Texture2D>("assets/manjetpack");
            _jetpackL = content.Load<Texture2D>("assets/manjetpackL");

            _dead = content.Load<Texture2D>("assets/dead");


            _spriteSheet = content.Load<SpriteSheet>("assets/fire.sf", new JsonContentLoader());
            _animatedSprite = new AnimatedSprite(_spriteSheet);
            Initialize();
        }

        public void Initialize()
        {
            _degree = 0;
            _jet = false;
            _speed = new Vector2(0, -13); 
            _position = new Rectangle(230, 560, 60, 60);
            _shootPosition = new Rectangle(_position.X + _position.Width/2, _position.Y + _position.Height/2 + 15, 12, 55);
            _firePosition = new Vector2(_position.X, _position.Y + _position.Height);
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
            _firePosition.Y = _position.Y + _position.Height;
        }

        public void Draw(SpriteBatch s, playerOrientEnum name, gameStateEnum game, bool collisionCheck)
        {
            if (game == gameStateEnum.gameRunning)
            {
                switch (name)
                {
                    case playerOrientEnum.Left:
                        if (_jet)
                        {
                            _active = _jetpackL;
                            _firePosition = new Vector2(_firePosition.X + _position.Width - 10, _firePosition.Y);
                            _animatedSprite.Draw(s, _firePosition, 0f, new Vector2(2, 2));
                        }else if(!collisionCheck)
                        {
                            _active = _dead;
                        }
                        else _active = _playerTexture;
                        s.Draw(_active, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                        break;
                    case playerOrientEnum.Right:
                        if (_jet)
                        {
                            _active = _jetpackR;
                            _firePosition = new Vector2(_firePosition.X + 10, _firePosition.Y);
                            _animatedSprite.Draw(s, _firePosition, 0f, new Vector2(2, 2));
                        }
                        else if (!collisionCheck)
                        {
                            _active = _dead;
                        }
                        else _active = _playerTexture;
                        s.Draw(_active, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                        break;
                }
                s.Draw(_shootTexture, _shootPosition, null, Color.White, _shootDegree, new Vector2(50, 0), SpriteEffects.None, 1f);
            }
            else if(game == gameStateEnum.introMenu)
            {
                s.Draw(_playerTexture, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        public bool BulletCollision(Bullet bullet)
        {
            if (bullet.Position.X > _position.X && bullet.Position.X + bullet.Position.Width < _position.X + _position.Width && bullet.Position.Y > _position.Y && bullet.Position.Y + bullet.Position.Height < _position.Y + _position.Height) return true;
            else return false;
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

        public Vector2 Speed
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

        public Vector2 FirePosition
        {
            get { return _firePosition; }
            set { _firePosition = value; }
        }

        public bool IsJetpack
        {
            get { return _jet; }
            set { _jet = value; }
        }

        public AnimatedSprite GetAnimatedSprite
        {
            get { return _animatedSprite; }
        }
    }
}
