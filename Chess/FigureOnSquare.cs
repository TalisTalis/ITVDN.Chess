namespace Chess
{
    public class FigureOnSquare
    {
        public Figure figure { get; set; }
        public Square square { get; set; }

        public FigureOnSquare(Figure figure, Square square)
        {
            this.figure = figure;
            this.square = square;
        }
    }
}