using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects.Scoring
{
    public class ScorClass
    {
        private SpriteFont _spFont;
        private readonly Vector2 _position;
        
        private int _score;
        private bool _check;

        public ScorClass(ContentManager content) 
        {
            _score = 0;
            _position = new Vector2(15f, 4f);
            _check = true;
            _spFont = content.Load<SpriteFont>("assets/SpriteFont1");
        }

        public void Draw(SpriteBatch sp, GameStateEnum gameState)
        {
            sp.DrawString(_spFont, _score.ToString(), _position, Color.White,0f, new Vector2(0,0),new Vector2(1,1), SpriteEffects.None, 0f);
        }

        public bool Check
        {
            get => _check;
            set => _check = value;
        }

        public SpriteFont SFont
        {
            get => _spFont;
            set => _spFont = value;
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }
    }
}
