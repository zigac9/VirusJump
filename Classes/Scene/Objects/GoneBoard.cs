using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using static VirusJump.Game1;
using SharpDX.Direct3D9;

namespace VirusJump.Classes.Scene.Objects
{
    public class GoneBoard
    {
        private Texture2D _texture;
        private Rectangle _position;

        public GoneBoard(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Doodle_jumpContent/p4");
            _position = new Rectangle(0, 0, 0, 0);
        }

        public void DrawSprite(SpriteBatch s)
        {
            s.Draw(_texture, _position, Color.White);
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
    }
}
