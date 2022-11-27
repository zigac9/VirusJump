﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct3D9;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects
{
    public class FakeBoard
    {
        private Texture2D _texture;
        private Rectangle _position;
        Sprite _sprite;


        public FakeBoard(ContentManager content)
        {
            _sprite = new Sprite();
            _texture = content.Load<Texture2D>("Doodle_jumpContent/p3");
            _position = new Rectangle(0, 0, 0, 0);
        }

        public void DrawSprite(SpriteBatch s)
        {
            s.Draw(_texture, _position, Color.White);
        }

        public bool Collision(doodle s)
        {
            if ((s.posize.X + 15 > _position.X && s.posize.X + 15 < _position.X + 60) || (s.posize.X + 45 > _position.X && s.posize.X + 45 < _position.X + 60))

                if (_position.Y - s.posize.Y - 60 < 5 && _position.Y - s.posize.Y - 60 > -20 && s.speed.Y > 0)
                    return true;
                else return false;
            else return false;
        }

        public Rectangle BoardPosition
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
