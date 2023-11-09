using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeSquare : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite circle;
    [SerializeField] private Sprite x;
    [SerializeField] private SqaureGrid sq;

    public static int clickCtr = 0;

    // Assume you have a char array to represent the Tic-Tac-Toe board
    public static char[] board = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    public static bool isPlayerTurn = true;



    int FindBestMove(char[] board)
    {
        int bestVal = int.MinValue;
        int bestMove = -1;

        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == ' ')
            {
                board[i] = 'O';
                int moveVal = MiniMax(board, 0);
                board[i] = ' '; // Undo the move

                if (moveVal > bestVal)
                {
                    bestMove = i;
                    bestVal = moveVal;
                }
            }
        }

        return bestMove;
    }

    int MiniMax(char[] board, int depth)
    {
        int score = Evaluate(board);

        // If the game is over, return the score
        if (score == 1 || score == -1 || score == 0)
            return score;

        // If it's the AI's move
        int best = int.MinValue;

        // Loop through all empty cells
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == ' ')
            {
                board[i] = 'O';
                best = Mathf.Max(best, MiniMax(board, depth + 1));
                board[i] = ' '; // Undo the move
            }
        }

        return best;
    }

    int Evaluate(char[] board)
    {
        // Check for a win
        int winScore = CheckWin(board);
        if (winScore != 0)
        {
            return winScore; // +1 for AI win, -1 for player win
        }

        // Check for a draw
        if (IsBoardFull(board))
        {
            return 0; // 0 for a draw
        }

        // The game is still going
        return -1;
    }

    int CheckWin(char[] board)
    {
        // Check rows, columns, and diagonals for a win
        for (int i = 0; i < 3; i++)
        {
            // Rows
            if (board[i * 3] == board[i * 3 + 1] && board[i * 3 + 1] == board[i * 3 + 2] && board[i * 3] != ' ')
            {
                return (board[i * 3] == 'O') ? 1 : -1; // +1 for AI win, -1 for player win
            }

            // Columns
            if (board[i] == board[i + 3] && board[i + 3] == board[i + 6] && board[i] != ' ')
            {
                return (board[i] == 'O') ? 1 : -1;
            }
        }

        // Diagonals
        if (board[0] == board[4] && board[4] == board[8] && board[0] != ' ')
        {
            return (board[0] == 'O') ? 1 : -1;
        }

        if (board[2] == board[4] && board[4] == board[6] && board[2] != ' ')
        {
            return (board[2] == 'O') ? 1 : -1;
        }

        // No winner yet
        return 0;
    }

    bool IsBoardFull(char[] board)
    {
        // Check if the board is full (indicating a draw)
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == ' ')
            {
                return false; // There's an empty space, so the board is not full
            }
        }

        return true; // The board is full
    }


    public void SquareClick()
    {
        if (isPlayerTurn && image.sprite != circle && image.sprite != x)
        {
            UpdateBoard(image);
            image.sprite = circle;

            bool isPlayerWin = CheckIfWon(circle);

            if (isPlayerWin)
            {
                Debug.Log("Player wins!");
                // Handle end game logic
            }

            // Switch to AI's turn
            isPlayerTurn = false;

            // Make AI move
            int aiMove = FindBestMove(board);
            if (aiMove != -1)
            {
                board[aiMove] = 'O';
                ProcessComputerAction(aiMove);
            }      

            bool isComputerWin = CheckIfWon(x);
            
            if (isComputerWin)
            {
                Debug.Log("Computer Wins!");
            }            

            if (IsBoardFull(board) && !isPlayerWin && !isComputerWin)
            {
                Debug.Log("Draw!");
            }
            else
            {
                isPlayerTurn = true;
            }
        }     
    }

    private void UpdateBoard(Image image)
    {
        switch (Array.IndexOf(sq.row1, image))
        {
            case 0:
                board[0] = '1';
                break;
            case 1:
                board[1] = '1';
                break;
            case 2:
                board[2] = '1';
                break;
        }

        switch (Array.IndexOf(sq.row2, image))
        {
            case 0:
                board[3] = '1';
                break;
            case 1:
                board[4] = '1';
                break;
            case 2:
                board[5] = '1';
                break;
        }

        switch (Array.IndexOf(sq.row3, image))
        {
            case 0:
                board[6] = '1';
                break;
            case 1:
                board[7] = '1';
                break;
            case 2:
                board[8] = '1';
                break;
        }
    }

    private void ComputerSelect(Image[] squares, int index)
    {
        squares[index].sprite = x;
    }

    private void ProcessComputerAction(int index)
    {
        switch (index)
        {
            case 0:
                ComputerSelect(sq.row1, 0);
                break;
            case 1:
                ComputerSelect(sq.row1, 1);
                break;
            case 2:
                ComputerSelect(sq.row1, 2);
                break;
            case 3:
                ComputerSelect(sq.row2, 0);
                break;
            case 4:
                ComputerSelect(sq.row2, 1);
                break;
            case 5:
                ComputerSelect(sq.row2, 2);
                break;
            case 6:
                ComputerSelect(sq.row3, 0);
                break;
            case 7:
                ComputerSelect(sq.row3, 1);
                break;
            case 8:
                ComputerSelect(sq.row3, 2);
                break;

        }
    }

    private bool CheckIfWon(Sprite compareSprite)
    {
        bool wonRow1 = true;
        bool wonRow2 = true;
        bool wonRow3 = true;
        bool wonColumn1 = true;
        bool wonColumn2 = true;
        bool wonColumn3 = true;
        bool wonDiagnol1 = true;
        bool wonDiagnol2 = true;
        List<bool> winChecks = new List<bool> { wonRow1, wonRow2, wonRow3, wonColumn1, wonColumn2, wonColumn3, wonDiagnol1, wonDiagnol2 };

        foreach (Image item in sq.row1)
        {
            if (item.sprite != compareSprite)
            {
                winChecks[winChecks.IndexOf(wonRow1)] = false;
                break;
            }
        }

        foreach (Image item in sq.row2)
        {
            if (item.sprite != compareSprite)
            {
                winChecks[winChecks.IndexOf(wonRow2)] = false;
                break;
            }
        }

        foreach (Image item in sq.row3)
        {
            if (item.sprite != compareSprite)
            {
                winChecks[winChecks.IndexOf(wonRow3)] = false;
                break;
            }
        }

        foreach (Image item in sq.column1)
        {
            if (item.sprite != compareSprite)
            {
                winChecks[winChecks.IndexOf(wonColumn1)] = false;
                break;
            }
        }

        foreach (Image item in sq.column2)
        {
            if (item.sprite != compareSprite)
            {
                winChecks[winChecks.IndexOf(wonColumn2)] = false;
                break;
            }
        }

        foreach (Image item in sq.column3)
        {
            if (item.sprite != compareSprite)
            {
                winChecks[winChecks.IndexOf(wonColumn3)] = false;
                break;
            }
        }

        foreach (Image item in sq.diagnol1)
        {
            if (item.sprite != compareSprite)
            {
                winChecks[winChecks.IndexOf(wonDiagnol1)] = false;
                break;
            }
        }

        foreach (Image item in sq.diagnol2)
        {
            if (item.sprite != compareSprite)
            {
                winChecks[winChecks.IndexOf(wonDiagnol2)] = false;
                break;
            }
        }

        foreach (bool winCheck in winChecks)
        {
            if (winCheck)
            {
                return true;
            }
        }

        return false;
    }
}
