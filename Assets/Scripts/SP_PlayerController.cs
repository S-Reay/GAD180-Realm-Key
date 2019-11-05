using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SP_PlayerController : MonoBehaviour
{

    /// <summary>
    /// TO DO
    /// Turn system
    /// </summary>
    public int state = 0;

    public Text diceText;

    public GameObject previousSpace;
    public GameObject currentSpace;
    public GameObject destination;

    public GameObject dice;
    public int moves;

    void Update()
    {
        switch (state)
        {
            case -1: //Not This Player's Turn

                break;

            case 0: //Idle (waiting for roll)
                Debug.Log("State 0");
                if (Input.GetKeyDown(KeyCode.R))
                {
                    moves = dice.GetComponent<SP_DiceRoll>().RollDice();
                    previousSpace = null;   //IS THIS OK?
                    state = 1;
                }
                break;
            case 1: //Select Direction
                Debug.Log("State 1");
                SelectDirection();
                break;
            case 2: //Move to next space
                Debug.Log("State 2");
                MovePlayer();
                break;
            case 3: //Determine next space
                Debug.Log("State 3");
                DetermineDestination();
                break;
            default:
                Debug.Log("State Default");
                break;
        }

        transform.position = currentSpace.transform.position;
        diceText.text = moves.ToString();

    }

    void SelectDirection()
    {
        for (int i = 1; i < currentSpace.transform.childCount; i++)
        {
            currentSpace.transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 1; i < currentSpace.transform.childCount; i++)                 //for loop only ignores child with index 0, meaning the model must be at index 0
        {
            if (currentSpace.transform.GetChild(i).gameObject.GetComponent<SP_Pointer>().pointedNode == previousSpace)   //determines which pointer points to the previous space
            {
                currentSpace.transform.GetChild(i).gameObject.SetActive(false);                                             //disables it
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
            state = 0;
        }
        else
        {
            state = -1; //ends player's turn when moves run out
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
}
