using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_GameManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public int activePlayer = 0;
    public string player1Name;
    public string player2Name;
    public string player3Name;
    public string player4Name;

    void Start()
    {
        switch (PlayerPrefs.GetInt("PlayerAmount"))
        {
            case 2:
                Destroy(GameObject.Find("Player 3"));
                Destroy(GameObject.Find("Player 4"));
                break;
            case 3:
                Destroy(GameObject.Find("Player 4"));
                break;
            case 4:

                break;
            default:
                Debug.LogError("Erorr :(");
                break;
        }

        for (int i = 0; i < PlayerPrefs.GetInt("PlayerAmount"); i++)    //populates the List with each player gameobject
        {
            players.Add(GameObject.FindGameObjectsWithTag("Player")[i]);
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<SP_PlayerController>().state = -1;          //idle all players
        }
        players[activePlayer].GetComponent<SP_PlayerController>().state = 0;    //starts turn for first player in List
    }

    public void NextPlayer()
    {
        Debug.Log("Next player called");
        if (activePlayer != PlayerPrefs.GetInt("PlayerAmount")-1)
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
