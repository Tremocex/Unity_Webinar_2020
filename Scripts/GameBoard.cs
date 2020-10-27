using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class GameBoard : MonoBehaviour
{

    //Player 1 -'1' ( O ); Player 2 - '2' (X)

    public int[,] board = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    public int winner = 0;
    public int ScorePlayer1=0, ScorePlayer2=0;
    public int currentPlayer=1;
    public int n_pieces = 0;
    public TMP_Text scoreboard1, scoreboard2;



    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    //Initiating Function, where all the game variables are initiated.
    //Board game is cleaned by puting '0' flag in every positions possible.
    void Init()
    {
        //Link the score board on scene (TextViewer) and the variable that scores the score of the player
        scoreboard1.text = ScorePlayer1.ToString(); 
        scoreboard2.text = ScorePlayer2.ToString();

        int i,j,x,y;
        //Initiate BoardGame
        for ( i = 1; i <= 3; i++)
        {

            for (j = 1; j <= 3; j++)
            {
                x = i - 1;
                y = j - 1;

                board[x,y] = 0;
                turnOffGrid(i, j);
            }
        }
        //Hide winner canvas, because we will only display this when we now for sure who is the big winner ;)
        Image wb;
        wb = GameObject.Find("BoardTOP").GetComponent<Image>();
        Image origImage2 = wb;
        Color newColor2 = origImage2.color;
        newColor2.a = 0;
        origImage2.color = newColor2;

        //set global variables to initial values
        n_pieces = 0;
        currentPlayer = 1;
        winner = 0;
    }


    // Update is called once per frame & check if anybody as won already
    void Update()
    {
        //Update it's just like an While(1) Loop running along with the rest of the game
        

        //******************IMPLEMENTATION FOR THE WEBINARS WATCHING*********************************

        //#TIPS:

            //#1st -> CHECK IF ANY PLAYER REACH 5 POINTS IN THE SCORE BOARD , IF SO, STOP GAME AND CONGRAT WINNER
            
            //#2st -> IF NOBODY HAS WON THE GAME, CHECK IF IN THE CURRENT ITERATION ANYONE HAS WON THE CURRENT ROUND WITH HELP OF FLAGS

            //#3st -> IF NO ONE HAS WON THE ROUND, THEN CHECK IF THE CURRENT MOVEMENT OR THE BOARD SCHEME HAS A PATTERN THAT CALLS A WIN:
                                    //VERTICAL
                                    //HORIZONTAL
                                    //DIAGONAL

        
        
        
    }

    //Scene Manager to allow the application to go throw the different scenes of the game
    //In this case between Scene Menu and Scene GameBoard
    //receives the index of the scene where we want to go
    public void onLoadScene(int index)
    {

        SceneManager.LoadScene(index);
    }

    //Hides all the game pieces in the board game (deals with the final of each round )
    public void turnOffGrid(int i, int j)
    {
        Button aux;
        
        String strf = "obj" + i.ToString() + j.ToString();
        aux = GameObject.Find(strf).GetComponent<Button>();

        Image origImage = aux.GetComponent<Image>();
        Color newColor = origImage.color;
        newColor.a = 0;
        origImage.color = newColor;

    }
    //It's the opposite of the turnGrid, but is specifically for just 1 piece at time.
    public void turnOnPiece(int i, int j)
    {
        Button aux;
        String strf = "obj" + i.ToString() + j.ToString();
        aux = GameObject.Find(strf).GetComponent<Button>();

        Image origImage = aux.GetComponent<Image>();
        Color newColor = origImage.color;
        newColor.a = 1;
        origImage.color = newColor;

    }

    //Handles de position and which type of piece to put in place, based on the current player playing. Receives the indices of the piece in the 3*3 board
    //(i,j) and the both loaded sprites that will represent graphically the type of piece.
    public void putPiece(Sprite sprite1,Sprite sprite2,int i, int j)
    {

        Button aux_btn;
        int x = i + 1;
        int y = j + 1;

        String object_name = "obj" + x.ToString() + y.ToString();


        if (board[i,j] == 0)
        {
            n_pieces++;
            aux_btn = GameObject.Find(object_name).GetComponent<Button>();

            if (currentPlayer == 1)
            {
                aux_btn.GetComponent<Image>().sprite = sprite1;
                turnOnPiece(x, y);
                board[i, j] = 1;
                currentPlayer = 2;

            }
            else
            {

                aux_btn.GetComponent<Image>().sprite = sprite2;
                turnOnPiece(x, y);
                board[i, j] = 2;
                currentPlayer = 1;

            }

        }



    }

    //Handler of the Positions clicked in the game -> When a particular position is clicked by a player, whatever is the position in the
    //switch, the function "putPiece" will deal with the needs to pop up the piece of the player in the board game
    public void OnPieceClicked(int numPiece)
    {

        Button aux_btn;
        Sprite sprite1 = Resources.Load<Sprite>("ovo_material");
        Sprite sprite2 = Resources.Load<Sprite>("ovo_estr_material");
    
            switch (numPiece)
            {

                //0-8 (9) as peças do tabuleiro a poderem ser primidas

                case 0:
                    putPiece(sprite1, sprite2, 0, 0);
                    break;

                case 1:
                    putPiece(sprite1, sprite2, 0, 1);
                    break;

                case 2:
                    putPiece(sprite1, sprite2, 0, 2);
                    break;

                case 3:
                    putPiece(sprite1, sprite2, 1, 0);
                    break;

                case 4:
                    putPiece(sprite1, sprite2, 1, 1);
                    break;

                case 5:
                    putPiece(sprite1, sprite2, 1, 2);
                    break;

                case 6:
                    putPiece(sprite1, sprite2, 2, 0);
                    break;

                case 7:
                    putPiece(sprite1, sprite2, 2, 1);
                    break;

                case 8:
                    putPiece(sprite1, sprite2, 2, 2);
                    break;

                default:
                    break;

            }
    }

    // Hides and disable all the possible holes of the game when one player reach the score of 5

    public void disableFcn()
    {
        int i, j,x,y;

        for (i = 1; i <= 3; i++)
        {

            for (j = 1; j <= 3; j++)
            {
                
                disableBtn(i, j);

            }
        }
    }
    // Turns Alpha propertie to 0, basically to simulate the buttons disappearing and the Game is over.
    public void disableBtn(int i, int j)
    {
        Button aux;

        String strf = "obj" + i.ToString() + j.ToString();
        aux = GameObject.Find(strf).GetComponent<Button>();

        aux.enabled = false;
        Image origImage = aux.GetComponent<Image>();
        Color newColor = origImage.color;
        newColor.a = 0;
        origImage.color = newColor;



    }
}

