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

namespace VirusJump.Classes.Scene.Objects.Jumpers
{
    public class Trampo
    {
        private Texture2D _trampoTexture;
        private Rectangle _position;
        private int _tRand;
        private bool _visible;
        private int _frame;

        public Trampo(ContentManager content)
        {
            _trampoTexture = content.Load<Texture2D>("Doodle_jumpContent/toshak");
            Initialize();
        }

        public void Initialize()
        {
            _tRand = -1;
            _frame = 10;
            _visible = false;
            _position = new Rectangle(-100, 730, 40, 18);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_trampoTexture, _position, Color.White);
        }

        public bool Collision(Player s, bool collisionCheck)
        {
            if ((s.PlayerPosition.X + 10 > _position.X && s.PlayerPosition.X + 10 < _position.X + _position.Width) || (s.PlayerPosition.X + 50 > _position.X && s.PlayerPosition.X + 50 < _position.X + _position.Width))
                if (_position.Y + 12 - s.PlayerPosition.Y - _position.Width < 5 && _position.Y + 12 - s.PlayerPosition.Y - _position.Width > -15 && s.PlayerPosition.Y > 0)
                {
                    if (collisionCheck == true)
                        return true;
                    else
                        return false;
                }
                else return false;
            else return false;
        }

        public Rectangle TrampoPosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public int TRand
        {
            get { return _tRand; }
            set { _tRand = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public int Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }
    }
}
