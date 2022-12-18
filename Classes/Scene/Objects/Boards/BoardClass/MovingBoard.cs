using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VirusJump.Classes.Scene.Objects.Boards.BoardClass
{
    public class MovingBoard
    {
        private Texture2D _texture;

        private Rectangle _position;

        private int _speed;

        public MovingBoard(ContentManager content, Rectangle position, int speed)
        {
            _texture = content.Load<Texture2D>("assets/p2");
            _position = position;
            _speed = speed;
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
            if (player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + 60 || player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + 60)
                if (_position.Y - player.PlayerPosition.Y - 60 < 5 && _position.Y - player.PlayerPosition.Y - 60 > -20 && player.Speed.Y > 0)
                    return true;
                else return false;
            else return false;
        }

        public Rectangle Position
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
