using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using VirusJump.Classes.Scene.Objects;
using VirusJump.Classes.Scene.Objects.Enemies;
using VirusJump.Classes.Scene.Objects.Jumpers;

namespace VirusJump.Classes.Graphics;

public abstract class GameRenderer : Game1
{
    //playagain in reposition gre v renderer
    public static void PlayAgain()
    {
        CurrentGameState = GameStateEnum.GameRunning; //menjaj
        CollisionCheck = true;
        Score.Check = true;
        ThingsCollisionCheck = true;
        Gameover = false;
        Player.Initialize();
        BoardsList.Initialize();
        Bullet.Initialize();
        BulletEnemy.Initialize();
        Background.Initialize();
        PlayerOrientation = PlayerOrientEnum.Right;
        Score.Score = 0;
        ITexturesClasses.Trampo.Initialize();
        ITexturesClasses.Spring.Initialize();
        ITexturesClasses.Jetpack.Initialize();
        ITexturesClasses.MovingEnemy.Initialize();
        ITexturesClasses.StaticEnemy.Initialize();

        //delete boards
        Nivo = new List<bool> { false, false, false, false, false };
        Brisi = false;
    }

    public static void RePosition()
    {
        var minY = 999;
        var rnd = new Random();

        for (var i = 0; i < BoardsList.BoardList.Length; i++)
        {
            if (BoardsList.BoardList[i].Visible)
            {
                if (BoardsList.BoardList[i].Position.Y < -28)
                    BoardsList.BoardList[i].DrawVisible = false;
                else if (BoardsList.BoardList[i].Position.Y > -28) BoardsList.BoardList[i].DrawVisible = true;
                if (BoardsList.BoardList[i].Position.Y > 734)
                {
                    foreach (var board in BoardsList.BoardList)
                        if (board.Position.Y < minY)
                            minY = board.Position.Y;

                    for (var j = 0; j < 4; j++)
                    {
                        if (BoardsList.MovingBoardList[j].Position.Y < minY)
                            minY = BoardsList.MovingBoardList[j].Position.Y;
                        if (BoardsList.FakeBoardList[j].Position.Y < minY)
                            minY = BoardsList.FakeBoardList[j].Position.Y;
                        if (BoardsList.GoneBoardList[j].Position.Y < minY)
                            minY = BoardsList.GoneBoardList[j].Position.Y;
                    }

                    BoardsList.BoardList[i].Position = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 56, minY - 28),
                        BoardsList.BoardList[i].Position.Width, BoardsList.BoardList[i].Position.Height);
                }
            }

            if (i < 4)
            {
                if (BoardsList.FakeBoardList[i].Position.Y < -28)
                    BoardsList.FakeBoardList[i].DrawVisible = false;
                else if (BoardsList.FakeBoardList[i].Position.Y > -28) BoardsList.FakeBoardList[i].DrawVisible = true;

                if (BoardsList.GoneBoardList[i].Position.Y < -28)
                    BoardsList.GoneBoardList[i].DrawVisible = false;
                else if (BoardsList.GoneBoardList[i].Position.Y > -28) BoardsList.GoneBoardList[i].DrawVisible = true;

                if (BoardsList.MovingBoardList[i].Position.Y > 734)
                {
                    foreach (var board in BoardsList.BoardList)
                        if (board.Position.Y < minY && BoardsList.BoardList[i].Visible)
                            minY = board.Position.Y;

                    for (var j = 0; j < 4; j++)
                    {
                        if (BoardsList.MovingBoardList[j].Position.Y < minY)
                            minY = BoardsList.MovingBoardList[j].Position.Y;
                        if (BoardsList.FakeBoardList[j].Position.Y < minY)
                            minY = BoardsList.FakeBoardList[j].Position.Y;
                        if (BoardsList.GoneBoardList[j].Position.Y < minY)
                            minY = BoardsList.GoneBoardList[j].Position.Y;
                    }

                    BoardsList.MovingBoardList[i].Position = new Rectangle(rnd.Next(0, 420),
                        rnd.Next(minY - 56, minY - 28), BoardsList.MovingBoardList[i].Position.Width,
                        BoardsList.MovingBoardList[i].Position.Height);
                }

                if (BoardsList.FakeBoardList[i].Position.Y > 734)
                {
                    foreach (var t in BoardsList.BoardList)
                        if (t.Position.Y < minY && BoardsList.BoardList[i].Visible)
                            minY = t.Position.Y;

                    for (var j = 0; j < 4; j++)
                    {
                        if (BoardsList.MovingBoardList[j].Position.Y < minY)
                            minY = BoardsList.MovingBoardList[j].Position.Y;
                        if (BoardsList.FakeBoardList[j].Position.Y < minY)
                            minY = BoardsList.FakeBoardList[j].Position.Y;
                        if (BoardsList.GoneBoardList[j].Position.Y < minY)
                            minY = BoardsList.GoneBoardList[j].Position.Y;
                    }

                    BoardsList.FakeBoardList[i].Position = new Rectangle(rnd.Next(0, 420),
                        rnd.Next(minY - 56, minY - 28), BoardsList.FakeBoardList[i].Position.Width,
                        BoardsList.FakeBoardList[i].Position.Height);
                    BoardsList.FakeBoardList[i].Visible = true;
                }

                if (BoardsList.GoneBoardList[i].Position.Y > 734)
                {
                    foreach (var t in BoardsList.BoardList)
                        if (t.Position.Y < minY && BoardsList.BoardList[i].Visible)
                            minY = t.Position.Y;

                    for (var j = 0; j < 4; j++)
                    {
                        if (BoardsList.MovingBoardList[j].Position.Y < minY)
                            minY = BoardsList.MovingBoardList[j].Position.Y;
                        if (BoardsList.FakeBoardList[j].Position.Y < minY)
                            minY = BoardsList.FakeBoardList[j].Position.Y;
                        if (BoardsList.GoneBoardList[j].Position.Y < minY)
                            minY = BoardsList.GoneBoardList[j].Position.Y;
                    }

                    BoardsList.GoneBoardList[i].Position = new Rectangle(rnd.Next(0, 420),
                        rnd.Next(minY - 56, minY - 28), BoardsList.GoneBoardList[i].Position.Width,
                        BoardsList.GoneBoardList[i].Position.Height);
                    BoardsList.GoneBoardList[i].Visible = true;
                }
            }
        }

        //move movable enemy
        if (Score.Score > ITexturesClasses.MovingEnemy.Start && !ITexturesClasses.MovingEnemy.Visible)
        {
            ITexturesClasses.MovingEnemy.TextureRand = rnd.Next(0, 3);
            ITexturesClasses.MovingEnemy.Position = new Rectangle(ITexturesClasses.MovingEnemy.Position.X, 50, ITexturesClasses.MovingEnemy.Position.Width,
                ITexturesClasses.MovingEnemy.Position.Height);
            ITexturesClasses.MovingEnemy.Start += ITexturesClasses.MovingEnemy.Step;
            ITexturesClasses.MovingEnemy.Visible = true;
        }
        else if (Score.Score > ITexturesClasses.MovingEnemy.End)
        {
            ITexturesClasses.MovingEnemy.Visible = false;
            ITexturesClasses.MovingEnemy.End = ITexturesClasses.MovingEnemy.Start + ITexturesClasses.MovingEnemy.View;
        }


        //delete boards
        if (Score.Score > 500 && !Nivo[0])
        {
            Nivo[0] = true;
            Brisi = true;
            ITexturesClasses.Spring.ScoreMoveStep = 700;
            ITexturesClasses.Trampo.ScoreMoveStep = 1700;
            ITexturesClasses.Jetpack.ScoreMoveStep = 3000;
        }
        else if (Score.Score > 1000 && !Nivo[1])
        {
            Nivo[1] = true;
            Brisi = true;
            ITexturesClasses.Spring.ScoreMoveStep = 1000;
            ITexturesClasses.Trampo.ScoreMoveStep = 2000;
            ITexturesClasses.Jetpack.ScoreMoveStep = 5000;
        }
        else if (Score.Score > 2000 && !Nivo[2])
        {
            Nivo[2] = true;
            Brisi = true;
            ITexturesClasses.Spring.ScoreMoveStep = 2000;
            ITexturesClasses.Trampo.ScoreMoveStep = 3000;
            ITexturesClasses.Jetpack.ScoreMoveStep = 6000;
        }
        else if (Score.Score > 3000 && !Nivo[3])
        {
            Nivo[3] = true;
            Brisi = true;
            ITexturesClasses.Spring.ScoreMoveStep = 3000;
            ITexturesClasses.Trampo.ScoreMoveStep = 4000;
            ITexturesClasses.Jetpack.ScoreMoveStep = 8000;
        }
        else if (Score.Score > 4000 && !Nivo[4])
        {
            Nivo[4] = true;
            Brisi = true;
            ITexturesClasses.Spring.ScoreMoveStep = 4000;
            ITexturesClasses.Trampo.ScoreMoveStep = 6000;
            ITexturesClasses.Jetpack.ScoreMoveStep = 12000;
        }

        if (Brisi)
        {
            Brisi = false;
            var outBoard = 0;
            for (var j = 0; j < BoardsList.BoardList.Length; j++)
            {
                if (BoardsList.BoardList[j].Position.Y < -20 && BoardsList.BoardList[j].Visible && j != ITexturesClasses.Trampo.TRand &&
                    j != ITexturesClasses.Spring.SRand && j != ITexturesClasses.Jetpack.JRand && j != ITexturesClasses.StaticEnemy.StRand)
                {
                    BoardsList.BoardList[j].Visible = false;
                    outBoard++;
                }

                if (outBoard == 2)
                    break;
            }
        }
    }
}