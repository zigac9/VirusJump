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
    class Background
    {
        public Texture2D back;
        public Texture2D kooh;
        public Texture2D sides;
        public Texture2D introMenu;
        public Texture2D option;
        public Texture2D notif;
        public Texture2D pause;
        public Texture2D sOn;
        public Texture2D sOff;
        public Texture2D gameOvre;
        public Texture2D hScore;
        public Rectangle bPosize;
        public Rectangle kPosize;
        public Rectangle sPosise1;
        public Rectangle sPosise2;
        public Rectangle introMenuposize;
        public Rectangle hScoreposize;
        public Rectangle optionposize;
        public Rectangle notifposize;
        public Rectangle pauseposize;
        public Rectangle gameOverposize;
        public Rectangle sOnposize;
        public Rectangle sOffposize;
        public int hScore1;
        public int hScore2;
        public int hScore3;
        public int hScore4;
        public int hScore5;
        public bool soundCheck;
        public bool gameStateCheck;

        public Background()
        {

        }

        //public void draw(SpriteBatch s, int game, scor score)

        public void draw(SpriteBatch s, int game)
        {
            s.Draw(back, bPosize, Color.White);
            s.Draw(kooh, kPosize, Color.White);
            s.Draw(sides, sPosise1, Color.White);
            s.Draw(sides, sPosise2, Color.White);
            if (game == 0)
                s.Draw(introMenu, introMenuposize, Color.White);
            if (game == 2)
                s.Draw(pause, pauseposize, Color.White);
            if (game == 3)
            {
                s.Draw(option, optionposize, Color.White);
                if (soundCheck == true)
                    s.Draw(sOn, sOnposize, Color.White);
                else
                    s.Draw(sOff, sOffposize, Color.White);
            }
            if (game == 4)
            {
                s.Draw(gameOvre, gameOverposize, Color.White);
                //s.DrawString(score.spFont, score.s.ToString(), new Vector2(308f, 245f), Color.Black);
                //s.DrawString(score.spFont, score.bestS, new Vector2(295f, 297f), Color.Black);
            }
            if (game == 5)
            {
                s.Draw(hScore, hScoreposize, Color.White);
                //s.DrawString(score.spFont, hScore1.ToString(), new Vector2(215f, 245f), Color.Black);
                //s.DrawString(score.spFont, hScore2.ToString(), new Vector2(215f, 290f), Color.Black);
                //s.DrawString(score.spFont, hScore3.ToString(), new Vector2(215f, 335f), Color.Black);
                //s.DrawString(score.spFont, hScore4.ToString(), new Vector2(215f, 380f), Color.Black);
                //s.DrawString(score.spFont, hScore5.ToString(), new Vector2(215f, 420f), Color.Black);
            }
        }
        public void notifdraw(SpriteBatch spriteBatch, int game)
        {
            if (game != 0 && game != 3 && game != 5)
                spriteBatch.Draw(notif, notifposize, Color.White);
        }

        public void sideCheck()
        {
            if (sPosise1.Y > 720)
                sPosise1.Y = sPosise2.Y - 3600;
            if (sPosise2.Y > 720)
                sPosise2.Y = sPosise1.Y - 3600;
        }
    }
}
