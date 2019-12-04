using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SP_PlayerManager : MonoBehaviour
{    
    public GameObject selectPlayerAmountUI;
    public GameObject enterNameUI;

    public InputField p1Input;
    public InputField p2Input;
    public InputField p3Input;
    public InputField p4Input;

    public GameObject p1UI;
    public GameObject p2UI;
    public GameObject p3UI;
    public GameObject p4UI;

    public GameObject p1Portrait;
    public GameObject p2Portrait;
    public GameObject p3Portrait;
    public GameObject p4Portrait;

    public int p1SkinID = 0;
    public int p2SkinID = 0;
    public int p3SkinID = 0;
    public int p4SkinID = 0;

    public Sprite[] p1Sprites = new Sprite[6];
    public Sprite[] p2Sprites = new Sprite[6];
    public Sprite[] p3Sprites = new Sprite[6];
    public Sprite[] p4Sprites = new Sprite[6];

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
        PlayerPrefs.DeleteAll();
        selectPlayerAmountUI.SetActive(true);
        enterNameUI.SetActive(false);
        p1UI.SetActive(false);
        p2UI.SetActive(false);
        p3UI.SetActive(false);
        p4UI.SetActive(false);
    }

    public void ChoosePlayerAmount(int x)
    {
        PlayerPrefs.SetInt("PlayerAmount", x);
        selectPlayerAmountUI.SetActive(false);
        enterNameUI.SetActive(true);


        p1UI.SetActive(true);
        p2UI.SetActive(true);
        if (PlayerPrefs.GetInt("PlayerAmount") >=3)
        {
            p3UI.SetActive(true);
        }
        if (PlayerPrefs.GetInt("PlayerAmount") == 4)
        {
            p4UI.SetActive(true);
        }
    }


    public void SetName()
    {
        PlayerPrefs.SetString("P1Name", p1Input.text);
        PlayerPrefs.SetString("P2Name", p2Input.text);
        PlayerPrefs.SetString("P3Name", p3Input.text);
        PlayerPrefs.SetString("P4Name", p4Input.text);
    }

    public void SetSkin1(int skinID)
    {
        p1SkinID = skinID;
        PlayerPrefs.SetInt("P1SkinID", skinID);
    }
    public void SetSkin2(int skinID)
    {
        p2SkinID = skinID;
        PlayerPrefs.SetInt("P2SkinID", skinID);
    }
    public void SetSkin3(int skinID)
    {
        p3SkinID = skinID;
        PlayerPrefs.SetInt("P3SkinID", skinID);
    }
    public void SetSkin4(int skinID)
    {
        p4SkinID = skinID;
        PlayerPrefs.SetInt("P4SkinID", skinID);
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    void Update()
    {
        Debug.Log(PlayerPrefs.GetString("P1Name"));
        Debug.Log(PlayerPrefs.GetString("P2Name"));
        Debug.Log(PlayerPrefs.GetString("P3Name"));
        Debug.Log(PlayerPrefs.GetString("P4Name"));

        if (p1Portrait.GetComponent<Image>().sprite == null)
        {
            p1Portrait.SetActive(false);
        }
        else
        {
            p1Portrait.SetActive(true);
        }

        ////
        if (p2Portrait.GetComponent<Image>().sprite == null)
        {
            p2Portrait.SetActive(false);
        }
        else
        {
            p2Portrait.SetActive(true);
        }

        /////
        if (p3Portrait.GetComponent<Image>().sprite == null)
        {
            p3Portrait.SetActive(false);
        }
        else
        {
            p3Portrait.SetActive(true);
        }

        /////
        if (p4Portrait.GetComponent<Image>().sprite == null)
        {
            p4Portrait.SetActive(false);
        }
        else
        {
            p4Portrait.SetActive(true);
        }

        p1Portrait.GetComponent<Image>().sprite = p1Sprites[p1SkinID];
        p2Portrait.GetComponent<Image>().sprite = p2Sprites[p2SkinID];
        p3Portrait.GetComponent<Image>().sprite = p3Sprites[p3SkinID];
        p4Portrait.GetComponent<Image>().sprite = p4Sprites[p4SkinID];
    }
}
