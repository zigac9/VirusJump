﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusJump.Classes.Scene.Objects.Boards
{
    public class BoardsCoords
    {
        private List<Rectangle> _boardsCoordsArr;
        private List<Rectangle> _fakeBoardsCoordsArr;
        private List<Rectangle> _goneBoardsCoordsArr;
        private List<Rectangle> _movingBoardsCoordsArr;

        public BoardsCoords() 
        {
            _boardsCoordsArr = new List<Rectangle>();
            _goneBoardsCoordsArr = new List<Rectangle>();
            _fakeBoardsCoordsArr = new List<Rectangle>();
            _movingBoardsCoordsArr = new List<Rectangle>();
            Initialize();
        }

        public void Initialize()
        {
            _boardsCoordsArr.Add(new Rectangle(40, 700, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(240, 640, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(400, 620, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(250, 600, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(290, 570, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(240, 550, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(200, 580, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(160, 530, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(100, 500, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(180, 460, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(270, 430, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(310, 390, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(350, 360, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(310, 320, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(310, 290, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(200, 250, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(160, 220, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(140, 180, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(400, 140, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(360, 110, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(300, 70, 60, 14));
            _boardsCoordsArr.Add(new Rectangle(240, 40, 60, 14));

            _goneBoardsCoordsArr.Add(new Rectangle(100, 263, 60, 14));
            _goneBoardsCoordsArr.Add(new Rectangle(30, 330, 60, 14));
            _goneBoardsCoordsArr.Add(new Rectangle(410, -50, 60, 14));
            _goneBoardsCoordsArr.Add(new Rectangle(74, 660, 60, 14));

            _movingBoardsCoordsArr.Add(new Rectangle(100, 335, 60, 14));
            _movingBoardsCoordsArr.Add(new Rectangle(250, 15, 60, 14));
            _movingBoardsCoordsArr.Add(new Rectangle(0, -20, 60, 14));
            _movingBoardsCoordsArr.Add(new Rectangle(400, 630, 60, 14));

            _fakeBoardsCoordsArr.Add(new Rectangle(420, 435, 60, 14));
            _fakeBoardsCoordsArr.Add(new Rectangle(50, 120, 60, 14));
            _fakeBoardsCoordsArr.Add(new Rectangle(20, -35, 60, 14));
            _fakeBoardsCoordsArr.Add(new Rectangle(250, 525, 60, 14));
        }

        public List<Rectangle> BoardsCoordsArr
        {
            get { return _boardsCoordsArr; }
            set { _boardsCoordsArr = value; }
        }

        public List<Rectangle> FakeBoardsCoordsArr
        {
            get { return _fakeBoardsCoordsArr; }
            set { _fakeBoardsCoordsArr = value; }
        }

        public List<Rectangle> GoneBoardsCoordsArr
        {
            get { return _goneBoardsCoordsArr; }
            set { _goneBoardsCoordsArr = value; }
        }

        public List<Rectangle> MovingBoardsCoordsArr
        {
            get { return _movingBoardsCoordsArr; }
            set { _movingBoardsCoordsArr = value; }
        }
    }
}