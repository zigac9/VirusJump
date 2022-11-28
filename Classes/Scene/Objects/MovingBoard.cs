﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects
{
    public class MovingBoard
    {
        private Texture2D _texture;
        private Rectangle _position;
        private int _speed;

        public MovingBoard(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Doodle_jumpContent/p2");
            _position = new Rectangle(0, 0, 0, 0);
            _speed = 0;
        }

        public void DrawSprite(SpriteBatch s)
        {
            s.Draw(_texture, _position, Color.White);
        }

        public void Move()
        {
            _position.X += _speed;
            if (_position.X > 420 || _position.X < 0) _speed *= -1;
        }

        public bool Collision(Player player)
        {
            if ((player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + 60) || (player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + 60))

                if (_position.Y - player.PlayerPosition.Y - 60 < 5 && _position.Y - player.PlayerPosition.Y - 60 > -20 && player.PlayerSpeed.Y > 0)
                    return true;
                else return false;
            else return false;
        }

        public Rectangle BoardPosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
    }
}