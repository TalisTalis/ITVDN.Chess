using System.Text;

namespace Chess
{
    public class Board
    {
        public string fen { get; private set; }
        Figure[,] figures;
        public Color moveColor { get; private set; }
        public int moveNumber { get; private set; }

        public Board(string fen)
        {
            this.fen = fen;
            figures = new Figure[8, 8];
            Init();
        }

        private void Init()
        {
            // "rnbqkbnr/pppppppp/8/8/8/8/pppppppp/rNBQKBNR w KQkq - 0 1"
            // 0                                             1 2    3 4 5
            // 0 - расположение фигур
            // 1 - кто ходит (белые или черные)
            // 2 - флаги рокировки
            // 3 - битое поле
            // 4 - количество ходов для праавила 50 ходов
            // 5 - текуущий номер хода
            string[] parts = fen.Split();
            if (parts.Length != 6)
            {
                return;
            }
            
            // инициализация фигур
            InitFigures(parts[0]);
            // инициализация цвета
            //InitColor(parts[1]);

            moveColor = parts[1] == "b" ? Color.Black : Color.White;
            moveNumber = int.Parse(parts[5]);
        }

        private void InitFigures(string data)
        {
            for (int i = 8; i >= 2; i--)
            {
                data = data.Replace(i.ToString(), (i - 1).ToString() + "1");
            }

            data = data.Replace("1", ".");

            string[] lines = data.Split('/');

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    figures[x, y] = lines[7 - y][x] == '.' ? Figure.None : (Figure)lines[7 - y][x];
                }
            }
        }

        void GenerateFen()
        {
            fen = FenFigures() + " " +
                   (moveColor == Color.White ? "w" : "b") + 
                   " - - 0 " + moveNumber.ToString();
        }

        string FenFigures()
        {
            StringBuilder sb = new StringBuilder();
            
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(figures[x, y] == Figure.None ? '1' : (char)figures[x, y]);
                }

                if (y > 0)
                {
                    sb.Append('/');
                }
            }

            string eight = "11111111";
            for (int i = 8; i >= 2; i--)
            {
                sb.Replace(eight.Substring(0, i), i.ToString());
            }

            return sb.ToString();
        }
        
        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
            {
                return figures[square.x, square.y];
            }

            return Figure.None;
        }

        private void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
            {
                figures[square.x, square.y] = figure;
            }
        }

        public Board Move(FigureMoving fm)
        {
            Board next = new Board(fen);
            next.SetFigureAt(fm.from, Figure.None);
            next.SetFigureAt(fm.to,fm.promotion == Figure.None ? fm.figure : fm.promotion);

            if (moveColor == Color.Black)
            {
                next.moveNumber++;
            }

            next.moveColor = moveColor.FlipColor();
            next.GenerateFen();
            return next;
        }
    }
}
