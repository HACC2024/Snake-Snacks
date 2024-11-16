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
    public Player_Information playerInfo;
    public AchievementManager achMan;
    private bool toggle = true;


    void Start()
    {
        SetInfo();
        achMan.PopulateAchievements();
        ToggleUI();
        playerInfo = GameObject.Find("--------Player Information---------").GetComponent<Player_Information>();
    }

    private void SetInfo()
    {
        playerName.text = playerInfo.Player_Name;
        level.text = playerInfo.Level.ToString();
        currentExp.text = playerInfo.Current_EXP.ToString();
        maxExp.text = playerInfo.Max_EXP.ToString();
        birdsFound.text = playerInfo.Unique_Birds_Caught.Count.ToString();
        totalAchieved.text = playerInfo.Achievements.Count.ToString();
    }

    public void ToggleUI()
    {
        SetInfo();
        toggle = !toggle;
        profileUI.SetActive(toggle);
    }
}
