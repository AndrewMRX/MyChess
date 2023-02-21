using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessCore;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;

namespace ChessCore
{
    public class Rules : MonoBehaviour
    {
        DragAndDrop dad;
        Board board;
        Command cmd1;
        Command cmd2;
        Command activeCmd;
        Command enemyCmd;
        Text message;
        public TextMeshProUGUI checkMateMessage;

        public Rules()
        {
            dad = new DragAndDrop();

            bool ingame = true;
            board = new Board(8, 8);
            cmd1 = new Command("white", "");
            cmd2 = new Command("black", "");
            activeCmd = cmd1;
            enemyCmd = cmd2;
            

            board.StartSetFigures();
            board.FormBoard();

        }
        // Start is called before the first frame update
        public void Start()
        {
            //checkMateMessage = gameObject.AddComponent<TextMeshProUGUI>();
            ShowFigures();
            //MarkValidFigures();
            //checkMateMessage.text = "Empty";
        }

        // Update is called once per frame
        void Update()
        {

            if (dad.picked == 1)
            {
                ShowValidMoves();
            }
            else if (dad.picked == 0)
            {
                UnmarkAllSquares();
                dad.picked = -1;
            }


            if (dad.Action())
            {
                string from = GetSquare(dad.pickPosition);
                //Debug.Log(dad.pickPosition);
                string to = GetSquare(dad.dropPosition);
                //Debug.Log(to + " thisTo");
                //string figure = chess.GetFigureAt(from).ToString();
                //string move = figure + from + to;
                //Debug.Log(move);

                //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                //&&&&&&&&&&&&&&&&PROCESS&&&&&&&&&&&&&&&&&&&&&&&&
                //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                //if (board.isMate == true || board.isPat == true)
                //{
                //    ingame = false;
                //    break;
                //}

                

                //if(board.isCheck == true) break;

                //board.DrawBoard();


                

                board.GetPossibleMoves(activeCmd);


                ValidateMoves();
                

                board.ShowValidMoves();
                Console.WriteLine(board.validMoves.Count);


                activeCmd.value = from + to;
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
                //Console.WriteLine(activeCmd.player);
                //board.testField = board.field.Clone() as char[,];
                //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                //&&&&&&&&&&&&&&&&&&&&END&&&&&&&&&&&&&&&&&&&&&&&&&&
                //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                board.GetPossibleMoves(enemyCmd);
                board.isCheck = board.IsCheck();


                //^^^^^^^CHECK MESSAGE^^^^^^


                if (board.isCheck == true)
                {
                    //MarkSquare(0, 0, true);
                    checkMateMessage.text = "CHECK!";
                }

                else checkMateMessage.text = "";

                board.GetPossibleMoves(activeCmd);
                ValidateMoves();

                if (board.validMoves.Count == 0)
                {
                    if (board.isCheck == true)
                    {
                        board.isMate = true;
                        checkMateMessage.text = "CheckMate";
                    }

                    else
                    {
                        board.isPat = true;
                        checkMateMessage.text = "PAT";
                    }

                }
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^


                //chess = chess.Move(move);

                ShowFigures();
                //MarkValidFigures();

            }
        }

        public void ValidateMoves()
        {
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
        }

        public void ButtonTest()
        {
            PlaceFigure("box55", "q", 5, 5);
            Debug.Log("button clicked");
        }

        string GetSquare(Vector2 position)
        {
            int x = Convert.ToInt32(position.x / 2.0);
            //Debug.Log(x);
            int y = Convert.ToInt32(position.y / 2.0);
            //Debug.Log(y);
            return ((char)('a' + x)).ToString() + (y + 1).ToString();
        }

        void ShowFigures()
        {
            int nr = 0;
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    int indexInFigArray = board.GetFigureAt(x, y);
                    string figure;
                    if (indexInFigArray != -1)
                    {
                        figure = board.figures[indexInFigArray].name.ToString();
                        //Debug.Log(x + " " + y);
                    }
                    else continue;
                    PlaceFigure("box" + nr, figure, x, y);
                    nr++;
                }
            for (; nr < 32; nr++)
                PlaceFigure("box" + nr, "q", 9, 9);
            //MarkSquare(0,0,true);
        }
        void PlaceFigure(string box, string figure, int x, int y)
        {
            //Debug.Log(box + " " + figure + " " + x + " " + y);
            GameObject goBox = GameObject.Find(box);
            GameObject goFigure = GameObject.Find(figure);
            GameObject goSquare = GameObject.Find("" + y + x);

            var spriteFigure = goFigure.GetComponent<SpriteRenderer>();
            var spriteBox = goBox.GetComponent<SpriteRenderer>();
            spriteBox.sprite = spriteFigure.sprite;

            goBox.transform.position = goSquare.transform.position;
        }

        void MarkValidFigures()
        {
            //for (int y = 0; y < 8; y++)
            //    for (int x = 0; x < 8; x++)
            //        MarkSquare(x, y, true);
            //foreach (string moves in chess.GetAllMoves() )
            //{
            //    int x,y;
            //    GetCoord(moves.Substring(1,2),out x, out y);
            //    MarkSquare(x, y, true);
            //}
        }

        void GetCoord(string name, out int x, out int y) // e2, h8, g5...
        {
            x = 9;
            y = 9;
            if (name.Length == 2 &&
                name[0] >= 'a' && name[0] <= 'h' &&
                name[1] >= '1' && name[1] <= '8')
            {
                x = name[0] - 'a';
                y = name[1] - '1';
            }
        }

        private void MarkSquare(int x, int y, bool isMarked)
        {
            GameObject goSquare = GameObject.Find("" + x + y);
            GameObject goCell;
            string color = (x + y) % 2 == 0 ? "Black" : "White";
            if (isMarked)
                goCell = GameObject.Find(color + "SquareMarked");
            else
                goCell = GameObject.Find(color + "Square");
            var spriteSquare = goSquare.GetComponent<SpriteRenderer>();
            var spriteCell = goCell.GetComponent<SpriteRenderer>();
            spriteSquare.sprite = spriteCell.sprite;
        }
        public void ShowValidMoves()
        {
            board.GetPossibleMoves(activeCmd);
            ValidateMoves();
            Debug.Log(((dad.pickPosition.x) / 1) + " " + ((dad.pickPosition.y) / 1));
            foreach (var el in board.validMoves)
            {
                int x1 = Convert.ToInt32(el[0] - 'a');
                int y1 = 7 - Convert.ToInt32(el[1] - '1');
                int pickedX = Convert.ToInt32(dad.pickPosition.x) / 2;
                int pickedY = 7 - (Convert.ToInt32(dad.pickPosition.y) / 2);
                //Debug.Log(pickedX + " " + pickedY + " " + x1 + " " + y1);
                if (pickedX == x1 && pickedY == y1)
                {
                    //Debug.Log(el);
                    int x = Convert.ToInt32(el[2] - 'a');
                    int y = 7 - Convert.ToInt32(el[3] - '1');
                    //Debug.Log(x + " " + y);
                    MarkSquare(y, x, true);
                }

            }
        }
        public void UnmarkAllSquares()
        {
            for (int y = 0; y < 8; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    MarkSquare(y, x, false);
                }
            }
            
        }
    }

    class DragAndDrop
    {
        public enum State
        {
            none,
            drag
        }

        public int picked;

        public Vector2 pickPosition { get; private set; }
        public Vector2 dropPosition { get; private set; }

        public State state;
        GameObject item;
        Vector2 offset;
        public DragAndDrop()
        {
            state = State.none;
            item = null;
            picked = 0;
        }

        public bool Action()
        {
            switch (state)
            {
                case State.none:
                    if (IsMouseButtonPressed())
                        PickUp();
                    break;
                case State.drag:
                    if (IsMouseButtonPressed())
                        Drag();
                    else
                    {
                        Drop();
                        return true;
                    }

                    break;
            }
            return false;
        }

        bool IsMouseButtonPressed()
        {
            return Input.GetMouseButton(0);
        }

        void PickUp()
        {
            Vector2 clickPosition = GetClickPosition();
            Transform clickedItem = GetItemAt(clickPosition);
            if (clickedItem == null) return;
            pickPosition = clickedItem.position;
            item = clickedItem.gameObject;
            state = State.drag;
            picked = 1;
            offset = pickPosition - clickPosition;
            //Debug.Log("picked up: " + item.name);
        }

        Vector2 GetClickPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        Transform GetItemAt(Vector2 position)
        {
            RaycastHit2D[] figures = Physics2D.RaycastAll(position, position, 0.5f);
            if (figures.Length == 0)
                return null;
            return figures[0].transform;
        }

        void Drag()
        {
            item.transform.position = GetClickPosition() + offset;           
        }

        void Drop()
        {
            dropPosition = item.transform.position;
            state = State.none;
            picked = 0;
            item = null;
        }

       
    }


}