using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SP_DiceRoll : MonoBehaviour
{
    public TextMeshProUGUI diceText;
    public int RollDice()
    {
        int x = Random.Range(1, 7);
        diceText.text = "Last Roll: " + x.ToString();
        return x;
    }
    public int RollTripleDice()
    {
        int x = Random.Range(3, 19);
        diceText.text = "Last Roll: " + x.ToString();
        return x;
    }
    public int RollRiggedDice(int choice)
    {
        int x = choice;
        diceText.text = "Last Roll: " + x.ToString();
        return x;
    }
}