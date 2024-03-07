using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Tic : MonoBehaviour
{
    public char[,] board_ = { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
    public GameObject[] thing = new GameObject[9];
    [SerializeField]
    public TextMeshProUGUI[,] thingie = new TextMeshProUGUI[3, 3];
    public TextMeshProUGUI wintext;

    // Start is called before the first frame update
    void Start()
    {
        wintext.text = "Go play Tic tac toe and explasdofe";
        for (int i = 0; i < 9; i++)
        {
            thingie[(int)(i / 3), i % 3] = thing[i].GetComponent<TextMeshProUGUI>();
            thing[i].GetComponent<TextMeshProUGUI>().text = " ";
        }
        /*for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                thing[i*3+j].GetComponent<Button>().onClick.AddListener(() => PressButton(i, j));
                Debug.Log(i*3+j);
            }
        }*/
        Debug.Log("Yes");
    }

    // Update is called once per frame
    void Update()
    {
        //if (!CheckWinner(board_, 'O') && !CheckWinner(board_, 'X') && !CheckDraw(board_))
        if (CheckWinner(board_, 'O')) 
        {
            wintext.text = "AI wins, duh. Imagine losing to an AI";
        }
        if (CheckWinner(board_, 'X'))
        {
            wintext.text = "You actully won???!@?!?!";
        }
        if (CheckDraw(board_))
        {
            wintext.text = "L, it's a tie";
        }
    }

    public void restart() 
    {
        wintext.text = "Imagine restarting. AI judges you";
        for (int i = 0; i < 9; i++)
        {
            thing[i].GetComponent<TextMeshProUGUI>().text = " ";
           
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board_[i,j] = ' ';
            }
        }
    }
    public void button1()
    {
        PressButton(0, 0);
    }
    public void button2()
    {
        PressButton(0, 1);
    }
    public void button3()
    {
        PressButton(0, 2);
    }
    public void button4()
    {
        PressButton(1, 0);
    }
    public void button5()
    {
        PressButton(1, 1);
    }
    public void button6()
    {
        PressButton(1, 2);
    }
    public void button7()
    {
        PressButton(2, 0);
    }
    public void button8()
    {
        PressButton(2, 1);
    }
    public void button9()
    {
        PressButton(2, 2);
    }
    public void PressButton(int row, int col)
    {
        if (board_[row, col] == ' ' &&  !CheckWinner(board_, 'O') && !CheckWinner(board_, 'X') && !CheckDraw(board_))
        {
            board_[row, col] = 'X';
            thing[row * 3 + col].GetComponent<TextMeshProUGUI>().text = "X";
            if (!CheckWinner(board_, 'O') && !CheckWinner(board_, 'X') && !CheckDraw(board_))
            {
                MakeBestMove(board_);
            }
        }

    }
    public void MakeBestMove(char[,] board)
    {
        int bestScore = -999;
        int[] bestMove = null;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    board[i, j] = 'O';
                    int score = Minimax(board, 0, false, -999, 999);
                    board[i, j] = ' ';
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = new int[] { i, j };
                    }
                }
            }
        }

        board[bestMove[0], bestMove[1]] = 'O';
        thing[bestMove[0] * 3 + bestMove[1]].GetComponent<TextMeshProUGUI>().text = "O";
        int rand = UnityEngine.Random.Range(0,4);
        if (rand == 0) 
        {
            wintext.text = "AI asks you to do better";
        }
        if (rand == 1)
        {
            wintext.text = "AI thinks you should retire";
        }
        if (rand == 2)
        {
            wintext.text = "AI already knows your demise";
        }
        if (rand == 3)
        {
            wintext.text = "AIs are Among Us";
        }
    }
    public int Minimax(char[,] board, int depth, bool isMaximizing, int alpha, int beta)
    {
        if (CheckWinner(board, 'O'))
        {
            return 1;
        }
        else if (CheckWinner(board, 'X'))
        {
            return -1;
        }
        else if (CheckDraw(board))
        {
            return 0;
        }

        if (isMaximizing)
        {
            int bestScore = -999;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = 'O';
                        int score = Minimax(board, depth + 1, false,alpha,beta);
                        board[i, j] = ' ';
                        bestScore = Math.Max(score, bestScore);
                        alpha = Mathf.Max(alpha, bestScore);
                        if (beta <= alpha)
                          break;
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = 999;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = 'X';
                        int score = Minimax(board, depth + 1, true, alpha, beta);
                        board[i, j] = ' ';
                        bestScore = Math.Min(score, bestScore);
                        beta = Mathf.Min(beta,bestScore);
                        if (beta <= alpha)
                            break;
                    }
                }
            }
            return bestScore;
        }
    }
    public bool CheckWinner(char[,] board, char player)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
            {
                return true;
            }
            if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
            {
                return true;
            }
        }
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
        {
            return true;
        }
        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
        {
            return true;
        }
        return false;
    }

    public bool CheckDraw(char[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    return false;
                }
            }
        }
        return true;
    }
}
