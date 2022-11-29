using Microsoft.Xna.Framework.Content;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirusJump.Classes.Scene.Objects.Boards.BoardClass;

namespace VirusJump.Classes.Scene.Objects.Boards
{
    public class BoardsList
    {
        private ContentManager _content;
        private BoardsCoords _boardsCoords;

        private Board[] _boards;
        private FakeBoard[] _fakeBoards;
        private GoneBoard[] _goneBoards;
        private MovingBoard[] _movingBoards;

        public BoardsList(ContentManager content)
        {
            _content = content;
            _boardsCoords = new BoardsCoords();
            Initialize();
        }

        public void Initialize()
        {
            _boards = new Board[_boardsCoords.BoardsCoordsArr.Count];
            _fakeBoards = new FakeBoard[_boardsCoords.FakeBoardsCoordsArr.Count];
            _goneBoards = new GoneBoard[_boardsCoords.GoneBoardsCoordsArr.Count];
            _movingBoards = new MovingBoard[_boardsCoords.MovingBoardsCoordsArr.Count];
            FillArrays();
        }

        private void FillArrays()
        {
            for (int i = 0; i<_boards.Length; i++)
            {
                _boards[i] = new Board(_content, _boardsCoords.BoardsCoordsArr[i]);
            }
            for (int i = 0; i < _fakeBoards.Length; i++)
            {
                _fakeBoards[i] = new FakeBoard(_content, _boardsCoords.FakeBoardsCoordsArr[i]);
            }
            for (int i = 0; i < _goneBoards.Length; i++)
            {
                _goneBoards[i] = new GoneBoard(_content, _boardsCoords.GoneBoardsCoordsArr[i]);
            }
            for (int i = 0; i < _movingBoards.Length; i++)
            {
                var exclude = new HashSet<int>() { 0 };
                var range = Enumerable.Range(-4, 4).Where(i => !exclude.Contains(i));
                var rand = new System.Random();
                _movingBoards[i] = new MovingBoard(_content, _boardsCoords.MovingBoardsCoordsArr[i], rand.Next(-4,4 -exclude.Count));
            }
        }

        public Board[] BoardList
        {
            get { return _boards; }
            set { _boards = value; }
        }

        public FakeBoard[] FakeBoardList
        {
            get { return _fakeBoards; }
            set { _fakeBoards = value; }
        }

        public GoneBoard[] GoneBoardList
        {
            get { return _goneBoards; }
            set { _goneBoards = value; }
        }

        public MovingBoard[] MovingBoardList
        {
            get { return _movingBoards; }
            set { _movingBoards = value; }
        }
    }
}
