using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using VirusJump.Classes.Scene.Objects.Supplements;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects
{
    public class Player
    {
        // private readonly Texture2D _playerTexture;
        // private readonly Texture2D _shootTexture;
        // private readonly Texture2D _jetpackL;
        // private readonly Texture2D _jetpackR;
        private Texture2D _active;
        // private readonly Texture2D _dead;

        private readonly AnimatedSprite _animatedSprite;

        private Vector2 _firePosition;
        private Rectangle _shootPosition;
        private Rectangle _position;
        private Vector2 _speed;

        private int _accelarator;
        private int _ch;
        private float _degree;
        private float _shootDegree;

        private bool _jet;
        
        private Textures _textures;

        public Player(ContentManager content, Textures textures)
        {
            _textures = textures;
            var spriteSheet = content.Load<SpriteSheet>("assets/fire.sf", new JsonContentLoader());
            _animatedSprite = new AnimatedSprite(spriteSheet);
            Initialize();
        }

        public void Initialize()
        {
            _accelarator = 1;
            _ch = 0;
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

        public void Draw(SpriteBatch s, PlayerOrientEnum name, GameStateEnum game, bool collisionCheck)
        {
            if (game == GameStateEnum.GameRunning)
            {
                switch (name)
                {
                    case PlayerOrientEnum.Left:
                        if (_jet)
                        {
                            _active = _textures.Textures1["assets/manjetpackL"];
                            _firePosition = new Vector2(_firePosition.X + _position.Width - 10, _firePosition.Y);
                            _animatedSprite.Draw(s, _firePosition, 0f, new Vector2(2, 2));
                        }else if(!collisionCheck)
                        {
                            _active = _textures.Textures1["assets/dead"];
                        }
                        else _active = _textures.Textures1["assets/DoodleR1"];
                        s.Draw(_active, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                        break;
                    case PlayerOrientEnum.Right:
                        if (_jet)
                        {
                            _active = _textures.Textures1["assets/manjetpack"];
                            _firePosition = new Vector2(_firePosition.X + 10, _firePosition.Y);
                            _animatedSprite.Draw(s, _firePosition, 0f, new Vector2(2, 2));
                        }
                        else if (!collisionCheck)
                        {
                            _active = _textures.Textures1["assets/dead"];
                        }
                        else _active = _textures.Textures1["assets/DoodleR1"];
                        s.Draw(_active, _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                        break;
                }
                s.Draw(_textures.Textures1["assets/injection"], _shootPosition, null, Color.White, _shootDegree, new Vector2(50, 0), SpriteEffects.None, 1f);
            }
            else if(game == GameStateEnum.IntroMenu)
            {
                s.Draw(_textures.Textures1["assets/DoodleR1"], _position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        public bool BulletCollision(Bullet bullet)
        {
            if (bullet.Position.X > _position.X && bullet.Position.X + bullet.Position.Width < _position.X + _position.Width && bullet.Position.Y > _position.Y && bullet.Position.Y + bullet.Position.Height < _position.Y + _position.Height) return true;
            else return false;
        }

        public Rectangle PlayerPosition
        {
            get => _position;
            set => _position = value;
        }

        public Rectangle ShootPosition
        {
            get => _shootPosition;
            set => _shootPosition = value;
        }

        public Vector2 Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public float Degree
        {
            get => _degree;
            set => _degree = value;
        }

        public float ShootDegree
        {
            get => _shootDegree;
            set => _shootDegree = value;
        }

        public Vector2 FirePosition
        {
            get => _firePosition;
            set => _firePosition = value;
        }

        public bool IsJetpack
        {
            get => _jet;
            set => _jet = value;
        }

        public AnimatedSprite GetAnimatedSprite => _animatedSprite;
    }
}
