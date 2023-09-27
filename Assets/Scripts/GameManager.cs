using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AI
{
    Negamax,
    NegamaxAB,
    Negascout,
    Aspiracional,
    MTD
}

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int laps = 0;

    [SerializeField]
    IA aiSelected;
    [SerializeField]
    IA[] ais = null;


    public bool begin = false;

    private void Start()
    {
        gameManager = this;
        aiSelected = ais[0];
    }

    public void Play()
    {
        begin = true;
    }

    public int ReturnMove(Board board)
    {
        return aiSelected.ScoringMove(board);
    }

    public void ChangeAISelectedToNegamax()
    {

    }
}
