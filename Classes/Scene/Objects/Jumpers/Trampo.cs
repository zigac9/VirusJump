using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using VirusJump.Classes.Scene.Objects.Boards.BoardClass;

namespace VirusJump.Classes.Scene.Objects.Jumpers
{
    public class Trampo
    {
        private Texture2D _trampoTexture;
        private Rectangle _position;
        private int _tRand;
        private bool _visible;
        private bool _tCheck;
        private int _scoreToMove;
        private int _scoreMoveStep;

        public Trampo(ContentManager content)
        {
            _trampoTexture = content.Load<Texture2D>("assets/toshak");
            Initialize();
        }

        public void Initialize()
        {
            _scoreToMove = 1000;
            _scoreMoveStep = 700;
            _tRand = -1;
            _visible = false;
            _tCheck = false;
            _position = new Rectangle(-100, 730, 40, 18);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_trampoTexture, _position, Color.White);
        }

        public bool Collision(Player player, bool collisionCheck)
        {
            if ((player.PlayerPosition.X + 10 > _position.X && player.PlayerPosition.X + 10 < _position.X + player.PlayerPosition.Width) || (player.PlayerPosition.X + player.PlayerPosition.Width - 10 > _position.X && player.PlayerPosition.X + player.PlayerPosition.Width - 10 < _position.X + player.PlayerPosition.Width))
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

        public Rectangle TrampoPosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public int TRand
        {
            get { return _tRand; }
            set { _tRand = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        public bool TCheck
        {
            get { return _tCheck; }
            set { _tCheck = value; }
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
