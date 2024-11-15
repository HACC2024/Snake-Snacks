using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public AchievementUI achievementDisplay;
    public Transform contentPanel;
    public GameObject entryPrefab;
    private AchievementList achievementList;
    private Player_Information playerInfo;

    void Start()
    {
        achievementList = Resources.Load<AchievementList>("AchievementList");
        playerInfo = GameObject.Find("--------Player Information---------").GetComponent<Player_Information>();
    }
    
    public void UnlockAchievement(string achievementID)
    {
        foreach (var achievement in achievementList.achievements)
        {
            if (achievement.AchievementID == achievementID)
            {
                // Logic for unlocking the achievement
                achievementDisplay.DisplayAchievement(achievement);
                //save achievement
                achievement.Unlocked = true;
                playerInfo.Achievements.Add(achievementID);
                break;
            }
        }
    }

    public void ShowAchievements()
    {
        foreach(var achievement in achievementList.achievements)
        {
            GameObject entry = Instantiate(entryPrefab, contentPanel);

        }
    }

    // private void LoadAchievements()
    // {
    //     foreach (var achievement in achievementList.achievements)
    //     {
    //         if (achievement.Unlocked)
    //         {
    //             // Achievement already unlocked, handle accordingly
    //         }
    //     }
    // }
}
