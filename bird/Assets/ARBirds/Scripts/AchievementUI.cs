using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public TMP_Text Title;
    public TMP_Text Description;
    public Image Status;

    public void SetAchievementEntry(Achievement achievement)
    {
        Title.text = achievement.Title;
        Description.text = achievement.Description;
        Color green = Color.green;
        Color red = Color.red;
        if(achievement.Unlocked)
            Status.color = green;
        else
            Status.color = red;
    }

    public void SetNotification(Achievement achievement)
    {
        Title.text = achievement.Title;
        Description.text = achievement.Description;
    }
}
