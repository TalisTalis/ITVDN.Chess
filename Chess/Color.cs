namespace Chess
{
    public enum Color
    {
        None,
        
        White,
        Black
    }
    
    // метод смены цвета при смене хода
    static class ColorMetods
    {
        public static Color FlipColor(this Color color)
        {
            if (color == Color.Black)
            {
                return Color.White;
            }

            if (color == Color.White)
            {
                return Color.Black;
            }

            return Color.None;
        }
    }
}