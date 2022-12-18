using Microsoft.Xna.Framework.Content;
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
                var rand = new System.Random();
                int num = rand.Next(-4,4);
                if (num == 0) num = 1;
                _movingBoards[i] = new MovingBoard(_content, _boardsCoords.MovingBoardsCoordsArr[i], num);
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
