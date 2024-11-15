using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementUI : MonoBehaviour
{
    public GameObject achievementUIPrefab;
    public TMP_Text Title;
    public TMP_Text Description;


    public void DisplayAchievement(Achievement achievement)
    {
        GameObject achievementUI = Instantiate(achievementUIPrefab, achievementUIPrefab.transform);
        achievementUI.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = achievement.Title;
        achievementUI.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = achievement.Description;

        Destroy(achievementUI, 2f);
    }

    public void SetAchievementData(Achievement achievement)
    {
        //set data for entry prefabs
    }
}
