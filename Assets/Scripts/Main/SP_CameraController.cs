using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_CameraController : MonoBehaviour
{
    public GameObject target;
    public float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))    //Left
        {
            target.transform.Rotate(0f, cameraSpeed, 0f);
        }
        else if (Input.GetKey(KeyCode.D))    //Right
        {
            target.transform.Rotate(0f, -cameraSpeed, 0f);
        }


    }
}
