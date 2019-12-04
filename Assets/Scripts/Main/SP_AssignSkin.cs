using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_AssignSkin : MonoBehaviour
{

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public GameObject[] p1Skins = new GameObject[6];
    public GameObject[] p2Skins = new GameObject[6];
    public GameObject[] p3Skins = new GameObject[6];
    public GameObject[] p4Skins = new GameObject[6];

    /* Skins
    * 0 - Knight
    * 1 - Merchant
    * 2 - Archer
    * 3 - Priest
    * 4 - Rogue
    * 5 - Wizard
    */

    void Start()
    {
        GameObject P1Model = Instantiate(p1Skins[PlayerPrefs.GetInt("P1SkinID")], Player1.transform);
        GameObject P2Model = Instantiate(p2Skins[PlayerPrefs.GetInt("P2SkinID")], Player2.transform);
        GameObject P3Model = Instantiate(p3Skins[PlayerPrefs.GetInt("P3SkinID")], Player3.transform);
        GameObject P4Model = Instantiate(p4Skins[PlayerPrefs.GetInt("P4SkinID")], Player4.transform);
        P1Model.transform.localPosition = new Vector3(0, 0, 0);
        P2Model.transform.localPosition = new Vector3(0, 0, 0);
        P3Model.transform.localPosition = new Vector3(0, 0, 0);
        P4Model.transform.localPosition = new Vector3(0, 0, 0);
    }
}
