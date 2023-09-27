using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author: Miguel García

/*  Description
    Clase que recoje los datos de cada casilla como su numero en el tablero,
    si está ocupada, su material, etc.  
*/

public class Box 
{

    #region Atributes
    bool filled = false;
    [SerializeField]
    int row = 0, column = 0;
    [SerializeField]
    int player = 0;
    #endregion

    public Box(int row, int column)
    {
        this.row = row;
        this.column = column;
        player = 0;
        filled = false;
    }

    public Box(Box box)
    {
        this.row = box.row;
        this.column = box.column;
        this.player = box.player;
        this.filled = box.filled;
    }

    public void FillBox(int value)
    {
        filled = true;
        player = value;
    }

    public int ReturnPlayer()
    {
        return player;
    }

    public bool ReturnFilled()
    {
        return filled;
    }

}
