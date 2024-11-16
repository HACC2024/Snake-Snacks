using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    //For keeping information
    public static AchievementManager Instance;
    public List<Achievement> achievementList;
    private HashSet<string> unlockedAchievements = new HashSet<string>();
    public Player_Information playerInfo;

    [Header("Notifications")]
    public GameObject notificationPrefab;
    public Transform notificationParent;

    [Header("UI Settings")]
    public GameObject entryPrefab;
    public Transform contentPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerInfo = GameObject.Find("--------Player Information---------").GetComponent<Player_Information>();
    }

    void Update()
    {
        if(playerInfo.Unique_Birds_Caught.Count >= 1 && playerInfo.Current_EXP > 0 && IsAchievementUnlocked("FirstBird") == false)
        {
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name == "GPSTesting")
            {
               UnlockAchievement("FirstBird"); 
            }
        }
    }

    public void UnlockAchievement(string achievementID)
    {
        foreach (var achievement in achievementList)
        {
            if (achievement.AchievementID == achievementID)
            {
                DisplayAchievementNotif(achievement);
                achievement.Unlocked = true;
                unlockedAchievements.Add(achievementID);
                playerInfo.Add_Achievement(achievementID);
                break;
            }
        }
    }

    public void DisplayAchievementNotif(Achievement achievement)
    {
        GameObject notification = Instantiate(notificationPrefab, notificationParent);
        Debug.Log("notif displayed");
        AchievementUI notifUI = notification.GetComponent<AchievementUI>();
        notifUI.SetNotification(achievement);
        Destroy(notification, 3f);
    }

    public void PopulateAchievements()
    {
        foreach(Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
        foreach(var achievement in achievementList)
        {
            GameObject entry = Instantiate(entryPrefab, contentPanel);
            AchievementUI entryUI = entry.GetComponent<AchievementUI>();
            entryUI.SetAchievementEntry(achievement);
        }
    }

    // METHODS WITH PLAYER INFORMATION

    // public void UnlockAchievement(string achievementID)
    // {
    //     Debug.Log($"Searching for achievement: {achievementID}");
    //     foreach (var achievement in achievementList)
    //     {
    //         if (achievement.AchievementID == achievementID)
    //         {
    //             //Logic for unlocking the achievement
    //             DisplayAchievementNotif(achievement);
    //             //save achievement
    //             achievement.Unlocked = true;
    //             unlockedAchievements.Add(achievementID);
    //             playerInfo.Add_Achievement(achievementID);
    //         }
    //     }
    // }

    public bool IsAchievementUnlocked(string achievementID)
    {
        return unlockedAchievements.Contains(achievementID);
    }

    // private void LoadAchievements()
    // {
    //     // Example: Load unlocked achievements from PlayerPrefs
    //     string savedData = PlayerPrefs.GetString("Achievements", "");
    //     var unlockedAchievements = new HashSet<string>(savedData.Split(','));

    //     // Mark achievements as unlocked in their definitions
    //     foreach (var achievement in achievementList)
    //     {
    //         if (unlockedAchievements.Contains(achievement.achievementID))
    //         {
    //             achievement.Unlocked = true;
    //         }
    //     }
    // }


}
