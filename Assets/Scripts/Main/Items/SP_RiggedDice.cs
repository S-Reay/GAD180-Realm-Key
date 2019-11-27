using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_RiggedDice : MonoBehaviour
{
    public GameObject gameManager;

    public void RollRiggedDice(int choice)
    {
        for (int i = 0; i < gameManager.GetComponent<SP_GameManager>().players.Count; i++)
        {
            if (gameManager.GetComponent<SP_GameManager>().players[i].GetComponent<SP_PlayerController>().state != -1) //if the player is not innactive (hence is the active player)
            {
                gameManager.GetComponent<SP_GameManager>().players[i].GetComponent<SP_PlayerController>().UseRiggedDice(choice);
            }
        }
    }
}
