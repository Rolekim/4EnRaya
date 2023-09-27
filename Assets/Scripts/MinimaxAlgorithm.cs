using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimaxAlgorithm : IA
{
    public override int ScoringMove(Board board)
    {
        int bestMove = 3;
        int score;
        int bestScore = MINUS_INFINITE;

        Board newBoard;

        foreach(int move in board.PossibleMoves())
        {
            newBoard = board.GenerateNewBoardFromMove(move, board.turn);
            //Debug.Log(newBoard.PrintBoard());
            score = Minimax(newBoard, 0, false);

            //newBoard = null;
            if(score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
            //Debug.Log("Score: " + score + "\nMove: " + move);
            
        }
        Debug.Log("BestScore: " + bestScore);
        return bestMove;
    }

    int Minimax(Board board, int depth, bool isMaximizing)
    {
        int bestScore = 0;
        int score = 0;

        Board newBoard = null;
        int[] available = board.PossibleMoves();

        if(board.CheckWin() != 0 || depth == MAX_DEPTH)
        {
            //Debug.Log(board.Heuristic());
            //Debug.Log("Depth: " + depth);
            if(isMaximizing)
            {
                return board.Heuristic(2);
            }
            else
            {
                return board.Heuristic(1);
            }


        }

        if(isMaximizing)
        {
            bestScore = MINUS_INFINITE;

            foreach(int move in available)
            {
                newBoard = board.GenerateNewBoardFromMove(move, 2);
                score = Minimax(newBoard, depth + 1, false);
                newBoard.boxes = null;
                newBoard = null;

                if (score > bestScore)
                    bestScore = score;
            }

            return bestScore;
        }
        else
        {
            bestScore = INFINITE;

            foreach (int move in available)
            {
                newBoard = board.GenerateNewBoardFromMove(move, 1);
                score = Minimax(newBoard, depth + 1, true);
                newBoard.boxes = null;
                newBoard = null;

                if (score < bestScore)
                    bestScore = score;
            }

            available = null;
            return bestScore;
        }
    }
}
