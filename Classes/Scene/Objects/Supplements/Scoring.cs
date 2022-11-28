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
    public class Scoring
    {
        private int _s;
        private string _bestS;
        private Vector2 _position;
        private SpriteFont _spFont;
        private bool _check;

        public Scoring(ContentManager content) 
        {
            _s = 0;
            _bestS = null;
            _position = Vector2.Zero;
            _check = false;
            _spFont = content.Load<SpriteFont>("Doodle_jumpContent/SpriteFont1"); ;
        }

        public void Draw(SpriteBatch sp, int game)
        {
            if (game != 0 && game != 3 && game != 5)
                sp.DrawString(_spFont, _s.ToString(), _position, Color.White);
        }

        public Vector2 ScoringPosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public bool Check
        {
            get { return _check; }
            set { _check = value; }
        }

        public SpriteFont SFont
        {
            get { return _spFont; }
            set { _spFont = value; }
        }

        public int SNevem
        {
            get { return _s; }
            set { _s = value; }
        }

        public string BestS
        {
            get { return _bestS; }
            set { _bestS = value; }
        }


    }
}
