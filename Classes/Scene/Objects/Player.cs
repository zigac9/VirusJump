using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.X3DAudio;

namespace VirusJump.Classes.Scene.Objects
{
    class Player: Sprite
    {
        private Texture2D playerTexture;
        private Rectangle posize;
        private Vector2 speed;
        private const int accelarator = +1;
        private int ch; //kaj je to??
        //public float degree;

        private Sprite _sprite;

        //get in set dodat

        public Player() //not dodat elemente za sprite
        {
            _sprite= new Sprite(); //spremenit konstruktor za elemente
        }

        public void move()
        {
            if (ch == 0)
            {
                speed.Y += accelarator;
                ch = 1;
            }
            else
                ch = 0;

            posize.Y += (int)speed.Y;
        }

        public void draw(SpriteBatch spriteBatch, bool direction, MouseState mouse, int game)
        {
            //if (game == gameRunning)
            //    switch (name)
            //    {
            //        case cond.Left: s.Draw(textureL, posize, Color.White); break;
            //        case cond.Right: s.Draw(textureR, posize, Color.White); break;
            //        case cond.Tir:
            //            s.Draw(textureC, posize, Color.White);
            //            s.Draw(nose, posize, Color.White);
            //            name = cond.Left;
            //            break;

            //    }
        }
    }
}
