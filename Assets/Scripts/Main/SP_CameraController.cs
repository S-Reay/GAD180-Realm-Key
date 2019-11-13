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
        transform.LookAt(target.transform.position);

        if (Input.GetKey(KeyCode.A))    //Left
        {
            transform.Translate(Vector3.left * Time.deltaTime * cameraSpeed);
        }
        else if (Input.GetKey(KeyCode.D))    //Right
        {
            transform.Translate(Vector3.right * Time.deltaTime * cameraSpeed);
        }


    }
}
