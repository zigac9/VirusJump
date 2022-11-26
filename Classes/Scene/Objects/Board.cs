using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace VirusJump.Classes.Scene.Objects
{
    class Board : Sprite
    {
        private Texture2D texture;
        private Rectangle posize;

        Sprite _sprite;

        public Board()
        {
            _sprite = new Sprite();
        }
        public void draw(SpriteBatch s)
        {
            s.Draw(texture, posize, Color.White);
        }

        public bool Collision(Player doodleJumper)
        {
            //if ((s.posize.X + 15 > posize.X && s.posize.X + 15 < posize.X + 60) || (s.posize.X + 45 > posize.X && s.posize.X + 45 < posize.X + 60))

            //    if (posize.Y - s.posize.Y - 60 < 5 && posize.Y - s.posize.Y - 60 > -20 && s.speed.Y > 0)
            //        return true;
            //    else return false;
            //else return false;
            return false;
        }
    }
}
