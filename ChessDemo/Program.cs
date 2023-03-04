using System;
using System.Collections.Generic;

namespace ChessDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Random random = new Random();
            Chess.Chess chess = new Chess.Chess(); // "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
            List<string> list;

            while (true)
            {
                list = chess.GetAllMoves();
                Console.WriteLine(chess.Fen);
                Console.WriteLine(ChessToAscii(chess));

                Console.WriteLine(chess.IsCheck() ? "CHECK!" : "");

                foreach (string moves in chess.GetAllMoves())
                {
                    Console.Write(moves + '\t');
                }

                Console.WriteLine();
                Console.Write("> ");
                string move = Console.ReadLine();

                if (move == "q")
                {
                    break;
                }
                if (move == "")
                {
                    move = list[random.Next(list.Count)];
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