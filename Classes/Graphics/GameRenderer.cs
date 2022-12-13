using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static VirusJump.Game1;
using VirusJump.Classes.Scene.Objects.Boards;
using VirusJump.Classes.Scene.Objects.Jumpers;
using VirusJump.Classes.Scene.Objects.Supplements;
using VirusJump.Classes.Scene.Objects;

namespace VirusJump.Classes.Graphics
{
    public class GameRenderer : Game1
    {
        //playagain in reposition gre v renderer
        public static void PlayAgain(Player player, Scoring score, Background background)
        {
            currentGameState = gameStateEnum.gameRunning;//menjaj 
            collisionCheck = true;
            score.Check = true;
            gameover = false;
            player.Initialize();
            boardsList.Initialize();
            bullet.Initialize();
            background.Initialize();
            playerOrientation = playerOrientEnum.Right;
            score.Score = 0;
            trampo.Initialize();
            spring.Initialize();
            jetpack.Initialize();

            //delete boards
            nivo = new List<bool> { false, false, false, false, false };
            brisi = false;
        }

        public static void rePosition()
        {
            int minY = 999;
            Random rnd = new Random();

            for (int i = 0; i < boardsList.BoardList.Length; i++)
            {
                if (boardsList.BoardList[i].Visible)
                {
                    if (boardsList.BoardList[i].BoardPosition.Y < -28)
                    {
                        boardsList.BoardList[i].DrawVisible = false;
                    }
                    else if (boardsList.BoardList[i].BoardPosition.Y > -28)
                    {
                        boardsList.BoardList[i].DrawVisible = true;
                    }
                    if (boardsList.BoardList[i].BoardPosition.Y > 734)
                    {
                        for (int j = 0; j < boardsList.BoardList.Length; j++)
                            if (boardsList.BoardList[j].BoardPosition.Y < minY) minY = boardsList.BoardList[j].BoardPosition.Y;
                        for (int j = 0; j < 4; j++)
                        {
                            if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                            if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                            if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                        }
                        boardsList.BoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 56, minY - 28), boardsList.BoardList[i].BoardPosition.Width, boardsList.BoardList[i].BoardPosition.Height);
                    }
                }

                if (i < 4)
                {
                    if (boardsList.FakeBoardList[i].BoardPosition.Y < -28)
                    {
                        boardsList.FakeBoardList[i].DrawVisible = false;
                    }
                    else if (boardsList.FakeBoardList[i].BoardPosition.Y > -28)
                    {
                        boardsList.FakeBoardList[i].DrawVisible = true;
                    }

                    if (boardsList.GoneBoardList[i].BoardPosition.Y < -28)
                    {
                        boardsList.GoneBoardList[i].DrawVisible = false;
                    }
                    else if (boardsList.GoneBoardList[i].BoardPosition.Y > -28)
                    {
                        boardsList.GoneBoardList[i].DrawVisible = true;
                    }

                    if (boardsList.MovingBoardList[i].BoardPosition.Y > 734)
                    {
                        for (int j = 0; j < boardsList.BoardList.Length; j++)
                            if (boardsList.BoardList[j].BoardPosition.Y < minY && boardsList.BoardList[i].Visible) minY = boardsList.BoardList[j].BoardPosition.Y;
                        for (int j = 0; j < 4; j++)
                        {
                            if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                            if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                            if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                        }
                        boardsList.MovingBoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 56, minY - 28), boardsList.MovingBoardList[i].BoardPosition.Width, boardsList.MovingBoardList[i].BoardPosition.Height);
                    }
                    if (boardsList.FakeBoardList[i].BoardPosition.Y > 734)
                    {
                        for (int j = 0; j < boardsList.BoardList.Length; j++)
                            if (boardsList.BoardList[j].BoardPosition.Y < minY && boardsList.BoardList[i].Visible) minY = boardsList.BoardList[j].BoardPosition.Y;
                        for (int j = 0; j < 4; j++)
                        {
                            if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                            if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                            if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                        }
                        boardsList.FakeBoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 56, minY - 28), boardsList.FakeBoardList[i].BoardPosition.Width, boardsList.FakeBoardList[i].BoardPosition.Height);
                        boardsList.FakeBoardList[i].Visible = true;
                    }
                    if (boardsList.GoneBoardList[i].BoardPosition.Y > 734)
                    {
                        for (int j = 0; j < boardsList.BoardList.Length; j++)
                            if (boardsList.BoardList[j].BoardPosition.Y < minY && boardsList.BoardList[i].Visible) minY = boardsList.BoardList[j].BoardPosition.Y;
                        for (int j = 0; j < 4; j++)
                        {
                            if (boardsList.MovingBoardList[j].BoardPosition.Y < minY) minY = boardsList.MovingBoardList[j].BoardPosition.Y;
                            if (boardsList.FakeBoardList[j].BoardPosition.Y < minY) minY = boardsList.FakeBoardList[j].BoardPosition.Y;
                            if (boardsList.GoneBoardList[j].BoardPosition.Y < minY) minY = boardsList.GoneBoardList[j].BoardPosition.Y;
                        }
                        boardsList.GoneBoardList[i].BoardPosition = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 56, minY - 28), boardsList.GoneBoardList[i].BoardPosition.Width, boardsList.GoneBoardList[i].BoardPosition.Height);
                        boardsList.GoneBoardList[i].Visible = true;
                    }
                }
            }

            //delete boards
            if (score.Score > 500 && !nivo[0])
            {
                nivo[0] = true;
                brisi = true;
                spring.ScoreMoveStep = 700;
                trampo.ScoreMoveStep = 1700;
                jetpack.ScoreMoveStep = 3000;
            }
            else if (score.Score > 1000 && !nivo[1])
            {
                nivo[1] = true;
                brisi = true;
                spring.ScoreMoveStep = 1000;
                trampo.ScoreMoveStep = 2000;
                jetpack.ScoreMoveStep = 5000;
            }
            else if (score.Score > 2000 && !nivo[2])
            {
                nivo[2] = true;
                brisi = true;
                spring.ScoreMoveStep = 2000;
                trampo.ScoreMoveStep = 3000;
                jetpack.ScoreMoveStep = 6000;
            }
            else if (score.Score > 3000 && !nivo[3])
            {
                nivo[3] = true;
                brisi = true;
                spring.ScoreMoveStep = 3000;
                trampo.ScoreMoveStep = 4000;
                jetpack.ScoreMoveStep = 8000;
            }
            else if (score.Score > 4000 && !nivo[4])
            {
                nivo[4] = true;
                brisi = true;
                spring.ScoreMoveStep = 4000;
                trampo.ScoreMoveStep = 6000;
                jetpack.ScoreMoveStep = 12000;
            }
            if (brisi)
            {
                brisi = false;
                int outBoard = 0;
                for (int j = 0; j < boardsList.BoardList.Length; j++)
                {
                    if (boardsList.BoardList[j].BoardPosition.Y < -20 && boardsList.BoardList[j].Visible && j != trampo.TRand && j != spring.SRand && j != jetpack.JRand)
                    {
                        boardsList.BoardList[j].Visible = false;
                        outBoard++;
                    }
                    if (outBoard == 2)
                        break;
                }
            }

        }
    }
}
