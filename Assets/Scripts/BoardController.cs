using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    Board board;
    BoxGO[,] boardVisual;
    public static BoardController boardController;

    #region Draw board atributes
    [SerializeField]
    Transform StartPosition = null;
    [SerializeField]
    float xOffset = 0;
    [SerializeField]
    float yOffset = 0;
    [SerializeField]
    GameObject box = null;
    [SerializeField]
    int rows = 0;
    [SerializeField]
    int columns = 0;
    #endregion

    public int turn = 1;


    void Awake()
    {
        boardController = this;

        board = new Board(rows, columns);
        boardVisual = new BoxGO[rows, columns];


        Draw();
    }

    public void ReturnHeuristic()
    {
        Debug.Log("Heuristic: " + board.Heuristic(board.turn));
    }


    private void Draw()
    {
        Vector3 vec;

        for (int i = 0; i < rows; ++i)
        {
            for(int j = 0; j < columns; ++j)
            {
                vec = new Vector3(StartPosition.position.x  + xOffset * j + j * box.transform.localScale.x,
                StartPosition.position.y + yOffset * i + i * box.transform.localScale.y, StartPosition.position.z);

                boardVisual[i, j] = Instantiate(box, vec, Quaternion.identity).GetComponentInChildren<BoxGO>();
                if (!boardVisual[i, j])
                boardVisual[i, j].transform.parent.transform.parent = gameObject.transform;
            }
        }
    }

    public void ChangeTurn()
    {
        turn = 2;

        board.turn = turn;

        IAturn();
    }

    void IAturn()
    {
        int move = GameManager.gameManager.ReturnMove(board);
        board.FillBox(turn, board.FindBottomBox(move), move);
        turn = 1;
    }

    public void ClickColumn(int column)
    {
        if(board.IsColumnAvailable(column) && board.CheckWin() == 0)
        { 
            board.FillBox(turn, board.FindBottomBox(column), column);
            RefreshVisual();

            if (board.CheckWin() == 0)
            {
                ChangeTurn();
                RefreshVisual();
                Debug.Log(board.Heuristic(board.turn));
            }
            else
            {
                Debug.Log(board.Heuristic(board.turn));
            }
            //if (turn == 1)
            //    turn = 2;
            //else
            //    turn = 1;
        }
    }

    public void RefreshVisual()
    {
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; ++j)
            {
                if(board.boxes[i, j] != 0)
                {
                    boardVisual[i, j].FillBox(board.boxes[i, j]);
                }
            }
        }
    }

}
