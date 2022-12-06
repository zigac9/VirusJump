using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VirusJump.Classes.Scene.Objects.Boards.BoardClass
{
    public class Board
    {
        private Texture2D _texture;
        private Rectangle _position;
        private bool _visible;

        public Board(ContentManager content, Rectangle position)
        {
            _texture = content.Load<Texture2D>("Doodle_jumpContent/p1");
            _position = position;
            _visible = true;
        }

        public void DrawSprite(SpriteBatch s)
        {
            s.Draw(_texture, _position, Color.White);
        }


        public bool Collision(Player player)
        {
            if (_visible)
            {
                if (player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + 60 || player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + 60)

                    if (_position.Y - player.PlayerPosition.Y - 60 < 5 && _position.Y - player.PlayerPosition.Y - 60 > -20 && player.PlayerSpeed.Y > 0)
                        return true;
                    else return false;
                else return false;
            }
            return false;
        }

        public Rectangle BoardPosition
        {
            get { return _position; }
            set { _position = value; }
        }
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
    }
}
