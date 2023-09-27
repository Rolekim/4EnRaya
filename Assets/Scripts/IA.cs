using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IA : MonoBehaviour
{
    protected int MINUS_INFINITE = -10000;
    protected int INFINITE = 10000;
    [SerializeField]
    protected int MAX_DEPTH = 5;

    public virtual int ScoringMove(Board board)
    {
        return 1;
    }

}

