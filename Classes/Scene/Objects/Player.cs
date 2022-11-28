using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.X3DAudio;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects
{
    class Player
    {
        private Texture2D _playerTexture;
        private Texture2D _shootTexture;
        private Rectangle _position;
        private SpriteEffects _orientation;
        private Vector2 _speed;
        private int _accelarator;
        private int _ch;
        private float _degree;
        private bool _draw = false;

        Sprite _sprite;

        public Player(ContentManager content)
        {
            _accelarator = 1;
            _ch = 0;
            _speed = Vector2.Zero;
            _degree = 0f;
            _sprite = new Sprite();
            _orientation = SpriteEffects.None;
            _shootTexture = content.Load<Texture2D>("Doodle_jumpContent/DoodleShoot");
            _playerTexture = content.Load<Texture2D>("Doodle_jumpContent/DoodleR1");
            _position = new Rectangle(0, 0, 0, 0);
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

        public void Draw(SpriteBatch s, ref cond name, MouseState m, int game)
        {
            if (game == gameRunning)
                if (!_draw) 
                {
                    s.Draw(_shootTexture, _position, null, Color.White, 0f, Vector2.Zero, _orientation, 0f);
                    _draw = true;
                }
                switch (name)
                {
                    case cond.Left: _orientation = SpriteEffects.FlipHorizontally; break;
                    case cond.Right: _orientation = SpriteEffects.None; break;
                    case cond.Tir:
                        s.Draw(_shootTexture, _position, Color.White);
                        _draw= false;
                        name = cond.Left;
                        break;
                }
        }


    }
}
