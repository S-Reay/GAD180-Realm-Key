using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public GameObject pauseUI;

    public int keySpawnCountdown;

    public GameObject audioManager;
    bool paused = false;

    public GameObject winUI;
    public GameObject gameUI;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI flavourText;

    void Start()
    {
        P1Name.text = PlayerPrefs.GetString("P1Name", "Red");
        P2Name.text = PlayerPrefs.GetString("P2Name", "Blue");
        P3Name.text = PlayerPrefs.GetString("P3Name", "Green");
        P4Name.text = PlayerPrefs.GetString("P4Name", "Yellow");

        P1Stun.text = PlayerPrefs.GetString("P1Name", "Red");
        P2Stun.text = PlayerPrefs.GetString("P2Name", "Blue");
        P3Stun.text = PlayerPrefs.GetString("P3Name", "Green");
        P4Stun.text = PlayerPrefs.GetString("P4Name", "Yellow");

        winUI.SetActive(false); 
        gameUI.SetActive(true); 

        if (string.IsNullOrEmpty(P1Name.text))
        {
            P1Name.text = "Red";
            P1Stun.text = "Red";
        }
        if (string.IsNullOrEmpty(P2Name.text))
        {
            P2Name.text = "Blue";
            P2Stun.text = "Blue";
        }
        if (string.IsNullOrEmpty(P3Name.text))
        {
            P3Name.text = "Green";
            P3Stun.text = "Green";
        }
        if (string.IsNullOrEmpty(P4Name.text))
        {
            P4Name.text = "Yellow";
            P4Stun.text = "Yellow";
        }

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
                PlayerPrefs.SetInt("PlayerAmount", 4);
                Debug.LogError("PlayerPrefs:PlayerAmount Is not within expected range, defaulting to 4 player mode");
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

        audioManager.GetComponent<SP_AudioManager>().PlayBG();
    }

    public void NextPlayer()
    {
        Debug.Log("Next player called");

        audioManager.GetComponent<SP_AudioManager>().PlaySound(7);

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
                activePlayerUI.text = P1Name.text + "'s Turn";
                break;
            case 1:
                activePlayerUI.text = P2Name.text + "'s Turn";
                break;
            case 2:
                activePlayerUI.text = P3Name.text + "'s Turn";
                break;
            case 3:
                activePlayerUI.text = P4Name.text + "'s Turn";
                break;
            default:
                break;
        }
    }

    void Update()
    {
        DisplayActivePlayerName();
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            PauseMenu();
        }
    }

    public void StealKey(GameObject thief)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != thief)
            {
                if (Vector3.Distance(thief.transform.position, players[i].transform.position) < 1)
                {
                    audioManager.GetComponent<SP_AudioManager>().PlaySound(2);
                    players[i].GetComponent<SP_PlayerController>().holdingKey = false;
                    thief.GetComponent<SP_PlayerController>().holdingKey = true;
                }
            }
        }
    }

    public void PauseMenu()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void Unpause()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        SceneManager.LoadScene("SP_Menu");
    }

    public void PlayerWins(int playerID)
    {
        string winnerName = "";
        winUI.SetActive(true);
        gameUI.SetActive(false);
        switch (playerID)
        {
            case 0:
                winnerName = P1Name.text;
                break;
            case 1:
                winnerName = P2Name.text;
                break;
            case 2:
                winnerName = P3Name.text;
                break;
            case 3:
                winnerName = P4Name.text;
                break;
            default:
                break;
        }
        winnerText.text = winnerName + " Wins!";
        flavourText.text = winnerName + " has retrieved 3 magical keys and shall be crowned as the new King.";
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
