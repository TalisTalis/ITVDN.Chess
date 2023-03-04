using System;

namespace Chess
{
    public class FigureMoving
    {
        public Figure figure { get; private set; }
        // откуда идт фигура
        public Square from { get; private set; }
        // куда идет фигура
        public Square to { get; private set; }
        
        // в кого фигура превращается
        public Figure promotion { get; private set; }

        // конструктор
        public FigureMoving (FigureOnSquare fs, Square to, Figure promotion = Figure.None)
        {
            this.figure = fs.figure;
            this.from = fs.square;
            this.to = to;
            this.promotion = promotion;
        }

        // конструктор который принимает ход в текстовом варианте
        public FigureMoving(string move)
        {
            this.figure = (Figure)move[0]; // так как в фигуре есть соответствует символа фигуре
            this.from = new Square(move.Substring(1,2)); // от первого символа два символа
            this.to = new Square(move.Substring(3, 2));
            this.promotion = (move.Length == 6) ? (Figure)move[5] : Figure.None; // если длина строки 6 то возмет символ фигуры если нет то нон
        }

        public int DeltaX { get { return to.x - from.x; } }
        public int DeltaY { get { return to.y - from.y; } }

        public int AbsDeltaX { get { return Math.Abs(DeltaX); } }
        public int AbsDeltaY { get { return Math.Abs(DeltaY); } }

        public int AbsSignX { get { return Math.Sign(DeltaX); } }
        public int AbsSignY { get { return Math.Sign(DeltaY); } }
    }
}