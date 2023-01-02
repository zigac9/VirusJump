using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using VirusJump.Classes.Scene.Objects;

namespace VirusJump.Classes.Graphics;

public abstract class GameRenderer : Game1
{
    //playagain in reposition gre v renderer
    public static void PlayAgain()
    {
        CurrentGameState = GameStateEnum.GameRunning; //menjaj
        CollisionCheck = true;
        ThingsCollisionCheck = true;
        GameOver = false;
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
                if (BoardsList.BoardList[j].Position.Y < -20 && BoardsList.BoardList[j].Visible &&
                    j != ITexturesClasses.Trampo.TRand &&
                    j != ITexturesClasses.Spring.SRand && j != ITexturesClasses.Jetpack.JRand &&
                    j != ITexturesClasses.StaticEnemy.StRand)
                {
                    BoardsList.BoardList[j].Visible = false;
                    outBoard++;
                }

                if (outBoard == 2)
                    break;
            }
        }
        
        for(int i = 0; i < 4; i++)
        {
            if (BoardsList.FakeBoardList[i].Position.Y < -28)
                BoardsList.FakeBoardList[i].DrawVisible = false;
            else if (BoardsList.FakeBoardList[i].Position.Y > -28) BoardsList.FakeBoardList[i].DrawVisible = true;

            if (BoardsList.GoneBoardList[i].Position.Y < -28)
                BoardsList.GoneBoardList[i].DrawVisible = false;
            else if (BoardsList.GoneBoardList[i].Position.Y > -28) BoardsList.GoneBoardList[i].DrawVisible = true;
            
            if (BoardsList.MovingBoardList[i].Position.Y < -28)
                BoardsList.MovingBoardList[i].DrawVisible = false;
            else if (BoardsList.MovingBoardList[i].Position.Y > -28) BoardsList.MovingBoardList[i].DrawVisible = true;
        }

        var rnd = new Random();
        for (var i = 0; i < BoardsList.BoardList.Length; i++)
        {
            switch (i)
            {
                case 1:
                    MoveMovingBoard(0, rnd);
                    break;
                case 3:
                    MoveFakeBoard(0, rnd);
                    break;
                case 5:
                    MoveGoneBoard(0, rnd);
                    break;
                case 7:
                    MoveMovingBoard(1, rnd);
                    break;
                case 9:
                    MoveFakeBoard(1, rnd);
                    break;
                case 11:
                    MoveGoneBoard(1, rnd);
                    break;
                case 13:
                    MoveMovingBoard(2, rnd);
                    break;
                case 15:
                    MoveFakeBoard(2, rnd);
                    break;
                case 17:
                    MoveGoneBoard(2, rnd);
                    break;
                case 19:
                    MoveMovingBoard(3, rnd);
                    break;
                case 21:
                    MoveFakeBoard(3, rnd);
                    break;
                case 22:
                    MoveGoneBoard(3, rnd);
                    break;
            }

            if (BoardsList.BoardList[i].Visible)
            {
                if (BoardsList.BoardList[i].Position.Y < -28)
                    BoardsList.BoardList[i].DrawVisible = false;
                else if (BoardsList.BoardList[i].Position.Y > -28) BoardsList.BoardList[i].DrawVisible = true;

                if (BoardsList.BoardList[i].Position.Y > 734)
                {
                    var minY = FindMinY();

                    BoardsList.BoardList[i].Position = new Rectangle(rnd.Next(0, 420), rnd.Next(minY - 56, minY - 28),
                        BoardsList.BoardList[i].Position.Width, BoardsList.BoardList[i].Position.Height);
                }
            }
        }

        //move movable enemy
        if (Score.Score > ITexturesClasses.MovingEnemy.Start && !ITexturesClasses.MovingEnemy.Visible)
        {
            ITexturesClasses.MovingEnemy.TextureRand = rnd.Next(0, 3);
            ITexturesClasses.MovingEnemy.Position = new Rectangle(ITexturesClasses.MovingEnemy.Position.X, 50,
                ITexturesClasses.MovingEnemy.Position.Width,
                ITexturesClasses.MovingEnemy.Position.Height);
            ITexturesClasses.MovingEnemy.Start += ITexturesClasses.MovingEnemy.Step;
            ITexturesClasses.MovingEnemy.Visible = true;
        }
        else if (Score.Score > ITexturesClasses.MovingEnemy.End)
        {
            ITexturesClasses.MovingEnemy.Visible = false;
            ITexturesClasses.MovingEnemy.End = ITexturesClasses.MovingEnemy.Start + ITexturesClasses.MovingEnemy.View;
        }
    }

    private static void MoveGoneBoard(int i, Random rnd)
    {
        if (BoardsList.GoneBoardList[i].Position.Y > 734)
        {
            var minY = FindMinY();

            BoardsList.GoneBoardList[i].Position = new Rectangle(rnd.Next(0, 420),
                rnd.Next(minY - 56, minY - 28), BoardsList.GoneBoardList[i].Position.Width,
                BoardsList.GoneBoardList[i].Position.Height);
            BoardsList.GoneBoardList[i].Visible = true;
        }
    }

    private static void MoveFakeBoard(int i, Random rnd)
    {
        if (BoardsList.FakeBoardList[i].Position.Y > 734)
        {
            var minY = FindMinY();

            BoardsList.FakeBoardList[i].Position = new Rectangle(rnd.Next(0, 420),
                rnd.Next(minY - 56, minY - 28), BoardsList.FakeBoardList[i].Position.Width,
                BoardsList.FakeBoardList[i].Position.Height);
            BoardsList.FakeBoardList[i].Visible = true;
        }
    }

    private static void MoveMovingBoard(int i, Random rnd)
    {
        if (BoardsList.MovingBoardList[i].Position.Y > 734)
        {
            var minY = FindMinY();

            BoardsList.MovingBoardList[i].Position = new Rectangle(rnd.Next(0, 420),
                rnd.Next(minY - 56, minY - 28), BoardsList.MovingBoardList[i].Position.Width,
                BoardsList.MovingBoardList[i].Position.Height);
        }
    }

    private static int FindMinY()
    {
        var minY = int.MaxValue;
        foreach (var board in BoardsList.BoardList)
            if (board.Position.Y < minY && board.Visible)
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

        return minY;
    }

    public static void MoveWithPlayer()
    {
        if (Player.PlayerPosition.Y < 300)
        {
            var speed = (int)Player.Speed.Y;
            Player.PlayerPosition = new Rectangle(Player.PlayerPosition.X,
                Player.PlayerPosition.Y - (int)Player.Speed.Y, Player.PlayerPosition.Width,
                Player.PlayerPosition.Height);
            Player.ShootPosition = new Rectangle(Player.PlayerPosition.X + Player.PlayerPosition.Width / 2,
                Player.PlayerPosition.Y + Player.PlayerPosition.Height / 2 + 15, Player.ShootPosition.Width,
                Player.ShootPosition.Height);
            Player.FirePosition = new Vector2(Player.PlayerPosition.X,
                Player.PlayerPosition.Y + Player.PlayerPosition.Height);

            foreach (var board in BoardsList.BoardList)
                board.Position = new Rectangle(board.Position.X, board.Position.Y - speed,
                    board.Position.Width, board.Position.Height);

            for (var i = 0; i < BoardsList.MovingBoardList.Length; i++)
            {
                BoardsList.MovingBoardList[i].Position = new Rectangle(
                    BoardsList.MovingBoardList[i].Position.X,
                    BoardsList.MovingBoardList[i].Position.Y - speed,
                    BoardsList.MovingBoardList[i].Position.Width,
                    BoardsList.MovingBoardList[i].Position.Height);
                BoardsList.FakeBoardList[i].Position = new Rectangle(BoardsList.FakeBoardList[i].Position.X,
                    BoardsList.FakeBoardList[i].Position.Y - speed,
                    BoardsList.FakeBoardList[i].Position.Width,
                    BoardsList.FakeBoardList[i].Position.Height);
                BoardsList.GoneBoardList[i].Position = new Rectangle(BoardsList.GoneBoardList[i].Position.X,
                    BoardsList.GoneBoardList[i].Position.Y - speed,
                    BoardsList.GoneBoardList[i].Position.Width,
                    BoardsList.GoneBoardList[i].Position.Height);
            }

            if (Background.BPosize.Y < 0)
                Background.BPosize = new Rectangle(Background.BPosize.X, Background.BPosize.Y - speed / 2,
                    Background.BPosize.Width, Background.BPosize.Height);
            Background.SPosise1 = new Rectangle(Background.SPosise1.X, Background.SPosise1.Y - speed / 2,
                Background.SPosise1.Width, Background.SPosise1.Height);
            Background.SPosise2 = new Rectangle(Background.SPosise2.X, Background.SPosise2.Y - speed / 2,
                Background.SPosise2.Width, Background.SPosise2.Height);
            Score.Score -= speed / 2;
        }
    }
}