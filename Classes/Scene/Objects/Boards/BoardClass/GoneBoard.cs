﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace VirusJump.Classes.Scene.Objects.Boards.BoardClass
{
    public class GoneBoard
    {
        private Rectangle _position;

        private bool _visible;
        private bool _drawVisible;
        private Texture2D _texture;

        public GoneBoard(Texture2D texture, Rectangle position)
        {
            _texture = texture;
            _position = position;
            _visible = true;
            _drawVisible = true;
        }

        public void DrawSprite(SpriteBatch s)
        {
            s.Draw(_texture, _position, Color.White);
        }

        public bool Collision(Player player)
        {
            if (player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + 60 || player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + 60)

                if (_position.Y - player.PlayerPosition.Y - 60 < 5 && _position.Y - player.PlayerPosition.Y - 60 > -20 && player.Speed.Y > 0)
                    return true;
                else return false;
            else return false;
        }

        public Rectangle Position
        {
            get => _position;
            set => _position = value;
        }

        public bool Visible
        {
            get => _visible;
            set => _visible = value;
        }

        public bool DrawVisible
        {
            get => _drawVisible;
            set => _drawVisible = value;
        }
    }
}
