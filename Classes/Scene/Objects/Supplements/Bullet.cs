using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects.Supplements
{
    public class Bullet
    {
        private readonly List<Texture2D> _shootList;

        private Rectangle _position;
        private Vector2 _speed;

        private readonly float _accelertion;
        private bool _bullcheck;
        private readonly int _textureRnd;
        private Textures _textures;
        public Bullet(int rnd, Textures textures)
        {
            _textures = textures;
            _textureRnd = rnd;
            _shootList = new List<Texture2D> { _textures.Textures1["assets/tir"], _textures.Textures1["assets/virus"] };            
            _accelertion = 0.5f;
            _speed = new Vector2(0, 0);
            Initialize();
        }

        public void Initialize()
        {
            _bullcheck = false;
            _position = new Rectangle(-50, -50, 20, 20);
        }

        public void Draw(SpriteBatch s, GameStateEnum gameState)
        {
            if (gameState == GameStateEnum.GameRunning)
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
            get => _position;
            set => _position = value;
        }

        public Vector2 Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public bool IsCheck
        {
            get => _bullcheck;
            set => _bullcheck = value;
        }
    }
}
