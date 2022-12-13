using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects.Supplements
{
    public class Scoring
    {
        private int _score;
        private string _bestS;
        private Vector2 _position;
        private SpriteFont _spFont;
        private bool _check;

        public Scoring(ContentManager content) 
        {
            _score = 0;
            _bestS = "";
            _position = new Vector2(15f, 4f);
            _check = true;
            _spFont = content.Load<SpriteFont>("Doodle_jumpContent/SpriteFont1"); ;
        }

        public void Draw(SpriteBatch sp, gameStateEnum gameState)
        {
            sp.DrawString(_spFont, _score.ToString(), _position, Color.White);
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

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public string BestS
        {
            get { return _bestS; }
            set { _bestS = value; }
        }
    }
}
