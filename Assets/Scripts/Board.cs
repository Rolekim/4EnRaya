using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board 
{
    public int rows = 0;
    public int columns = 0;
    public int turn;
    public bool end;

    public int[,] boxes;
    
    
    public Board(int rowsValue, int columnsValue)
    {
        rows = rowsValue;
        columns = columnsValue;
        turn = BoardController.boardController.turn;
        boxes = new int[rows, columns];
        InitializeBoxes(boxes);
    }

    public Board(Board b)
    {
        rows = b.rows;
        columns = b.columns;
        boxes = new int[rows, columns];
        CopyBoxes(b);
    }


    private static int[,] evaluationTable = new int[,]
    {
        {3, 4, 5, 7, 5, 4, 3},
        {4, 6, 8, 10, 8, 6, 4},
        {5, 8, 11, 13, 11, 8, 5},
        {5, 8, 11, 13, 11, 8, 5},
        {4, 6, 8, 10, 8, 6, 4},
        {3, 4, 5, 7, 5, 4, 3}
    };

    void InitializeBoxes(int[,] boxes)
    {
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < columns; c++)
            {
                boxes[r, c] = 0;
            }
        }
    }

    void CopyBoxes(Board b)
    {
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < columns; c++)
            {
                boxes[r, c] = b.boxes[r, c];
            }
        }
    }

    public int[] PossibleMoves()
    {
        int[] available = new int[7];
        int counter = 0;

        for (int i = 0; i < columns; ++i)
        {
            if (IsColumnAvailable(i))
            {
                available[counter] = i;
                ++counter;      
            }
        }

        Array.Resize(ref available, counter);

        return available;
    }

    public string PrintBoard()
    {
        string boardPrint = "";

        for (int r = rows - 1; r > -1; --r)
        {
            for (int c = 0; c < columns; ++c)
            {
                boardPrint += boxes[r, c] + " ";
            }
            boardPrint += "\n";
        }

        return boardPrint;
    }
    
    public string PrintEvaluation()
    {
        string boardPrint = "";

        for (int r = rows - 1; r > -1; --r)
        {
            for (int c = 0; c < columns; ++c)
            {
                boardPrint += evaluationTable[r, c] + " ";
            }
            boardPrint += "\n";
        }

        return boardPrint;
    }

    public Board GenerateNewBoardFromMove(int move, int turn)
    {
        var boxes = this.boxes;
        var t = this.turn;
        Board newBoard = new Board(this);

        newBoard.FillBox(turn, FindBottomBox(move), move);

        this.boxes = boxes;
        this.turn = t;

        return newBoard;
    }

    public int CheckWin()
    {
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < columns; c++)
            {
                if (boxes[r, c] == 0)
                {
                    continue;
                }

                int player = boxes[r, c];

                if (c + 3 < columns &&
                boxes[r, c + 1] == player &&
                boxes[r, c + 2] == player &&
                boxes[r, c + 3] == player)
                {
                    return player;
                }
                if (r + 3 < rows)
                {
                    if (boxes[r + 1, c] == player &&
                        boxes[r + 2, c] == player &&
                        boxes[r + 3, c] == player)
                    {
                        return player;
                    }
                    if (c + 3 < columns &&
                       boxes[r + 1, c + 1] == player &&
                       boxes[r + 2, c + 2] == player &&
                       boxes[r + 3, c + 3] == player)
                    {
                        return player;
                    }
                    if (c - 3 >= 0 &&
                      boxes[r + 1, c - 1] == player &&
                      boxes[r + 2, c - 2] == player &&
                      boxes[r + 3, c - 3] == player)
                    {
                        return player;
                    }
                }
            }
        }

        return 0;
    }

    public int FindBottomBox(int column)
    {
        for (int i = 0; i < rows; ++i)
        {
            if (boxes[i, column] == 0)
            {
                return i;
            }
        }

        return 0;
    }

    public bool IsColumnAvailable(int column)
    {
        for (int i = 0; i < rows; ++i)
        {
            if (boxes[i, column] == 0)
            {
                return true;
            }
        }

        return false;
    }

    public int Heuristic(int turn)
    {
        int sum = 0;

        if(CheckWin() == 0)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (boxes[i, j] == 2)
                    {
                        sum += evaluationTable[i, j];
                    }                
                    else if (boxes[i, j] == 1)
                    {
                        sum -= evaluationTable[i, j];
                    }
                }
            }

            if(ThreeOnRow(1) > 0 && turn == 2)
            {
                sum = -8500;
            }
            else if(ThreeOnRow(1) > 0 && turn == 1)
            {
                sum -= ThreeOnRow(1);
            }
            if(ThreeOnRow(2) > 0 && turn == 1)
            {
                sum = 8500;
            }
            else if(ThreeOnRow(2) > 0 && turn == 2) 
            {
                sum += ThreeOnRow(2);
            }

            return sum;
        }
        else
        {
            if (CheckWin() == 2)
                sum = 9000;
            else
            {
                sum = -9000;
            }

            return sum;
        }

    }

    int ThreeOnRow(int player)
    {
        int sum = 0;

        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < columns; c++)
            {
                if (boxes[r, c] != player)
                {
                    continue;
                }

                if (c + 3 < columns &&
                boxes[r, c + 1] == player &&
                boxes[r, c + 2] == player)
                {
                    if(c - 1 >= 0 && boxes[r, c - 1] == 0)
                    {
                        if(r - 1 <= 0)
                        {
                            sum += 100;
                        }
                        else if(boxes[r - 1, c - 1] != 0)
                        {
                            sum += 100;
                        }
                    }
                    else if(boxes[r, c + 3] == 0)
                    {
                        if (r - 1 <= 0)
                        {
                            sum += 100;
                        }
                        else if (boxes[r - 1, c + 3] != 0)
                        {
                            sum += 100;
                        }
                    }
                }

                if(c + 3 < columns &&
                    boxes[r, c + 1] == player &&
                    boxes[r, c + 2] == 0 &&
                    boxes[r, c + 3] == player)
                {
                    if(r - 1 >= 0)
                    {
                        if(boxes[r - 1, c + 2] != 0)
                        {
                            sum += 100;
                        }
                    }
                    else
                    {
                        sum += 100;
                    }
                }

                if (c + 3 < columns &&
                    boxes[r, c + 1] == 0 &&
                    boxes[r, c + 2] == player &&
                    boxes[r, c + 3] == player)
                {
                    if (r - 1 >= 0)
                    {
                        if (boxes[r - 1, c + 1] != 0)
                        {
                            sum += 100;
                        }
                    }
                    else
                    {
                        sum += 100;
                    }
                }

                if (r + 3 < rows)
                {
                    if (boxes[r + 1, c] == player &&
                        boxes[r + 2, c] == player &&
                        boxes[r + 3, c] == 0)
                    {
                        sum += 100;
                    }

                    if (c + 3 < columns)
                    {
                        if (boxes[r + 1, c + 1] == player &&
                           boxes[r + 2, c + 2] == player &&
                           boxes[r + 3, c + 3] == 0 &&
                           boxes[r + 2, c + 3] != 0)
                        {
                            sum += 100;
                        }

                        if (boxes[r + 1, c + 1] == player &&
                            boxes[r + 3, c + 3] == player &&
                            boxes[r + 2, c + 2] == 0 &&
                            boxes[r + 1, c + 2] != 0)
                        {
                            sum += 100;
                        }

                        if (boxes[r + 2, c + 2] == player &&
                            boxes[r + 3, c + 3] == player &&
                            boxes[r + 1, c + 1] == 0 &&
                            boxes[r, c + 1] != 0)
                        {
                            sum += 100;
                        }
                    }
                       

                    if(c - 3 >= 0)
                    {
                        if (boxes[r + 1, c - 1] == player &&
                            boxes[r + 2, c - 2] == player &&
                            boxes[r + 3, c - 3] == 0 &&
                            boxes[r + 2, c - 3] != 0)
                        {
                            sum += 100;
                        }

                        if (boxes[r + 1, c - 1] == player &&
                            boxes[r + 3, c - 3] == player &&
                            boxes[r + 2, c - 2] == 0 &&
                            boxes[r + 1, c - 2] != 0)
                        {
                            sum += 100;
                        }

                        if (boxes[r + 2, c - 2] == player &&
                            boxes[r + 3, c - 3] == player &&
                            boxes[r + 1, c - 1] == 0 &&
                            boxes[r, c - 1] != 0)
                        {
                            sum += 100;
                        }
                    }

                    
                }
            }
        }

        return sum;
    }

    public void FillBox(int value, int row, int column)
    {
        boxes[row, column] = value;
    }
}
