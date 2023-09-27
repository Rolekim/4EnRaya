using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author: Miguel García

/*  Description
    Clase que recoje los datos de cada casilla como su numero en el tablero,
    si está ocupada, su material, etc.  
*/

public class BoxGO : MonoBehaviour
{
    #region Materials
    //[SerializeField]
    //Material selectedMaterial = null;
    [SerializeField]
    MeshRenderer currentMaterial = null;
    [SerializeField]
    Material player1Material = null;
    [SerializeField]
    Material player2Material = null;
    /*[SerializeField]
    Material initialMaterial = null;*/
    #endregion

    #region Atributes
    bool filled = false;
    [SerializeField]
    int row = 0, column = 0;
    [SerializeField]
    int player = 0;
    #endregion

    public void InitiateRowsColumns(int r, int c)
    {
        row = r;
        column = c;
    }

    public void FillBox(int playerTurn)
    {
        if (playerTurn == 1)
        {
            currentMaterial.material = player1Material;
            player = playerTurn;
        }
        else
        {
            currentMaterial.material = player2Material;
            player = playerTurn;
        }

        filled = true;
    }


}
