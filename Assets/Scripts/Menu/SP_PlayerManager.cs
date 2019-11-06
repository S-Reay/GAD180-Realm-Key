using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    }
}
