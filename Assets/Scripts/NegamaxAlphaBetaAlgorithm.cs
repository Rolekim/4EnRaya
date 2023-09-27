using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NegamaxAlphaBetaAlgorithm : IA
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
            score = -NegamaxAB(newBoard, 1, MINUS_INFINITE, INFINITE);

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

    int NegamaxAB(Board board, int depth, int alpha, int beta)
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
            if (turn == 0 || depth == 0)
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
                score = -NegamaxAB(newBoard, depth + 1, -beta, -Math.Max(alpha, bestScore));

                newBoard.boxes = null;
                newBoard = null;

                if (score > bestScore)
                    bestScore = score;
                if(bestScore >= beta)
                {
                    return bestScore;
                }
            }

            return bestScore;
        }
    }
}

