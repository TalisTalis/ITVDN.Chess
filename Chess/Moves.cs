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
                    return CanStraightMove();

                case Figure.WhiteRook:
                case Figure.BlackRook:
                    return (fm.SignX == 0 || fm.SignY == 0) &&
                            CanStraightMove();

                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return (fm.SignX != 0 && fm.SignY != 0) &&
                            CanStraightMove();

                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return CanKnightMove();

                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return CanPawnMove();

                default: return false;
            }
        }

        private bool CanPawnMove()
        {
            if (fm.from.y < 1 || fm.from.y > 6)
            {
                return false;
            }

            int stepY = fm.figure.GetColor() == Color.White ? 1 : -1;

            return CanPawnGo(stepY) || 
                   CanPawJump(stepY) ||
                   CanPawnEat(stepY);
        }

        private bool CanPawnEat(int stepY)
        {
            if (board.GetFigureAt(fm.to) != Figure.None)
            {
                if (fm.AbsDeltaX == 1)
                {
                    if (fm.DeltaY == stepY)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CanPawJump(int stepY)
        {
            if (board.GetFigureAt(fm.to) == Figure.None)
            {
                if (fm.DeltaX == 0)
                {
                    if (fm.DeltaY == 2 * stepY)
                    {
                        if (fm.from.y == 1 || fm.from.y == 6)
                        {
                            if (board.GetFigureAt(new Square(fm.from.x, fm.from.y + stepY)) == Figure.None)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool CanPawnGo(int stepY)
        {
            if (board.GetFigureAt(fm.to) == Figure.None)
            {
                if (fm.DeltaX == 0)
                {
                    if (fm.DeltaY == stepY)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanStraightMove()
        {
            Square at = fm.from;

            do
            {
                at = new Square(at.x + fm.SignX, at.y + fm.SignY);

                if (at == fm.to)
                {
                    return true;
                }
            } while (at.OnBoard() &&
                    board.GetFigureAt(at) == Figure.None);

            return false;
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
