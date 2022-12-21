using Microsoft.Xna.Framework.Content;
using VirusJump.Classes.Scene.Objects.Boards.BoardClass;

namespace VirusJump.Classes.Scene.Objects.Boards
{
    public class BoardsList
    {
        private readonly ContentManager _content;
        private readonly BoardsCoords _boardsCoords;

        private Board[] _boards;
        private FakeBoard[] _fakeBoards;
        private GoneBoard[] _goneBoards;
        private MovingBoard[] _movingBoards;
        private Textures _textures;

        public BoardsList(Textures textures)
        {
            _textures = textures;
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
            for (var i = 0; i<_boards.Length; i++)
            {
                _boards[i] = new Board(_textures.Textures1["assets/p1"], _boardsCoords.BoardsCoordsArr[i]);
            }
            for (var i = 0; i < _fakeBoards.Length; i++)
            {
                _fakeBoards[i] = new FakeBoard(_textures.Textures1["assets/p3"], _boardsCoords.FakeBoardsCoordsArr[i]);
            }
            for (var i = 0; i < _goneBoards.Length; i++)
            {
                _goneBoards[i] = new GoneBoard(_textures.Textures1["assets/p4"], _boardsCoords.GoneBoardsCoordsArr[i]);
            }
            for (var i = 0; i < _movingBoards.Length; i++)
            {
                var rand = new System.Random();
                var num = rand.Next(-4,4);
                if (num == 0) num = 1;
                _movingBoards[i] = new MovingBoard(_textures.Textures1["assets/p2"], _boardsCoords.MovingBoardsCoordsArr[i], num);
            }
        }

        public Board[] BoardList
        {
            get => _boards;
            set => _boards = value;
        }

        public FakeBoard[] FakeBoardList
        {
            get => _fakeBoards;
            set => _fakeBoards = value;
        }

        public GoneBoard[] GoneBoardList
        {
            get => _goneBoards;
            set => _goneBoards = value;
        }

        public MovingBoard[] MovingBoardList
        {
            get => _movingBoards;
            set => _movingBoards = value;
        }
    }
}
