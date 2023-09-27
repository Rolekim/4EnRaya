using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AspirationalSearchAlgorithm : IA
{
    public int previousScore = 0;
    public int moveSelected;
    public int counter = 0;
    int windowRange = 0;

    public override int ScoringMove(Board board)
    {
        moveSelected = 3;
        int alpha, beta;
        int bestMove = 3;
        int score;
        int bestScore = MINUS_INFINITE;


        if (previousScore != 0)
        {
            alpha = previousScore - windowRange;
            beta = previousScore + windowRange;

            while (true)
            {
                score = -NegamaxAB(board, 0, alpha, beta);
                if (score <= alpha) alpha = MINUS_INFINITE;
                else if (score >= beta) beta = INFINITE;
                else
                {
                    break;
                }
            }
        }
        else
        {
            score = -NegamaxAB(board, 0, MINUS_INFINITE, INFINITE);
            previousScore = score;
        }

        return moveSelected;

        //Board newBoard;

        //if(previousScore != 0)
        //{
        //    alpha = previousScore - windowRange;
        //    beta = previousScore + windowRange;

        //    while(true)
        //    {
        //        foreach (int move in board.PossibleMoves())
        //        {
        //            newBoard = board.GenerateNewBoardFromMove(move, board.turn);

        //            score = NegamaxAB(newBoard, 1, MINUS_INFINITE, INFINITE);

        //            newBoard = null;
        //            if (score > bestScore)
        //            {
        //                bestScore = score;
        //                bestMove = move;
        //            }
        //        }
        //    }      
        //}


        //Debug.Log("BestScore: " + bestScore);
        //return bestMove;
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
                {
                    bestScore = score;
                    if(depth == 0)
                    moveSelected = move;
                }
                if (bestScore >= beta)
                {
                    if (depth == 0)
                    moveSelected = move;
                    return bestScore;
                }
            }


            return bestScore;
        }
    }
}

