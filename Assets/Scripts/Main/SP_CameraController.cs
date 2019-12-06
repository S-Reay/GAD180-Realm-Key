using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_CameraController : MonoBehaviour
{
    public GameObject target;
    public GameObject mapCentre;
    public GameObject activePlayer;
    public bool mapMode = false;
    public float cameraSpeed;

    private void Start()
    {
        UpdateMode();
    }
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
        else if (Input.GetKeyDown(KeyCode.M))
        {
            UpdateMode();
        }
        if (!mapMode)
        {
            target.transform.position = activePlayer.transform.position;
        }

    }

    public void UpdateMode()
    {
            if (!mapMode)       //Set to map mode
            {
                target.transform.position = mapCentre.transform.position;   //Move the target to the centre
                transform.localPosition = new Vector3(0f, 75f, 0f);
                transform.eulerAngles = new Vector3(90, 0, 0);

            }
            else                //Set to player mode
            {
                if (activePlayer != null)
                {
                    target.transform.position = activePlayer.transform.position;
                    transform.localPosition = new Vector3(8f, 4.3f, 0);
                    transform.LookAt(target.transform.position);
                }
            }
            mapMode = !mapMode; //toggle boolean

    }
}
