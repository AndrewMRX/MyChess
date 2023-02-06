using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;

namespace DemoChess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            Chess.Chess chess = new Chess.Chess("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            List<string> list;
            while(true)
            {
                list = chess.GetAllMoves();
                Console.WriteLine(chess.fen);
                //Console.WriteLine(ChessToAscii(chess));
                ColoredOutput(chess);
                Console.WriteLine(chess.IsCheck() ? "CHEEK" : "-");
                foreach (string moves in list)
                    Console.Write(moves + "\t");
                Console.WriteLine();
                Console.Write("> ");
                string move = Console.ReadLine();
                if (move == "q") break;
                if (move == "") move = list[random.Next(list.Count)];
                chess = chess.Move(move);
            }
        }

        static string ChessToAscii (Chess.Chess chess)
        {
            string text = "  +----------------+\n";
            for(int y = 7; y >= 0; y --)
            {
                text += y + 1;
                text += " | ";
                for (int x = 0; x < 8; x++)
                    text += chess.GetFigureAt(x, y) + " ";
                text += "|\n";
            }
            text += "  +----------------+\n";
            text += "    a b c d e f g h";
            return text;
        }

        static void ColoredOutput (Chess.Chess chess)
        {
            string text = "";
            Console.Write("  +----------------+\n");

            for (int y = 7; y >= 0; y--)
            {
                Console.Write(" " + (y + 1) + "|");
                for (int x = 0; x < 8; x++)
                {                   
                    if (chess.GetFigureAt(x, y) >= 'a' && chess.GetFigureAt(x, y) <= 'z')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }                    
                    Console.Write(chess.GetFigureAt(x, y) + " ");
                    Console.ForegroundColor = ConsoleColor.White;                    
                }
                Console.Write("|");
                Console.WriteLine();
            }

            Console.Write("  +----------------+\n");
            Console.Write("    a b c d e f g h");
        }
    }
}
