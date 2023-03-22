using System;
using System.Collections;
using System.Collections.Generic;
using ChessCore;
using UnityEngine;

public class Style : MonoBehaviour
{
    string style = "b&w";

    public void NextStyle()
    {
        style = style == "wood" ? "b&w" : "wood";
    }
    public void ChangeStyle()
    {
        NextStyle();

        GameObject ActiveBoardFull;
        SpriteRenderer spriteActiveBoardFull;
        GameObject BoardFull;
        SpriteRenderer spriteBoardFull;

        GameObject ActiveBlackSq;
        SpriteRenderer spriteActiveBlackSq;
        GameObject ActiveWhiteSq;
        SpriteRenderer spriteActiveWhiteSq;

        GameObject styleBlackSquare;
        SpriteRenderer spriteBlackStyle;
        GameObject styleWhiteSquare;
        SpriteRenderer spriteWhiteStyle;

        switch (style)
        {
            case "wood":
                //BoardFull

                ActiveBoardFull = GameObject.Find("ActiveBoardFull");
                spriteActiveBoardFull = ActiveBoardFull.GetComponent<SpriteRenderer>();
                BoardFull = GameObject.Find("BoardFull_w");
                spriteBoardFull = BoardFull.GetComponent<SpriteRenderer>();
                spriteActiveBoardFull.sprite = spriteBoardFull.sprite;

                //Squares
                ActiveBlackSq = GameObject.Find("ActiveStyleBlackSquare");
                spriteActiveBlackSq = ActiveBlackSq.GetComponent<SpriteRenderer>();
                ActiveWhiteSq = GameObject.Find("ActiveStyleWhiteSquare");
                spriteActiveWhiteSq = ActiveWhiteSq.GetComponent<SpriteRenderer>();

                styleBlackSquare = GameObject.Find("BlackSquare_w");
                spriteBlackStyle = styleBlackSquare.GetComponent<SpriteRenderer>();
                styleWhiteSquare = GameObject.Find("WhiteSquare_w");
                spriteWhiteStyle = styleWhiteSquare.GetComponent<SpriteRenderer>();

                spriteActiveBlackSq.sprite = spriteBlackStyle.sprite;
                spriteActiveWhiteSq.sprite = spriteWhiteStyle.sprite;
                break;

            case "b&w":
                //BoardFull

                ActiveBoardFull = GameObject.Find("ActiveBoardFull");
                spriteActiveBoardFull = ActiveBoardFull.GetComponent<SpriteRenderer>();
                BoardFull = GameObject.Find("BoardFull");
                spriteBoardFull = BoardFull.GetComponent<SpriteRenderer>();
                spriteActiveBoardFull.sprite = spriteBoardFull.sprite;

                //Squares
                ActiveBlackSq = GameObject.Find("ActiveStyleBlackSquare");
                spriteActiveBlackSq = ActiveBlackSq.GetComponent<SpriteRenderer>();
                ActiveWhiteSq = GameObject.Find("ActiveStyleWhiteSquare");
                spriteActiveWhiteSq = ActiveWhiteSq.GetComponent<SpriteRenderer>();

                styleBlackSquare = GameObject.Find("BlackSquare");
                spriteBlackStyle = styleBlackSquare.GetComponent<SpriteRenderer>();
                styleWhiteSquare = GameObject.Find("WhiteSquare");
                spriteWhiteStyle = styleWhiteSquare.GetComponent<SpriteRenderer>();

                spriteActiveBlackSq.sprite = spriteBlackStyle.sprite;
                spriteActiveWhiteSq.sprite = spriteWhiteStyle.sprite;
                break;

            default:
                styleBlackSquare = GameObject.Find("BlackSquare");
                spriteBlackStyle = styleBlackSquare.GetComponent<SpriteRenderer>();
                styleWhiteSquare = GameObject.Find("BlackSquare_w");
                spriteWhiteStyle = styleBlackSquare.GetComponent<SpriteRenderer>();
                break;
        }

        bool sqCol = false;
        
        for (int y = 0; y < 8; y++)
        {
            sqCol = (sqCol == true) ? false : true;
            for (int x = 0; x < 7; x++)
            {
                if (sqCol)
                {
                    GameObject sq00 = GameObject.Find("" + y + x);
                    SpriteRenderer sq00Style = sq00.GetComponent<SpriteRenderer>();
                    sq00Style.sprite = spriteBlackStyle.sprite;
                    GameObject sq01 = GameObject.Find("" + y + (x + 1));
                    SpriteRenderer sq01Style = sq01.GetComponent<SpriteRenderer>();
                    sq01Style.sprite = spriteWhiteStyle.sprite;
                    x++;
                }
                else
                {
                    GameObject sq00 = GameObject.Find("" + y + x);
                    SpriteRenderer sq00Style = sq00.GetComponent<SpriteRenderer>();
                    sq00Style.sprite = spriteWhiteStyle.sprite;
                    GameObject sq01 = GameObject.Find("" + y + (x + 1));
                    SpriteRenderer sq01Style = sq01.GetComponent<SpriteRenderer>();
                    sq01Style.sprite = spriteBlackStyle.sprite;
                    x++;
                }
                
            }
        }
    }
}
