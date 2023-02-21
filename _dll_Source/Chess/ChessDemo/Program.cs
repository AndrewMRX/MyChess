using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessCore;

public static class Program
{
    static void Main()
    {
        bool ingame = true;
        Board board = new Board(8, 8);
        Command cmd1 = new Command("white", "");
        Command cmd2 = new Command("black", "");
        Command activeCmd = cmd1;
        Command enemyCmd = cmd2;
        board.StartSetFigures();
        board.FormBoard();
        //board.DrawBoard();
        //board.DrawBoardDebug();


        while (ingame == true)
        {
            if (board.isMate == true || board.isPat == true)
            {
                ingame = false;
                break;
            }

            board.GetPossibleMoves(enemyCmd);
            board.isCheck = board.IsCheck();

            //if(board.isCheck == true) break;

            board.DrawBoard();



            board.GetPossibleMoves(activeCmd);



            //Validate Moves
            //**************
            board.validMoves.Clear();
            //Console.WriteLine("Count" + board.possibleMoves.Count);
            for (int i = 0; i < board.possibleMoves.Count; i++)
            {
                //Console.WriteLine(Board.possibleMoves[i]);
                Board testBoard = new Board(board);
                //testBoard.StartSetFigures();
                testBoard.FormBoard();
                Command testCmd = new Command(activeCmd.player, board.possibleMoves[i]);
                testCmd.value = Command.ToSystemCoords(testCmd.value);
                testCmd = Command.FillIntCmd(testCmd);


                //Console.WriteLine(testBoard.possibleMoves[i]);

                //Console.WriteLine("now test cmd " + testCmd.value + " " + testCmd.intvalues[0] + testCmd.intvalues[1] + testCmd.intvalues[2] + testCmd.intvalues[3]);
                //Thread.Sleep(1000);

                testBoard.Move(testCmd);
                testBoard.FormBoard();
                if (testCmd.player == "white")
                    testCmd.player = "black";
                else testCmd.player = "white";
                testBoard.GetPossibleMoves(testCmd);


                testBoard.moveColor = testCmd.player;
                if (testBoard.IsCheck2() == false)
                {
                    board.validMoves.Add(board.possibleMoves[i]);
                }

                //testBoard.DrawBoard();
                //testBoard.ShowPossibleMoves();
                //Thread.Sleep(1000);

            }
            //**************

            board.ShowValidMoves();
            Console.WriteLine(board.validMoves.Count);


            activeCmd.value = Console.ReadLine();
            activeCmd.value = Command.ToSystemCoords(activeCmd.value);
            activeCmd = Command.FillIntCmd(activeCmd);

            if (board.Move(activeCmd) == true)
            {
                if (activeCmd == cmd2)
                {
                    activeCmd = cmd1;
                    enemyCmd = cmd2;
                }
                else
                {
                    activeCmd = cmd2;
                    enemyCmd = cmd1;
                }
            }
            board.moveColor = activeCmd.player;
            Console.WriteLine(activeCmd.value);
            Console.WriteLine(activeCmd.intvalues[0] + " " + activeCmd.intvalues[1] + " " + activeCmd.intvalues[2] + " " + activeCmd.intvalues[3]);
            board.FormBoard();
            //board.DrawBoard();
            Console.WriteLine(activeCmd.player);
            //board.testField = board.field.Clone() as char[,];

        }

        //Console.WriteLine(board.figures.Count);
    }
}


