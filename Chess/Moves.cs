using System;

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

        bool CanMoveTo()
        {
            return fm.to.OnBoard() &&
                   fm.from != fm.to &&
                   board.GetFigureAt(fm.to).GetColor() != board.moveColor;
        }

        public bool CanFigureMove()
        {
            switch (fm.figure)
            {
                case Figure.WhiteKing:
                case Figure.BlackKing:
                    return CanKingMove();

                case Figure.WhiteQueen:
                case Figure.BlackQueen:
                    return false;

                case Figure.WhiteRook:
                case Figure.BlackRook:
                    return false;

                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return false;

                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return CanKnightMove();

                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return false;

                default: return false;
            }
        }

        public bool CanKingMove()
        {
            if (fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1)
            {
                return true;
            }
            return false;
        }

        public bool CanKnightMove()
        {
            if (fm.AbsDeltaX == 1 && fm.AbsDeltaY == 2)
            {
                return true;
            }
            if (fm.AbsDeltaX == 2 && fm.AbsDeltaY == 1)
            {
                return true;
            }
            return false;
        }
    }
}
