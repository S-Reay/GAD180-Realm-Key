using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Freemium : MonoBehaviour
{
    /* Skins
     * 0 - Knight
     * 1 - Merchant
     * 2 - Archer
     * 3 - Priest
     * 4 - Rogue
     * 5 - Wizard
     */
    public GameObject[] redSkinIcons = new GameObject[6];
    public GameObject[] blueSkinIcons = new GameObject[6];
    public GameObject[] greenSkinIcons = new GameObject[6];
    public GameObject[] yellowSkinIcons = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
        redSkinIcons[0].SetActive(true);
        blueSkinIcons[0].SetActive(true);
        greenSkinIcons[0].SetActive(true);
        yellowSkinIcons[0].SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < redSkinIcons.Length; i++)
        {
            bool isBought = PlayerPrefs.GetInt(i.ToString(), 0) != 0;
            if (isBought)
            {
                redSkinIcons[i].SetActive(true);
            }
        }
        for (int i = 1; i < blueSkinIcons.Length; i++)
        {
            bool isBought = PlayerPrefs.GetInt(i.ToString(), 0) != 0;
            if (isBought)
            {
                blueSkinIcons[i].SetActive(true);
            }
        }
        for (int i = 1; i < greenSkinIcons.Length; i++)
        {
            bool isBought = PlayerPrefs.GetInt(i.ToString(), 0) != 0;
            if (isBought)
            {
                greenSkinIcons[i].SetActive(true);
            }
        }
        for (int i = 1; i < yellowSkinIcons.Length; i++)
        {
            bool isBought = PlayerPrefs.GetInt(i.ToString(), 0) != 0;
            if (isBought)
            {
                yellowSkinIcons[i].SetActive(true);
            }
        }
    }

    public void PurchaseSkin(int skinCode)
    {
        PlayerPrefs.SetInt(skinCode.ToString(), 1);
    }
}
