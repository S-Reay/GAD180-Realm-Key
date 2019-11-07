using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_KeySpawn : MonoBehaviour
{
    public List<GameObject> keySpawns = new List<GameObject>();
    public GameObject key;

    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Node").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Node")[i].GetComponent<SP_NodeScript>().isKeySpawn)
            {
                keySpawns.Add(GameObject.FindGameObjectsWithTag("Node")[i]);
            }
        }
    }


    public void SpawnKey()
    {
        int whichSpace = Random.Range(0, keySpawns.Count);

        if (keySpawns[whichSpace].gameObject.GetComponent<SP_NodeScript>().heldKey == null && !keySpawns[whichSpace].gameObject.GetComponent<SP_NodeScript>().isOccupied)   //Checks if the chosen spawn point already has a key or player
        {
            keySpawns[whichSpace].gameObject.GetComponent<SP_NodeScript>().heldKey = Instantiate(key, keySpawns[whichSpace].transform.position, transform.rotation);        //Spawns a key in the chosen spawn point if it is empty
        }  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnKey();
        }
    }
}
