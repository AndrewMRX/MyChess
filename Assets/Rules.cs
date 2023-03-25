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
using UnityEngine.Networking;
using System.Threading;
using UnityEngine.Rendering;
using System.Text;
using System.Threading.Tasks;
//using UnityEditor.SceneManagement;

namespace ChessCore
{
    [System.Serializable]
    public class Message
    {
        public string text;
        [SerializeField] public TMP_Text textObject;
    }

    public class Rules : MonoBehaviour
    {
        string playerPlayingAs = ServerAnswers.MyColor;

        DragAndDrop dad;
        Board board;
        static bool ingame = true;
        string style = "wood";
        Command cmd1;
        Command cmd2;
        Command activeCmd;
        Command enemyCmd;
        Text message;
        public int maxMessages = 25;
        public GameObject chatPanel, textObject;
        [SerializeField] public TextMeshProUGUI checkMateMessage;
        [SerializeField] public GameObject content;
        [SerializeField] List<Message> messageList = new List<Message>();
        //[SerializeField] public ScrollView scrollView;
        //[SerializeField] private Scrollbar scrollBar;
        //[SerializeField] private GUIContent scrollViewContent;
        //public TextMeshPro

        bool proceesFinished = false;
        int proceesFinishedInt = 0;

        public Rules()
        {
            dad = new DragAndDrop();

            ingame = true;
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
            var messages = FindObjectsOfType<TextMeshProUGUI>();
            foreach(var m in messages)
            {
                
                if (m.text == "checkMess")
                {
                    checkMateMessage = m;
                }
                //if (m.text == "Suzuki")
                //{
                //    Destroy(m);
                //    //var item_go = m.transform.parent;
                //    var newText = gameObject.AddComponent<TextMeshProUGUI>();
                //    newText.text = "mitsu";
                //    Instantiate(newText, item_go.transform);
                //    Debug.Log("NEWTEXT TEXT: " + newText.text);
                //    Debug.Log("ITEMGO NAME: " + item_go.name);
                //}

            }
            checkMateMessage.text = "";

            //this.board.figures[0].name = 'r';           
            //checkMateMessage = gameObject.AddComponent<TextMeshProUGUI>();
            ShowFigures();
            //MarkValidFigures();
            //checkMateMessage.text = "Empty";
        }

        public void SendMessageToChat(string text)
        {
            Message newMessage = new Message();
            newMessage.text = text;
            GameObject newText = Instantiate(textObject, chatPanel.transform);
            newMessage.textObject = newText.GetComponent<TMP_Text>();
            newMessage.textObject.text = newMessage.text;
            messageList.Add(newMessage);
        }

        public void DelayRequest()
        {          
            Debug.Log("I REQUEST SERVER");
            //SendMove_Get(PlayerData.NickName,"111");
            RequestMove_Get(ServerAnswers.EnemyName);
            proceesFinished = false;
            //ServerAnswers.prevMove = ServerAnswers.serverAnswerMove;              
        }

        // Update is called once per frame
        void Update()
        {
            //Task.Delay(4000);    
            if (Rules.ingame)
            {
                checkMateMessage.text = "S111";
                if (board.moveColor == "white" && playerPlayingAs == "white")
                {
                    ShowMoveAndMove();
                    checkMateMessage.text = playerPlayingAs;
                    
                }
                else if(board.moveColor == "black" && playerPlayingAs == "white")
                {
                    GeneralProcees(true);
                    checkMateMessage.text = playerPlayingAs;
                    
                    proceesFinishedInt++;
                    if (proceesFinishedInt > 100)
                    {
                        proceesFinishedInt = 0;
                        StartCoroutine("DelayRequest");
                    }
                }
                else if (board.moveColor == "white" && playerPlayingAs == "black")
                {
                    GeneralProcees(true);
                    checkMateMessage.text = playerPlayingAs;

                    proceesFinishedInt++;
                    if (proceesFinishedInt > 100)
                    {
                        proceesFinishedInt = 0;
                        StartCoroutine("DelayRequest");
                    }
                }
                else if (board.moveColor == "black" && playerPlayingAs == "black")
                {
                    ShowMoveAndMove();
                    checkMateMessage.text = playerPlayingAs;
                }
            }
        }

        private void ShowMoveAndMove()
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
            GeneralProcees(false);
        }

        private void GeneralProcees(bool noAction)
        {
 
            if (dad.Action() || noAction == true)
            {

                //Debug.Log("dad.Action = true");
                string from;
                string to;
                if (noAction == false)
                {
                    from = GetSquare(dad.pickPosition);
                    //Debug.Log(from + " !!!!!!!!!!!!!!!!thisfrom");
                    to = GetSquare(dad.dropPosition);
                    //Debug.Log(to + " !!!!!!!!!!!!!!!!!!thisTo");
                }
                else
                {
                    //SendMove_Get("sss");
                    StringBuilder sb1 = new StringBuilder("a1");
                    StringBuilder sb2 = new StringBuilder("a1");

                    if (ServerAnswers.serverAnswerMove.Length == 4)
                    {
                        
                        sb1[0] = ServerAnswers.serverAnswerMove[0];
                        sb1[1] = ServerAnswers.serverAnswerMove[1];
                        sb2[0] = ServerAnswers.serverAnswerMove[2];
                        sb2[1] = ServerAnswers.serverAnswerMove[3];
                    }
                    else
                    {
                        sb1[0] = 'a';
                        sb1[1] = '1';
                        sb2[0] = 'a';
                        sb2[1] = '1';
                    }

                    //Debug.Log("SB = " + sb1);

                    from = Convert.ToString(sb1);

                    //Debug.Log(from + " thisfrom");
                    to = Convert.ToString(sb2);
                    //Debug.Log(to + " thisTo");
                    //Debug.Log(ServerAnswers.serverAnswerMove);


                }

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


                //foreach (var mov in board.validMoves)
                //{
                //    Debug.Log("VALIDE MOVE: " + mov);
                //}
                
                Command validCommand = new(activeCmd);
                validCommand.value  = Command.ToChessCoords(validCommand.value);
                //Debug.Log("ACTIVE VALIDATE MOVE: " + validCommand.value);
                if (board.validMoves.Contains(validCommand.value))
                {
                    if (board.Move(activeCmd) == true)
                    {
                        SendMessageToChat(from + to);
                        if(noAction == false)
                        {
                            SendMove_Get(PlayerData.NickName, (from + to));
                        }

                        //SendMove_GetSecond(from + to);

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


                //^^^^^^^CHECK MESSAGE^^^^^^&&


                if (board.isCheck == true)
                {
                    //MarkSquare(0, 0, true);
                    checkMateMessage.text = "CHECK!";
                }

                else
                {
                    try
                    {
                        checkMateMessage.text = "";
                    }
                    catch (NullReferenceException ex)
                    {
                        Debug.Log("myLight was not set in the inspector");
                    }

                }

                board.GetPossibleMoves(activeCmd);
                ValidateMoves();

                if (board.validMoves.Count == 0)
                {
                    if (board.isCheck == true)
                    {
                        board.isMate = true;
                        checkMateMessage.text = "CheckMate";
                        Rules.ingame = false;
                    }

                    else
                    {
                        board.isPat = true;
                        checkMateMessage.text = "PAT";
                        Rules.ingame = false;
                    }

                }


                //chess = chess.Move(move);

                ShowFigures();
                //MarkValidFigures();

                UnmarkAllSquares();

            }
        }

        //^^^^^^^^^^^^^^^^^^^^^^^^^^^SEND TO SERVER^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        public void RequestMove_Get(string EnemyName)
        {
            StartCoroutine(RequestMove_GetSecond(EnemyName));
        }

        //public bool SendMove_GetDelay(string myName, string move)
        //{
        //    Thread.Sleep(2000);
        //    SendMove_GetSecond(move);
        //    return true;
        //}
        public IEnumerator RequestMove_GetSecond(string EnemyName)
        {

            Debug.Log("METHOD ACTIVE");

            string url = "https://localhost:7098/api/Battle/CurrentMove?name=" + EnemyName;
            string answer = "null";
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            answer = www.downloadHandler.text;

            Debug.Log("ANSWER IS: " + answer);
            ServerAnswers.serverAnswerMove = answer;

            www.Dispose();
        }


        public void SendMove_Get(string myName, string move)
        {
            StartCoroutine(SendMove_GetSecond(myName, move));
        }

        //public bool SendMove_GetDelay(string myName, string move)
        //{
        //    Thread.Sleep(2000);
        //    SendMove_GetSecond(move);
        //    return true;
        //}
        public IEnumerator SendMove_GetSecond(string myName, string move)
        {

            Debug.Log("METHOD ACTIVE");

            string url = "https://localhost:7098/api/Battle/SendMyMove?name=" + myName + "&move=" + move;
            string answer = "null";
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            answer = www.downloadHandler.text;

            Debug.Log("ANSWER IS: " + answer);
            ServerAnswers.serverAnswerMove = answer;

            www.Dispose();
        }

        //---------------------------------------
        //public void SendMove_Get2(string move)
        //{
        //    string url = "https://localhost:7098/api/Battle/CurrentMove?move=" + move;
        //    string answer = "null";
        //    UnityWebRequest www = UnityWebRequest.Get(url);
        //    www.SendWebRequest();           

        //    answer = www.downloadHandler.text;

        //    while (answer != "Accept Move")
        //    {
        //        Thread.Sleep(1000);
        //    }

        //    Debug.Log(answer);

        //    www.Dispose();
        //}



        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

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
                goCell = GameObject.Find("ActiveStyle" + color + "Square");
            var spriteSquare = goSquare.GetComponent<SpriteRenderer>();
            var spriteCell = goCell.GetComponent<SpriteRenderer>();
            spriteSquare.sprite = spriteCell.sprite;
        }
        public void ShowValidMoves()
        {
            board.GetPossibleMoves(activeCmd);
            ValidateMoves();
            //Debug.Log(((dad.pickPosition.x) / 1) + " " + ((dad.pickPosition.y) / 1));
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