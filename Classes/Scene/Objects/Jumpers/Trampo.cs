﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VirusJump.Classes.Scene.Objects.Jumpers
{
    public class Trampo
    {
        private Rectangle _position;

        private bool _visible;
        private bool _tCheck;

        private int _scoreToMove;
        private int _scoreMoveStep;
        private int _tRand;
        private Textures _textures;

        public Trampo(Textures textures)
        {
            _textures = textures;
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
            spriteBatch.Draw( _textures.Textures1["assets/toshak"], _position, Color.White);
        }

        public bool Collision(Player player, bool collisionCheck)
        {
            if ((player.PlayerPosition.X + 10 > _position.X && player.PlayerPosition.X + 10 < _position.X + player.PlayerPosition.Width) || (player.PlayerPosition.X + player.PlayerPosition.Width - 10 > _position.X && player.PlayerPosition.X + player.PlayerPosition.Width - 10 < _position.X + player.PlayerPosition.Width))
            {
                if (_position.Y + _position.Height - player.PlayerPosition.Y - player.PlayerPosition.Height < 5 && _position.Y + _position.Height - player.PlayerPosition.Y - player.PlayerPosition.Height > -15 && player.Speed.Y > 0)
                {
                    if (collisionCheck)
                        return true;
                    return false;
                }
                return false;
            }
            return false;
        }

        public Rectangle TrampoPosition
        {
            get => _position;
            set => _position = value;
        }

        public int TRand
        {
            get => _tRand;
            set => _tRand = value;
        }

        public bool Visible
        {
            get => _visible;
            set => _visible = value;
        }
        public bool Check
        {
            get => _tCheck;
            set => _tCheck = value;
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
