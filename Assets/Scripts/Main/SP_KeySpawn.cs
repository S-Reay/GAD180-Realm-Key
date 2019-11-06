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


    void SpawnKey()
    {
        int whichSpace = Random.Range(0, keySpawns.Count);

        Instantiate(key, keySpawns[whichSpace].transform.position, transform.rotation);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnKey();
        }
    }
}
