namespace Chess
{
    public class Moves
    {
        FigureMoving fm;
        Board board;

        // конструктор
        public Moves(Board board)
        {
            this.board = board;
        }

        public bool CanMove(FigureMoving fm)
        {
            this.fm = fm;
            return
                CanMoveFrom() && // можно ли пойти с клетки
                CanMoveTo() && // можно ли пойти на ту клетку
                CanFigureMove(); // может ли фигура сходить
        }

        bool CanMoveFrom()
        {
            return fm.from.OnBoard() &&
                    fm.figure.GetColor() == board.moveColor;
        }
    }
}
