using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SP_PlayerController : MonoBehaviour
{

    /// <summary>
    /// TO DO
    /// Remove 'previous space' at beginning of turn
    /// </summary>
    public int state = 0;

    public Text diceText;

    public GameObject previousSpace;
    public GameObject currentSpace;
    public GameObject destination;

    public GameObject dice;
    public int moves;
    public bool startOfTurn;

    void Update()
    {

        switch (state)
        {
            case 0: //Idle (waiting for roll)
                Debug.Log("State 0");
                if (Input.GetKeyDown(KeyCode.R))
                {
                    moves = dice.GetComponent<SP_DiceRoll>().RollDice();
                    startOfTurn = true;
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
        for (int i = 0; i < currentSpace.transform.childCount; i++)
        {
            currentSpace.transform.GetChild(i).gameObject.SetActive(true);
        }

            for (int i = 0; i < currentSpace.transform.childCount; i++)
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

                        for (int i = 0; i < currentSpace.transform.childCount; i++)
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
            state = 0;  //return state to idle once moves have run out
        }
        startOfTurn = false;
    }

    void DetermineDestination()
    {
        if (!currentSpace.GetComponent<SP_NodeScript>().isFork)
        {
            if (currentSpace.GetComponent<SP_NodeScript>().nextSpace1 == previousSpace)
            {
                destination = currentSpace.GetComponent<SP_NodeScript>().nextSpace2;
            }
            else if (currentSpace.GetComponent<SP_NodeScript>().nextSpace2 == previousSpace)
            {
                destination = currentSpace.GetComponent<SP_NodeScript>().nextSpace1;
            }
            else
            {
                Debug.LogError("CANNOT DETERMINE NEXT SPACE");
            }

            state = 2;
        }
        else
        {
            state = 1;
        }
    }
}
