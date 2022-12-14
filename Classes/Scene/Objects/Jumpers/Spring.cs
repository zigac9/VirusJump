using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace VirusJump.Classes.Scene.Objects.Jumpers
{
    public class Spring
    {
        private Texture2D _fanarIn;
        private Texture2D _fanarOut;
        private Rectangle _position;
        private bool _visible;
        private bool _sCheck;
        private int _scoreToMove;
        private int _scoreMoveStep;
        private bool _inOut;
        private int _sRand;


        public Spring(ContentManager content)
        {
            _fanarIn = content.Load<Texture2D>("Doodle_jumpContent/fanar");
            _fanarOut = content.Load<Texture2D>("Doodle_jumpContent/oFanar");
            Initialize();
        }

        public void Initialize()
        {
            _visible = false;
            _scoreToMove = 200;
            _scoreMoveStep = 500;
            _position = new Rectangle(-100, 730, 20, 30);
            _sRand = -1;
            _inOut = true;
            _sCheck = false;
        }

        public void Draw(SpriteBatch s)
        {
            if (_inOut)
            {
                s.Draw(_fanarIn, _position, Color.White);
            }
            else
            {
                s.Draw(_fanarOut, _position, Color.White);
                _visible = false;
            }
        }

        public bool Collision(Player player, bool collisionCheck)
        {
            if ((player.PlayerPosition.X + 10 > _position.X && player.PlayerPosition.X + 10 < _position.X + player.PlayerPosition.Width) || (player.PlayerPosition.X + player.PlayerPosition.Width - 10 > _position.X && player.PlayerPosition.X + player.PlayerPosition.Width -10 < _position.X + player.PlayerPosition.Width))
            {
                if (_position.Y + _position.Height - player.PlayerPosition.Y - player.PlayerPosition.Height < 5 && _position.Y + _position.Height - player.PlayerPosition.Y - player.PlayerPosition.Height > -15 && player.Speed.Y > 0)
                {
                    if (collisionCheck == true)
                        return true;
                    else
                        return false;
                }
                else return false;
            }
            else return false;
        }

        public Rectangle SpringPosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public bool SCheck
        {
            get { return _sCheck; }
            set { _sCheck = value; }
        }

        public bool InOut
        {
            get { return _inOut; }
            set { _inOut = value; }
        }

        public int SRand
        {
            get { return _sRand; }
            set { _sRand = value; }
        }

        public int ScoreToMove
        {
            get { return _scoreToMove; }
            set { _scoreToMove = value; }
        }

        public int ScoreMoveStep
        {
            get { return _scoreMoveStep; }
            set { _scoreMoveStep = value; }
        }
    }
}
