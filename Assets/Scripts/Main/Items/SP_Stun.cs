using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Stun : MonoBehaviour
{
    public GameObject[] players = new GameObject[4];
    public GameObject stunPrompt;
    public void StunPlayer(int player)
    {
        Debug.Log("Attempting to stun Player " + player);
        players[player].GetComponent<SP_PlayerController>().isStunned = true;
        stunPrompt.SetActive(false);
    }
}
