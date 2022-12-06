using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using VirusJump.Classes.Scene.Objects.Supplements;
using static VirusJump.Game1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace VirusJump.Classes.Scene.Objects
{
    public class Background
    {
        private Texture2D _back;
        private Texture2D _kooh;
        private Texture2D _sides;
        private Texture2D _introMenu;
        private Texture2D _option;
        private Texture2D _notif;
        private Texture2D _pause;
        private Texture2D _sOn;
        private Texture2D _sOff;
        private Texture2D _gameOvre;
        private Texture2D _hScore;
        private Rectangle _bPosize;
        private Rectangle _kPosize;
        private Rectangle _sPosise1;
        private Rectangle _sPosise2;
        private Rectangle _introMenuposize;
        private Rectangle _hScoreposize;
        private Rectangle _optionposize;
        private Rectangle _notifposize;
        private Rectangle _pauseposize;
        private Rectangle _gameOverposize;
        private Rectangle _sOnposize;
        private Rectangle _sOffposize;
        private int _hScore1;
        private int _hScore2;
        private int _hScore3;
        private int _hScore4;
        private int _hScore5;
        private bool _soundCheck;
        private bool _gameStateCheck;

        public Background(ContentManager content)
        {
            _back = content.Load<Texture2D>("Doodle_jumpContent/gradient");
            _kooh = content.Load<Texture2D>("Doodle_jumpContent/kooh");
            _introMenu = content.Load<Texture2D>("Doodle_jumpContent/mainMenu1");
            _option = content.Load<Texture2D>("Doodle_jumpContent/option");
            _sOn = content.Load<Texture2D>("Doodle_jumpContent/sOn");
            _sOff = content.Load<Texture2D>("Doodle_jumpContent/sOff");
            _notif = content.Load<Texture2D>("Doodle_jumpContent/notif");
            _pause = content.Load<Texture2D>("Doodle_jumpContent/pause");
            _sides = content.Load<Texture2D>("Doodle_jumpContent/sides");
            _gameOvre = content.Load<Texture2D>("Doodle_jumpContent/gameOver");
            _hScore = content.Load<Texture2D>("Doodle_jumpContent/highscore");
            _introMenuposize = new Rectangle(0, 0, 480, 720);
            _optionposize = new Rectangle(0, 0, 480, 720);
            _sOnposize = new Rectangle(100, 330, 136, 45);
            _sOffposize = new Rectangle(100, 330, 136, 45);
            _notifposize = new Rectangle(0, 0, 480, 60);
            _pauseposize = new Rectangle(0, 0, 480, 720);
            _gameOverposize = new Rectangle(0, 0, 480, 720);
            _hScoreposize = new Rectangle(0, 0, 480, 720);
            Initialize();

        }

        public void Initialize()
        {
            _bPosize = new Rectangle(0, -6480, 480, 7200);
            _kPosize = new Rectangle(0, 0, 480, 720);
            _sPosise1 = new Rectangle(0, -2880, 480, 3600);
            _sPosise2 = new Rectangle(0, -6480, 480, 3600);
            _bPosize = new Rectangle(_bPosize.X, -7200 + 720, _bPosize.Width, _bPosize.Height);
            _soundCheck = true;
            _gameStateCheck = true;
        }


        public void Draw(SpriteBatch s, gameStateEnum gameState, Scoring score)
        {
            s.Draw(_back, _bPosize, Color.White);
            s.Draw(_kooh, _kPosize, Color.White);
            s.Draw(_sides, _sPosise1, Color.White);
            s.Draw(_sides, _sPosise2, Color.White);
            if (gameState == gameStateEnum.introMenu)
                s.Draw(_introMenu, _introMenuposize, Color.White);
            if (gameState == gameStateEnum.pause)
                s.Draw(_pause, _pauseposize, Color.White);
            if (gameState == gameStateEnum.option)
            {
                s.Draw(_option, _optionposize, Color.White);
                if (_soundCheck == true)
                    s.Draw(_sOn, _sOnposize, Color.White);
                else
                    s.Draw(_sOff, _sOffposize, Color.White);
            }
            if (gameState == gameStateEnum.gameOver)
            {
                s.Draw(_gameOvre, _gameOverposize, Color.White);
                s.DrawString(score.SFont, score.Score.ToString(), new Vector2(308f, 245f), Color.Black);
                s.DrawString(score.SFont, score.BestS, new Vector2(295f, 297f), Color.Black);
            }
            if (gameState == gameStateEnum.hScore)
            {
                s.Draw(_hScore, _hScoreposize, Color.White);
                s.DrawString(score.SFont, _hScore1.ToString(), new Vector2(215f, 245f), Color.Black);
                s.DrawString(score.SFont, _hScore2.ToString(), new Vector2(215f, 290f), Color.Black);
                s.DrawString(score.SFont, _hScore3.ToString(), new Vector2(215f, 335f), Color.Black);
                s.DrawString(score.SFont, _hScore4.ToString(), new Vector2(215f, 380f), Color.Black);
                s.DrawString(score.SFont, _hScore5.ToString(), new Vector2(215f, 420f), Color.Black);
            }
        }
        public void Notifdraw(SpriteBatch s, gameStateEnum gameState)
        {
            if (gameState != gameStateEnum.introMenu && gameState != gameStateEnum.option && gameState != gameStateEnum.hScore)
                s.Draw(_notif, _notifposize, Color.White);
        }

        public void SideCheck()
        {
            if (_sPosise1.Y > 720)
                _sPosise1.Y = _sPosise2.Y - 3600;
            if (_sPosise2.Y > 720)
                _sPosise2.Y = _sPosise1.Y - 3600;
        }

        public Rectangle KPosize
        {
            get { return _kPosize; }
            set { _kPosize = value; }
        }

        public Rectangle BPosize
        {
            get { return _bPosize; }
            set { _bPosize = value; }
        }
        public Rectangle SPosise1
        {
            get { return _sPosise1; }
            set { _sPosise1 = value; }
        }
        public Rectangle SPosise2
        {
            get { return _sPosise2; }
            set { _sPosise2 = value; }
        }

        public bool SoundCheck
        {
            get { return _soundCheck; }
            set { _soundCheck = value; }
        }

        public bool GameStateCheck
        {
            get { return _gameStateCheck; }
            set { _gameStateCheck = value; }
        }
    }
}
