using System.Collections.Generic;

namespace Chess
{
    public class Chess
    {
        public string Fen { get; private set; }

        private Board board;
        Moves moves;
        List<FigureMoving> allMoves;
        
        // конструктор
        // передатся фен-параметр.
        // фен-параметр - это нотация кот. позволяет задать любую позицию шахмат
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.Fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
        }

        Chess(Board board)
        {
            this.board = board;
            Fen = this.board.fen;
            moves = new Moves(board);
        }
        public Chess Move(string move) // формат Pe2e4 - пешка с е2 на е4 или Pe7e8Q - пешка превратилась в королеву
        {
            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm))
            {
                return this;
            }
            if (board.IsCheckAfterMove(fm))
            {
                return this;
            }

            Board nextBoard = board.Move(fm);
            
            // новый шаг - создание нового экземпляра
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }
        
        // метод кот. позволяет узнать где находится фигура
        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure f = board.GetFigureAt(square);
            return f == Figure.None ? '.' : (char)f;
        }

        void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();

            foreach (FigureOnSquare fs in board.YieldFigures())
            {
                foreach (Square to in Square.YieldSquares())
                {
                    FigureMoving fm = new FigureMoving(fs, to);

                    if (moves.CanMove(fm))
                    {
                        if (!board.IsCheckAfterMove(fm))
                        {
                            allMoves.Add(fm);
                        }
                    }
                }
            }
        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> list = new List<string>();

            foreach (FigureMoving fm in allMoves)
            {
                list.Add(fm.ToString());
            }

            return list;
        }

        public bool IsCheck()
        {
            return board.IsCheck();
        }
    }
}
