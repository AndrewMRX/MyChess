using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessCore;

namespace ChessCore
{
    public class Board
    {
        public int length;
        public int hight;
        public char[,] field;
        public char[,] testField;
        public string moveColor;
        public List<Figure> figures;
        public List<string> possibleMoves;
        public List<string> validMoves;

        public bool isCheck;
        public bool isMate;
        public bool isPat;

        public string error;

        public Board(int l, int h)
        {
            this.length = l;
            this.hight = h;
            this.field = new char[8, 8] {{'.','.','.','.','.','.','.','.'},
                                             {'.','.','.','.','.','.','.','.'},
                                             {'.','.','.','.','.','.','.','.'},
                                             {'.','.','.','.','.','.','.','.'},
                                             {'.','.','.','.','.','.','.','.'},
                                             {'.','.','.','.','.','.','.','.'},
                                             {'.','.','.','.','.','.','.','.'},
                                             {'.','.','.','.','.','.','.','.'}};

            this.moveColor = "white";
            this.figures = new List<Figure>(0);
            this.possibleMoves = new List<string>(0);
            this.validMoves = new List<string>(0);

            this.isCheck = false;
            this.isMate = false;
            this.isPat = false;

            this.error = "null error";
        }

        public Board(Board copyBoard)
        {
            this.length = copyBoard.length;
            this.hight = copyBoard.hight;
            this.field = copyBoard.field.Clone() as char[,];

            this.moveColor = copyBoard.moveColor;

            this.figures = new List<Figure>(0);
            for (int i = 0; i < copyBoard.figures.Count; i++)
            {
                this.figures.Add(new Figure(copyBoard.figures[i]));

            }
            //this.figures = new List<Figure> (copyBoard.figures);
            this.possibleMoves = new List<string>(copyBoard.possibleMoves);
            this.validMoves = new List<string>(copyBoard.validMoves);

            this.isCheck = copyBoard.isCheck;
            this.isMate = copyBoard.isMate;
            this.isPat = copyBoard.isPat;

            this.error = copyBoard.error;
        }

        // Расставляем фигуры перед партией
        public void StartSetFigures()
        {
            this.figures.Add(new Figure('r', "black", 0, 0));
            this.figures.Add(new Figure('n', "black", 1, 0));
            this.figures.Add(new Figure('b', "black", 2, 0));
            this.figures.Add(new Figure('q', "black", 3, 0));
            this.figures.Add(new Figure('k', "black", 4, 0));
            this.figures.Add(new Figure('b', "black", 5, 0));
            this.figures.Add(new Figure('n', "black", 6, 0));
            this.figures.Add(new Figure('r', "black", 7, 0));

            //*
            this.figures.Add(new Figure('p', "black", 0, 1));
            this.figures.Add(new Figure('p', "black", 1, 1));
            this.figures.Add(new Figure('p', "black", 2, 1));
            this.figures.Add(new Figure('p', "black", 3, 1));
            this.figures.Add(new Figure('p', "black", 4, 1));
            this.figures.Add(new Figure('p', "black", 5, 1));
            this.figures.Add(new Figure('p', "black", 6, 1));
            this.figures.Add(new Figure('p', "black", 7, 1));
            //*/

            this.figures.Add(new Figure('R', "white", 0, 7));
            this.figures.Add(new Figure('N', "white", 1, 7));
            this.figures.Add(new Figure('B', "white", 2, 7));
            this.figures.Add(new Figure('Q', "white", 3, 7));
            this.figures.Add(new Figure('K', "white", 4, 7));
            this.figures.Add(new Figure('B', "white", 5, 7));
            this.figures.Add(new Figure('N', "white", 6, 7));
            this.figures.Add(new Figure('R', "white", 7, 7));

            //*
            this.figures.Add(new Figure('P', "white", 0, 6));
            this.figures.Add(new Figure('P', "white", 1, 6));
            this.figures.Add(new Figure('P', "white", 2, 6));
            this.figures.Add(new Figure('P', "white", 3, 6));
            this.figures.Add(new Figure('P', "white", 4, 6));
            this.figures.Add(new Figure('P', "white", 5, 6));
            this.figures.Add(new Figure('P', "white", 6, 6));
            this.figures.Add(new Figure('P', "white", 7, 6));
            //*/
        }

        public int GetFigureAt(int x, int y)
        {
            for (int i = 0; i < this.figures.Count; i++)
            {
                if (this.figures[i].posx == x && this.figures[i].posy == y)
                    return i; //return figure index in figures
            }
            return -1;

        }

        public int GetEnemyKing()
        {
            for (int i = 0; i < this.figures.Count; i++)
            {
                if (this.moveColor == "white" && this.figures[i].name == 'k')
                    return i;
                else if (this.moveColor == "black" && this.figures[i].name == 'K')
                    return i;
            }
            return -1;
        }

        public int GetFriendKing()
        {
            for (int i = 0; i < this.figures.Count; i++)
            {
                if (this.moveColor == "white" && this.figures[i].name == 'K')
                    return i;
                else if (this.moveColor == "black" && this.figures[i].name == 'k')
                    return i;
            }
            return -1;
        }

        public void FormBoard()
        {
            for (int i = 0; i < this.figures.Count; i++)
            {
                this.field[figures[i].posx, figures[i].posy] = figures[i].name;
            }
        }

        public void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("  a b c d e f g h\n");
            for (int j = 0; j < 8; j++)
            {
                Console.Write(8 - j + " ");
                for (int i = 0; i < 8; i++)
                {

                    if (this.field[i, j] >= 'a' && this.field[i, j] <= 'z')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(this.field[i, j] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.Write(this.field[i, j] + " ");
                }
                Console.Write(" " + (8 - j));
                Console.WriteLine();
            }
            Console.WriteLine("\n  a b c d e f g h");
            Console.WriteLine(this.moveColor);
            //Console.WriteLine(this.figures.Count);
            //Console.WriteLine(error);
            if (this.isCheck == true)
                Console.WriteLine("Check");
            if (this.isMate == true)
                Console.WriteLine("Mate");
            if (this.isPat == true)
                Console.WriteLine("Pat");

        }

        public void DrawBoardDebug()
        {
            Console.Clear();
            Console.WriteLine(" 0 1 2 3 4 5 6 7\n");
            for (int j = 0; j < 8; j++)
            {
                Console.Write(j + " ");
                for (int i = 0; i < 8; i++)
                {

                    if (this.field[i, j] >= 'a' && this.field[i, j] <= 'z')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(this.field[i, j] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.Write(this.field[i, j] + " ");
                }
                Console.Write(" " + (j));
                Console.WriteLine();
            }
            Console.WriteLine("\n 0 1 2 3 4 5 6 7");

        }

        public void GetPossibleMoves(Command cmd)
        {
            Command cmdpm = new Command(cmd);
            this.possibleMoves.Clear();
            StringBuilder sb = new StringBuilder("");

            for (int i = 0; i < this.figures.Count; i++)
            {

                if (this.figures[i].color == cmdpm.player)
                {

                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            cmdpm.intvalues[0] = this.figures[i].posx;
                            cmdpm.intvalues[1] = this.figures[i].posy;
                            cmdpm.intvalues[2] = x;
                            cmdpm.intvalues[3] = y;
                            //Console.Write(this.figures[i].color + " ");
                            if (this.CanMove(cmdpm))
                            {
                                sb.Append(this.figures[i].posx.ToString());
                                sb.Append(this.figures[i].posy.ToString());
                                sb.Append(x.ToString());
                                sb.Append(y.ToString());
                                this.possibleMoves.Add(Command.ToChessCoords(sb.ToString()));
                                sb.Clear();
                            }
                        }
                    }
                    //Console.WriteLine("end");
                }
            }

        }

        public void ShowPossibleMoves()
        {
            foreach (var el in this.possibleMoves)
            {
                Console.Write(el + " ");
            }
            Console.WriteLine();
        }

        public void ShowValidMoves()
        {
            foreach (var el in this.validMoves)
            {
                Console.Write(el + " ");
            }
            Console.WriteLine();
        }

        public bool Move(Command cmd)
        {
            //Thread.Sleep(10000);
            Figure figBuff;
            Figure figBuff2;

            if (this.field[cmd.intvalues[0], cmd.intvalues[1]] != '.')
            {
                figBuff = this.figures[GetFigureAt(cmd.intvalues[0], cmd.intvalues[1])];
                if (CanFigureMove(figBuff, cmd) == false)
                {
                    //Console.WriteLine(figBuff.name + cmd.value + " " + cmd.intvalues[0] + cmd.intvalues[1] + cmd.intvalues[2] + cmd.intvalues[3]);
                    //Thread.Sleep(3000);
                    return false;
                }
                if (cmd.player != figBuff.color)
                    return false;

                if (this.field[cmd.intvalues[2], cmd.intvalues[3]] != '.')
                {
                    figBuff2 = this.figures[GetFigureAt(cmd.intvalues[2], cmd.intvalues[3])];
                    if (figBuff.color == figBuff2.color)
                        return false;
                }

                if (cmd.intvalues[0] == cmd.intvalues[2] && cmd.intvalues[1] == cmd.intvalues[3])
                    return false;

                if (this.field[cmd.intvalues[2], cmd.intvalues[3]] != '.')
                {
                    this.figures.RemoveAt(GetFigureAt(cmd.intvalues[2], cmd.intvalues[3]));
                }

                this.field[cmd.intvalues[0], cmd.intvalues[1]] = '.';

                figBuff = this.figures[GetFigureAt(cmd.intvalues[0], cmd.intvalues[1])];
                figBuff.posx = cmd.intvalues[2];
                figBuff.posy = cmd.intvalues[3];

                figBuff.firstMove = true;
                return true;


                //if(TestCheckSelf(cmd,figBuff) == false) return false;

                //figBuff.firstMove = true;

                //Console.WriteLine("done");
                //Thread.Sleep(10000);

                //return true;
            }
            return false;
        }

        public bool CanMove(Command cmd)
        {
            Figure figBuff;
            Figure figBuff2;

            if (this.field[cmd.intvalues[0], cmd.intvalues[1]] != '.')
            {
                figBuff = this.figures[GetFigureAt(cmd.intvalues[0], cmd.intvalues[1])];
                if (CanFigureMove(figBuff, cmd) == false)
                    return false;

                if (cmd.player != figBuff.color)
                    return false;

                if (this.field[cmd.intvalues[2], cmd.intvalues[3]] != '.')
                {
                    figBuff2 = this.figures[GetFigureAt(cmd.intvalues[2], cmd.intvalues[3])];
                    if (figBuff.color == figBuff2.color)
                        return false;
                }

                if (cmd.intvalues[0] == cmd.intvalues[2] && cmd.intvalues[1] == cmd.intvalues[3])
                    return false;

                //if(this.field[cmd.intvalues[2],cmd.intvalues[3]] != '.')
                //{
                // this.figures.RemoveAt( GetFigureAt(cmd.intvalues[2],cmd.intvalues[3]) );
                //}

                //this.field[cmd.intvalues[0],cmd.intvalues[1]] = '.';

                //figBuff = this.figures[GetFigureAt(cmd.intvalues[0],cmd.intvalues[1])];
                //figBuff.posx = cmd.intvalues[2];
                //figBuff.posy = cmd.intvalues[3];

                //figBuff.firstMove = true;
                return true;
            }
            return false;
        }

        public bool CanFigureMove(Figure figure, Command cmd)
        {
            int xf = cmd.intvalues[0];
            int xt = cmd.intvalues[2];
            int yf = cmd.intvalues[1];
            int yt = cmd.intvalues[3];
            bool block = false;
            switch (figure.name)
            {
                case 'n':
                case 'N':
                    if ((Math.Abs(figure.posx - cmd.intvalues[2]) == 2 && Math.Abs(figure.posy - cmd.intvalues[3]) == 1) ||
                    (Math.Abs(figure.posx - cmd.intvalues[2]) == 1 && Math.Abs(figure.posy - cmd.intvalues[3]) == 2))
                        return true;
                    else return false;
                    break;
                /*
                case 'N':
                if( (Math.Abs(figure.posx - cmd.intvalues[2]) == 2 && Math.Abs(figure.posy - cmd.intvalues[3]) == 1 ) ||
                (Math.Abs(figure.posx - cmd.intvalues[2]) == 1 && Math.Abs(figure.posy - cmd.intvalues[3]) == 2 ) )
                return true;
                else return false;
                break;
                */


                case 'p':
                    if ((figure.posx - cmd.intvalues[2] == 0 && figure.posy - cmd.intvalues[3] == -2) &&
                    this.field[cmd.intvalues[2], cmd.intvalues[3] - 1] == '.' &&
                    this.field[cmd.intvalues[2], cmd.intvalues[3]] == '.' &&
                    figure.firstMove == false)
                    {
                        return true;
                    }
                    else if (figure.posx - cmd.intvalues[2] == 0 && figure.posy - cmd.intvalues[3] == -1 &&
                    this.field[cmd.intvalues[2], cmd.intvalues[3]] == '.')
                        return true;

                    else if (
                    Math.Abs(figure.posx - cmd.intvalues[2]) == 1 && (figure.posy - cmd.intvalues[3]) == -1 &&
                    this.field[cmd.intvalues[2], cmd.intvalues[3]] != '.'
                    )
                        return true;

                    else return false;
                    break;

                case 'P':
                    if ((figure.posx - cmd.intvalues[2] == 0 && figure.posy - cmd.intvalues[3] == 2) &&
                    this.field[cmd.intvalues[2], cmd.intvalues[3] + 1] == '.' &&
                    this.field[cmd.intvalues[2], cmd.intvalues[3]] == '.' &&
                    figure.firstMove == false)
                    {
                        return true;
                    }
                    else if (figure.posx - cmd.intvalues[2] == 0 && figure.posy - cmd.intvalues[3] == 1 &&
                    this.field[cmd.intvalues[2], cmd.intvalues[3]] == '.')
                        return true;

                    else if (
                    Math.Abs(figure.posx - cmd.intvalues[2]) == 1 && (figure.posy - cmd.intvalues[3]) == 1 &&
                    this.field[cmd.intvalues[2], cmd.intvalues[3]] != '.'
                    )
                        return true;

                    else return false;
                    break;

                case 'r':
                case 'R':
                    //int xf = cmd.intvalues[0];
                    //int xt = cmd.intvalues[2];
                    //int yf = cmd.intvalues[1];
                    //int yt = cmd.intvalues[3];
                    //bool block = false;
                    if (xf == xt && yf != yt)
                    {
                        for (int i = Math.Min(yf, yt) + 1; i < Math.Max(yf, yt); i++)
                        {
                            if (this.field[xf, i] != '.')
                            {
                                block = true;
                                break;
                            }
                        }
                        if (block)
                            return false;
                        else return true;
                    }
                    else if (xf != xt && yf == yt)
                    {
                        for (int i = Math.Min(xf, xt) + 1; i < Math.Max(xf, xt); i++)
                        {
                            if (this.field[i, yf] != '.')
                            {
                                block = true;
                                break;
                            }
                        }
                        if (block)
                            return false;
                        else return true;
                    }
                    else return false;
                    break;
                /*
                case 'R':
                xf = cmd.intvalues[0];
                xt = cmd.intvalues[2];
                yf = cmd.intvalues[1];
                yt = cmd.intvalues[3];
                block = false;
                if(xf == xt && yf != yt)
                {
                for(int i = Math.Min(yf,yt)+1; i < Math.Max(yf,yt); i++ )
                {
                if(this.field[xf,i] != '.')
                {
                block = true;
                break;
                }
                }
                if(block)
                return false;
                else return true;
                }
                else if(xf != xt && yf == yt)
                {
                for(int i = Math.Min(xf,xt)+1; i < Math.Max(xf,xt); i++ )
                {
                if(this.field[i,yf] != '.')
                {
                block = true;
                break;
                }
                }
                if(block)
                return false;
                else return true;
                }
                else return false;
                break;
                */
                case 'B':
                case 'b':
                    //xf = cmd.intvalues[0];
                    //xt = cmd.intvalues[2];
                    //yf = cmd.intvalues[1];
                    //yt = cmd.intvalues[3];
                    //block = false;
                    if (Math.Abs(xf - xt) == Math.Abs(yf - yt))
                    {

                        int direction;
                        int inc = 0;
                        if (yf > yt)//move up
                        {
                            if (xf > xt)
                                inc = -1;
                            else inc = 1;
                            for (int i = yf - 1, j = xf + inc; i > yt; i--)
                            {
                                //this.field[j,i] = '&';
                                if (this.field[j, i] != '.')
                                {
                                    block = true;
                                    //this.error = "blocked";
                                    //this.field[j,i] = 'X';
                                    break;
                                }
                                j += inc;
                            }
                        }
                        else if (yf < yt)//move down
                        {
                            if (xf > xt)
                                inc = -1;
                            else inc = 1;
                            for (int i = yf + 1, j = xf + inc; i < yt; i++)
                            {
                                if (this.field[j, i] != '.')
                                {
                                    block = true;
                                    //this.error = "blocked";
                                    break;
                                }
                                j += inc;
                            }
                        }

                        if (block)
                            return false;
                        else return true;
                    }

                    else return false;
                    break;

                case 'q':
                case 'Q':
                    if (xf == xt && yf != yt)
                    {
                        for (int i = Math.Min(yf, yt) + 1; i < Math.Max(yf, yt); i++)
                        {
                            if (this.field[xf, i] != '.')
                            {
                                block = true;
                                break;
                            }
                        }
                        if (block)
                            return false;
                        else return true;
                    }
                    else if (xf != xt && yf == yt)
                    {
                        for (int i = Math.Min(xf, xt) + 1; i < Math.Max(xf, xt); i++)
                        {
                            if (this.field[i, yf] != '.')
                            {
                                block = true;
                                break;
                            }
                        }
                        if (block)
                            return false;
                        else return true;
                    }
                    else if (Math.Abs(xf - xt) == Math.Abs(yf - yt))
                    {

                        int direction;
                        int inc = 0;
                        if (yf > yt)//move up
                        {
                            if (xf > xt)
                                inc = -1;
                            else inc = 1;
                            for (int i = yf - 1, j = xf + inc; i > yt; i--)
                            {
                                //this.field[j,i] = '&';
                                if (this.field[j, i] != '.')
                                {
                                    block = true;
                                    //this.error = "blocked";
                                    //this.field[j,i] = 'X';
                                    break;
                                }
                                j += inc;
                            }
                        }
                        else if (yf < yt)//move down
                        {
                            if (xf > xt)
                                inc = -1;
                            else inc = 1;
                            for (int i = yf + 1, j = xf + inc; i < yt; i++)
                            {
                                if (this.field[j, i] != '.')
                                {
                                    block = true;
                                    //this.error = "blocked";
                                    break;
                                }
                                j += inc;
                            }
                        }

                        if (block)
                            return false;
                        else return true;
                    }
                    else return false;
                    break;

                case 'k':
                case 'K':
                    if (Math.Abs(xf - xt) <= 1 && Math.Abs(yf - yt) <= 1)
                        return true;
                    else return false;
                    break;

                default: return false;
            }
            return true;
        }

        public bool IsCheck()
        {
            foreach (var el in this.possibleMoves)
            {
                string checkMove = Command.ToSystemCoords(el);
                Figure friendKing = this.figures[GetFriendKing()];
                Command checkCmd = new Command(this.moveColor, checkMove);
                checkCmd = Command.FillIntCmd(checkCmd);
                if (friendKing.posx == checkCmd.intvalues[2] && friendKing.posy == checkCmd.intvalues[3])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsCheck2()
        {
            foreach (var el in this.possibleMoves)
            {
                string checkMove = Command.ToSystemCoords(el);
                Figure enemyKing = this.figures[GetEnemyKing()];
                Command checkCmd = new Command(this.moveColor, checkMove);
                checkCmd = Command.FillIntCmd(checkCmd);
                if (enemyKing.posx == checkCmd.intvalues[2] && enemyKing.posy == checkCmd.intvalues[3])
                {
                    return true;
                }
            }
            return false;
        }
    }
}

