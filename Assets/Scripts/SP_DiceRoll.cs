using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_DiceRoll : MonoBehaviour
{
    public int RollDice()
    {
        int x = Random.Range(1, 7);
        return x;
    }
}