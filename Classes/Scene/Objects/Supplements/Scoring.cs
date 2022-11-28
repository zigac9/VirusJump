using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace VirusJump.Classes.Scene.Objects.Supplements
{
    public class Scoring
    {
        private int _s;
        private string _bestS;
        private Vector2 _position;
        private SpriteFont _spFont;
        private bool _check;

        public Scoring() { }

        public void Draw(SpriteBatch sp, int game)
        {
            if (game != 0 && game != 3 && game != 5)
                sp.DrawString(_spFont, _s.ToString(), _position, Color.White);
        }
    }
}
