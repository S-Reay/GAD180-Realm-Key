using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SP_PlayerController : MonoBehaviour
{
    public int state;
    public int team;
    /* Teams:
    * 0 - None
    * 1 - Red (Player 1)
    * 2 - Blue (Player 2)
    * 3 - Green (Player 3)
    * 4 - Yellow (Player 4)
    */
    public Text diceText;
    public GameObject dice;

    public GameObject previousSpace;
    public GameObject currentSpace;
    public GameObject destination;

    public GameObject gameManager;
    public int moves;
    public int keys;

    public bool holdingKey;
    public GameObject holdingItem;

    public int[] items = new int[3];
    public Image[] itemSpritesUI = new Image[3];
     /* Item Codes:
     *  0 - Empty Slot
     *  1 - Stun
     *  2 - Triple Dice
     *  3 - Rigged Dice
     */

    public GameObject rollUI;
    public GameObject useItemUI;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        state = -1;
        moves = 0;
        keys = 0;

        switch (gameObject.name)
        {
            case "Player 1":
                team = 1;
                break;
            case "Player 2":
                team = 2;
                break;
            case "Player 3":
                team = 3;
                break;
            case "Player 4":
                team = 4;
                break;
            default:
                break;
        }
        gameManager.GetComponent<SP_GameManager>().UpdatePlayerUI(team, keys);
    }


    void Update()
    {
        switch (state)
        {
            case -1: //Innactive (Not This Player's Turn)
                break;

            case 0: //Idle (waiting for roll)

                rollUI.SetActive(true);
                useItemUI.SetActive(false);
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != 0)
                    {
                        useItemUI.SetActive(true);
                    }
                }

                break;
            case 1: //Select Direction
                SelectDirection();
                break;
            case 2: //Move to next space
                MovePlayer();
                break;
            case 3: //Determine next space
                DetermineDestination();
                break;
            default:
                Debug.LogError("State Default");
                break;
        }

        transform.position = currentSpace.transform.position;

    }

    public void Roll()
    {
        if (state == 0)
        {
            moves = dice.GetComponent<SP_DiceRoll>().RollDice();
            currentSpace.GetComponent<SP_NodeScript>().isOccupied = false;
            state = 1;
            rollUI.SetActive(false);
        }
    }

    void SelectDirection()
    {
        for (int i = 1; i < currentSpace.transform.childCount; i++)
        {
            currentSpace.transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 1; i < currentSpace.transform.childCount; i++)                                                     //for loop only ignores child with index 0, meaning the model must be at index 0
        {
            if (currentSpace.transform.GetChild(i).gameObject.GetComponent<SP_Pointer>().pointedNode == previousSpace)  //determines which pointer points to the previous space
            {
                currentSpace.transform.GetChild(i).gameObject.SetActive(false);                                         //disables it
            }

        }

        //activate pointers for player input


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        //Choose Direction
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Pointer")
                {
                    Debug.Log("Player has clicked " + hit.transform.gameObject.name);

                    for (int i = 1; i < currentSpace.transform.childCount; i++)
                    {
                        currentSpace.transform.GetChild(i).gameObject.SetActive(false);
                    }

                    destination = hit.transform.gameObject.GetComponent<SP_Pointer>().pointedNode;

                    state = 2;
                }
            }
        }
    }

    void MovePlayer()
    {
        if (moves > 1)
        {
            previousSpace = currentSpace;
            currentSpace = destination;
            moves--;
            state = 3;
        }
        else if (moves == 1)
        {
            previousSpace = currentSpace;
            currentSpace = destination;
            moves--;
            CheckSpace();
            currentSpace.GetComponent<SP_NodeScript>().isOccupied = true;
            state = -1; //ends player's turn when moves run out
            gameManager.GetComponent<SP_GameManager>().NextPlayer();
        }
        else
        {
            state = -1; //ends player's turn when moves run out
            gameManager.GetComponent<SP_GameManager>().NextPlayer();
        }
    }

    void DetermineDestination()
    {


        if (!currentSpace.GetComponent<SP_NodeScript>().isFork) //if this isn't a fork, determine the next space
        {

            if (currentSpace.transform.Find("Pointer (0)").GetComponent<SP_Pointer>().pointedNode == previousSpace)
            {
                destination = currentSpace.transform.Find("Pointer (1)").GetComponent<SP_Pointer>().pointedNode;
            }
            else if (currentSpace.transform.Find("Pointer (1)").GetComponent<SP_Pointer>().pointedNode == previousSpace)
            {
                destination = currentSpace.transform.Find("Pointer (0)").GetComponent<SP_Pointer>().pointedNode;
            }
            else
            {
                Debug.LogError("Cannot Determine Next Space");
            }
            state = 2;
        }
        else                    //if it is a fork, promt the player to choose a direction
        {
            state = 1;          
        }
    }

    void CheckSpace()
    {
        if (currentSpace.GetComponent<SP_NodeScript>().heldKey != null && !holdingKey)
        {
            Destroy(currentSpace.GetComponent<SP_NodeScript>().heldKey);
            currentSpace.GetComponent<SP_NodeScript>().heldKey = null;
            holdingKey = true;
        }
        else if (currentSpace.GetComponent<SP_NodeScript>().heldItem != null)   //If the space has an item
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == 0)                                              //If the player has a free space
                {
                    items [i] = currentSpace.GetComponent<SP_NodeScript>().heldItem.GetComponent<SP_Item>().itemID; //Adds to the first available slot
                    itemSpritesUI[i].sprite = currentSpace.GetComponent<SP_NodeScript>().heldItem.GetComponent<SP_Item>().itemSprite;
                    Destroy(currentSpace.GetComponent<SP_NodeScript>().heldItem);
                    currentSpace.GetComponent<SP_NodeScript>().heldItem = null;
                    return;
                }
            }
        }
        else if (team == currentSpace.GetComponent<SP_NodeScript>().team && holdingKey)
        {
            //Key is Captured
            holdingKey = false;
            keys++;
            gameManager.GetComponent<SP_GameManager>().UpdatePlayerUI(team, keys);
        }
    }
}
