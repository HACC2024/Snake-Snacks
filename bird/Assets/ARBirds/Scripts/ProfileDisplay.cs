using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProfileDisplay : MonoBehaviour
{
    public TMP_Text playerName;
    public TMP_Text level;
    public TMP_Text currentExp;
    public TMP_Text maxExp;
    public TMP_Text birdsFound;
    public TMP_Text totalAchieved;
    public GameObject profileUI;
    private bool toggle = true;


    void Start()
    {
        SetInfo();
        ToggleUI();
    }

    private void SetInfo()
    {
        playerName.text = PlayerPrefs.GetString("playerName").ToString();
        level.text = PlayerPrefs.GetInt("level").ToString();
        currentExp.text = PlayerPrefs.GetFloat("currentEXP").ToString();
        maxExp.text = PlayerPrefs.GetFloat("maxEXP").ToString();
    }

    public void ToggleUI()
    {
        toggle = !toggle;
        profileUI.SetActive(toggle);
    }
}
