using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_ItemSpawn : MonoBehaviour
{
    public List<GameObject> itemSpawns = new List<GameObject>();
    public GameObject stun;
    public GameObject tripleDice;
    public GameObject riggedDice;
    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Node").Length; i++)
        {
            if (!GameObject.FindGameObjectsWithTag("Node")[i].GetComponent<SP_NodeScript>().isKeySpawn
                && GameObject.FindGameObjectsWithTag("Node")[i].GetComponent<SP_NodeScript>().team == 0)
            {
                itemSpawns.Add(GameObject.FindGameObjectsWithTag("Node")[i]);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnItem();
            }

        }
    }

    public void SpawnItem()
    {
        int whichSpace = Random.Range(0, itemSpawns.Count);
        int whichItem = Random.Range(0, 3);

        switch (whichItem)
        {
            case 0:     //Stun
                if (itemSpawns[whichSpace].GetComponent<SP_NodeScript>().heldItem == null && !itemSpawns[whichSpace].GetComponent<SP_NodeScript>().isOccupied)
                {
                    itemSpawns[whichSpace].GetComponent<SP_NodeScript>().heldItem = Instantiate(stun, itemSpawns[whichSpace].transform.position, transform.rotation);
                }
                break;
            case 1:     //TripleDice
                if (itemSpawns[whichSpace].GetComponent<SP_NodeScript>().heldItem == null && !itemSpawns[whichSpace].GetComponent<SP_NodeScript>().isOccupied)
                {
                    itemSpawns[whichSpace].GetComponent<SP_NodeScript>().heldItem = tripleDice = Instantiate(tripleDice, itemSpawns[whichSpace].transform.position, transform.rotation);
                }
                break;
            case 2:     //RiggedDice
                if (itemSpawns[whichSpace].GetComponent<SP_NodeScript>().heldItem == null && !itemSpawns[whichSpace].GetComponent<SP_NodeScript>().isOccupied)
                {
                    itemSpawns[whichSpace].GetComponent<SP_NodeScript>().heldItem = riggedDice = Instantiate(riggedDice, itemSpawns[whichSpace].transform.position, transform.rotation);        //THIS MAY BE CAUSING A MAJOR BUG :(
                    
                }
                break;
            default:
                break;
        }

        Debug.LogWarning("Attempted to Spawn Item\nwhichSpace: " + whichSpace + "   whichItem: " + whichItem);
    }
}
