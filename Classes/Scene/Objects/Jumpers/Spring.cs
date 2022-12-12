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

        public Spring(ContentManager content)
        {
            _fanarIn = content.Load<Texture2D>("Doodle_jumpContent/fanar");
            _fanarOut = content.Load<Texture2D>("Doodle_jumpContent/oFanar");

        }

        public void Initialize()
        {

        }

        public void Draw(SpriteBatch s, bool check)
        {
            if (!check)
                s.Draw(_fanarIn, _position, Color.White);
            else
                s.Draw(_fanarOut, _position, Color.White);
        }

        public bool Collision(Player player, bool collisionCheck)
        {
            if ((player.PlayerPosition.X + 10 > _position.X && player.PlayerPosition.X + 10 < _position.X + _position.Width) || (player.PlayerPosition.X + 50 > _position.X && player.PlayerPosition.X + 50 < _position.X + _position.Width))
                if (_position.Y + 17 - player.PlayerPosition.Y - _position.Width < 5 && _position.Y + 17 - player.PlayerPosition.Y - _position.Width > -15 && player.PlayerSpeed.Y > 0)
                {
                    if (collisionCheck == true)
                        return true;
                    else
                        return false;
                }
                else return false;
            else return false;
        }

        public Rectangle SpringPosition
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
