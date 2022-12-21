using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VirusJump.Classes.Scene.Objects.Jumpers
{
    public class Jetpack
    {
        private readonly Texture2D _jetpack;

        private Rectangle _position;

        private bool _visible;
        private bool _jCheck;

        private int _scoreToMove;
        private int _scoreMoveStep;
        private int _jRand;

        public Jetpack(ContentManager content)
        {
            _jetpack = content.Load<Texture2D>("assets/jet");
            Initialize();
        }

        public void Initialize()
        {
            _visible = false;
            _scoreToMove = 1000;
            _scoreMoveStep = 2000;
            _position = new Rectangle(-100, 730, 30, 40);
            _jRand = -1;
            _jCheck = false;
        }

        public void Draw(SpriteBatch s)
        {
            s.Draw(_jetpack, _position, Color.White);
        }

        public bool Collision(Player player, bool collisionCheck)
        {
            if ((player.PlayerPosition.X + 10 > _position.X && player.PlayerPosition.X + 10 < _position.X + player.PlayerPosition.Width) || (player.PlayerPosition.X + player.PlayerPosition.Width - 10 > _position.X && player.PlayerPosition.X + player.PlayerPosition.Width -10 < _position.X + player.PlayerPosition.Width))
            {
                if (_position.Y + _position.Height - player.PlayerPosition.Y - player.PlayerPosition.Height < 5 && _position.Y + _position.Height - player.PlayerPosition.Y - player.PlayerPosition.Height > -15 && player.Speed.Y > 0)
                {
                    return collisionCheck;
                }
                else return false;
            }
            else return false;
        }

        public Rectangle JetPosition
        {
            get => _position;
            set => _position = value;
        }

        public bool Visible
        {
            get => _visible;
            set => _visible = value;
        }

        public bool JCheck
        {
            get => _jCheck;
            set => _jCheck = value;
        }

        public int JRand
        {
            get => _jRand;
            set => _jRand = value;
        }

        public int ScoreToMove
        {
            get => _scoreToMove;
            set => _scoreToMove = value;
        }

        public int ScoreMoveStep
        {
            get => _scoreMoveStep;
            set => _scoreMoveStep = value;
        }
    }
}
