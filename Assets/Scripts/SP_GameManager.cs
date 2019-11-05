using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_GameManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public int activePlayer = 0;

    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)    //populates the List with each player gameobject
        {
            players.Add(GameObject.FindGameObjectsWithTag("Player")[i]);
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<SP_PlayerController>().state = -1;
        }
        players[activePlayer].GetComponent<SP_PlayerController>().state = 0;    //starts turn for first player in List
    }

    public void NextPlayer()
    {
        Debug.Log("Next player called");
        if (activePlayer != 3)
        {
            activePlayer++;
        }
        else
        {
            activePlayer = 0;
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<SP_PlayerController>().state = -1;
        }
        players[activePlayer].GetComponent<SP_PlayerController>().state = 0;
    }
}
