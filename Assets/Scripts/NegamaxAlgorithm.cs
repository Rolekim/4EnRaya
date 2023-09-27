using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegamaxAlgorithm : IA
{
    public override int ScoringMove(Board board)
    {
        int bestMove = 3;
        int score;
        int bestScore = MINUS_INFINITE;

        Board newBoard;

        foreach (int move in board.PossibleMoves())
        {
            newBoard = board.GenerateNewBoardFromMove(move, board.turn);
            //Debug.Log(newBoard.PrintBoard());
            score = -Negamax(newBoard, 1);

            newBoard = null;
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        Debug.Log("BestScore: " + bestScore);
        return bestMove;
    }

    int Negamax(Board board, int depth)
    {
        int bestScore = 0;
        int score = 0;

        Board newBoard = null;
        int[] available = board.PossibleMoves();

        if (board.CheckWin() != 0 || depth == MAX_DEPTH)
        {
            if (depth % 2 == 0)
            {
                return board.Heuristic(2);
            }
            else
            {
                return -board.Heuristic(1);
            }
        }
        else
        {
            bestScore = MINUS_INFINITE;

            int turn = depth % 2;
            if(turn == 0 || depth == 0)
            {
                turn = 2;
            }
            else
            {
                turn = 1;
            }

            foreach (int move in available)
            {
                newBoard = board.GenerateNewBoardFromMove(move, turn);
                score = -Negamax(newBoard, depth + 1);

                newBoard.boxes = null;
                newBoard = null;

                if (score > bestScore)
                    bestScore = score;
            }

            return bestScore;
        }
    }
}

