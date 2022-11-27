using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VirusJump.Classes.Scene.Objects
{
    class MovingBoard : Sprite
    {
        private Texture2D texture;
        private Vector2 cords;
        private Rectangle posize;
        private int speed;

        private Sprite _sprite;

        public MovingBoard() 
        {
            _sprite = new Sprite();
        }

        public void draw(SpriteBatch s)
        {
            s.Draw(texture, posize, Color.White);
        }

        public void move()
        {
            posize.X += speed;
            if (posize.X > 420 || posize.X < 0) speed *= -1;
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
