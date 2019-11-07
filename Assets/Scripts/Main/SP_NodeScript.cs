using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_NodeScript : MonoBehaviour
{
    public bool isFork;
    public bool isKeySpawn;
    public bool isOccupied;
    public int  team;
    /* Teams:
     * 0 - None
     * 1 - Red (Player 1)
     * 2 - Blue (Player 2)
     * 3 - Green (Player 3)
     * 4 - Yellow (Player 4)
     */
    public GameObject heldItem;
    public GameObject heldKey;

    public GameObject nextSpace1;
    public GameObject nextSpace2;
    public GameObject nextSpace3;


    void Start()
    {
        if (!isFork)
        {
            nextSpace3 = null;
        }
        heldItem = null;
        heldKey = null;
        isOccupied = false;
    }

    public GameObject DetermineNextSpace(GameObject previousSpace)
    {
        GameObject destination = null;

        if(isFork)
        {
            //Take player input
            //Offer 1 2 or 3, but not whichever is equal to previousSpace
        }
        else if(previousSpace == nextSpace1)
        {
            destination = nextSpace2;
        }
        else if (previousSpace == nextSpace2)
        {
            destination = nextSpace1;
        }

        return destination;
    }
}
