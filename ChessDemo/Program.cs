using System;

namespace ChessDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Chess.Chess chess = new Chess.Chess();

            while (true)
            {
                Console.WriteLine(chess.Fen);
                Console.WriteLine(ChessToAscii(chess));
                string move = Console.ReadLine();

                if (move == "")
                {
                    break;
                }

                chess = chess.Move(move);
            }
        }

        private static string ChessToAscii(Chess.Chess chess)
        {
            string text = "  +-----------------+\n";

            for (int y = 7; y >= 0; y--)
            {
                text += y + 1;
                text += " | ";

                for (int x = 0; x < 8; x++)
                {
                    text += chess.GetFigureAt(x, y) + " ";
                }

                text += "|\n";
            }
            
            text += "  +-----------------+\n";
            text += "    a b c d e f g h\n";

            return text;
        }
    }
}