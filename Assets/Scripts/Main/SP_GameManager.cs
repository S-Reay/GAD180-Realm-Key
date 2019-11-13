using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SP_GameManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public int activePlayer = 0;
    public Text activePlayerUI;

    public GameObject P1UI;
    public GameObject P2UI;
    public GameObject P3UI;
    public GameObject P4UI;

    public int keySpawnCountdown;

    void Start()
    {
        switch (PlayerPrefs.GetInt("PlayerAmount"))
        {
            case 2:
                Destroy(GameObject.Find("Player 3"));
                Destroy(GameObject.Find("Player 4"));
                P3UI.SetActive(false);
                P4UI.SetActive(false);
                break;
            case 3:
                Destroy(GameObject.Find("Player 4"));
                P4UI.SetActive(false);
                break;
            case 4:

                break;
            default:
                Debug.LogError("PlayerPrefs:PlayerAmount Is not within expected range");
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

        keySpawnCountdown = 1;                                                  //all players get 1 move before the first key spawns


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
            if (keySpawnCountdown > 1)
            {
                keySpawnCountdown--;
            }
            else
            {
                //Spawn Key
                gameObject.GetComponent<SP_KeySpawn>().SpawnKey();
                keySpawnCountdown = Random.Range(4, 6);
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<SP_PlayerController>().state = -1;              //Puts all players into the Innactive State
        }
        players[activePlayer].GetComponent<SP_PlayerController>().state = 0;        //Puts then active player into the Idle State
    }

    void DisplayActivePlayerName()
    {
        switch (activePlayer)
        {
            case 0:
                activePlayerUI.text = PlayerPrefs.GetString("P1Name") + "'s Turn";
                break;
            case 1:
                activePlayerUI.text = PlayerPrefs.GetString("P2Name") + "'s Turn";
                break;
            case 2:
                activePlayerUI.text = PlayerPrefs.GetString("P3Name") + "'s Turn";
                break;
            case 3:
                activePlayerUI.text = PlayerPrefs.GetString("P4Name") + "'s Turn";
                break;
            default:
                break;
        }
    }

    void Update()
    {
        DisplayActivePlayerName();
    }

    public void UpdatePlayerUI(int player, int keys)
    {
        switch (player)
        {
            case 1:
                P1UI.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Player 1\nKeys: " + keys;
                break;
            case 2:
                P2UI.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Player 2\nKeys: " + keys;
                break;
            case 3:
                P3UI.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Player 3\nKeys: " + keys;
                break;
            case 4:
                P4UI.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Player 4\nKeys: " + keys;
                break;
            default:
                break;
        }
    }
}
