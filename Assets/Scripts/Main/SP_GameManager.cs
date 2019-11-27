using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SP_GameManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public int activePlayer = 0;
    public TextMeshProUGUI activePlayerUI;

    public GameObject P1UI;
    public GameObject P2UI;
    public GameObject P3UI;
    public GameObject P4UI;

    public TextMeshProUGUI P1Name;
    public TextMeshProUGUI P2Name;
    public TextMeshProUGUI P3Name;
    public TextMeshProUGUI P4Name;

    public TextMeshProUGUI P1Stun;
    public TextMeshProUGUI P2Stun;
    public TextMeshProUGUI P3Stun;
    public TextMeshProUGUI P4Stun;

    public GameObject P1GreyKey1;
    public GameObject P1GreyKey2;
    public GameObject P1GreyKey3;
    public GameObject P2GreyKey1;
    public GameObject P2GreyKey2;
    public GameObject P2GreyKey3;
    public GameObject P3GreyKey1;
    public GameObject P3GreyKey2;
    public GameObject P3GreyKey3;
    public GameObject P4GreyKey1;
    public GameObject P4GreyKey2;
    public GameObject P4GreyKey3;

    public int keySpawnCountdown;

    void Start()
    {
        P1Name.text = PlayerPrefs.GetString("P1Name", "PlayerNotFound");
        P2Name.text = PlayerPrefs.GetString("P2Name", "PlayerNotFound");
        P3Name.text = PlayerPrefs.GetString("P3Name", "PlayerNotFound");
        P4Name.text = PlayerPrefs.GetString("P4Name", "PlayerNotFound");

        P1Stun.text = PlayerPrefs.GetString("P1Name", "PlayerNotFound");
        P2Stun.text = PlayerPrefs.GetString("P2Name", "PlayerNotFound");
        P3Stun.text = PlayerPrefs.GetString("P3Name", "PlayerNotFound");
        P4Stun.text = PlayerPrefs.GetString("P4Name", "PlayerNotFound");

        switch (PlayerPrefs.GetInt("PlayerAmount"))
        {
            case 2:
                Destroy(GameObject.Find("Player 3"));
                Destroy(GameObject.Find("Player 4"));
                P3UI.SetActive(false);
                P4UI.SetActive(false);
                Destroy(P3Stun.gameObject);
                Destroy(P4Stun.gameObject);
                break;
            case 3:
                Destroy(GameObject.Find("Player 4"));
                P4UI.SetActive(false);
                Destroy(P4Stun.gameObject);
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
            gameObject.GetComponent<SP_ItemSpawn>().SpawnItem();
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
                switch (keys)
                {
                    case 0:

                        break;
                    case 1:
                        P1GreyKey1.SetActive(false);
                        break;
                    case 2:
                        P1GreyKey2.SetActive(false);
                        break;
                    case 3:
                        P1GreyKey3.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (keys)
                {
                    case 0:

                        break;
                    case 1:
                        P2GreyKey1.SetActive(false);
                        break;
                    case 2:
                        P2GreyKey2.SetActive(false);
                        break;
                    case 3:
                        P2GreyKey3.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (keys)
                {
                    case 0:

                        break;
                    case 1:
                        P3GreyKey1.SetActive(false);
                        break;
                    case 2:
                        P3GreyKey2.SetActive(false);
                        break;
                    case 3:
                        P3GreyKey3.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                switch (keys)
                {
                    case 0:

                        break;
                    case 1:
                        P4GreyKey1.SetActive(false);
                        break;
                    case 2:
                        P4GreyKey2.SetActive(false);
                        break;
                    case 3:
                        P4GreyKey3.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
}
