using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace VirusJump.Classes.Scene.Objects.Supplements
{
    public class Pointer
    {
        private Rectangle _position;
        private Texture2D _texture;

        public Pointer(ContentManager content) 
        {
            _position = new Rectangle(0, 0, 20, 20);
            _texture = content.Load<Texture2D>("Doodle_jumpContent/pointer");
        }
        
        public void Draw(SpriteBatch s)
        {
            s.Draw(_texture, _position, Color.White);
        }

        public Rectangle PointerPosition
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
