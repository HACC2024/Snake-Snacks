using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.CloudSave.Models.Data.Player;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Information : MonoBehaviour
{
    [SerializeField] public string Player_Name;
    [SerializeField] public int Level;
    [SerializeField] public float Current_EXP;
    [SerializeField] public float Max_EXP;
    [SerializeField] public List<string> Unique_Birds_Caught;
    [SerializeField] public List<string> Achievements;

    [SerializeField] private GameObject No_Token_Path;
    [SerializeField] private GameObject Attempting_Log_In;
    [SerializeField] private bool Clear_Token;

    async private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void Start()
    {
        if (Clear_Token) { AuthenticationService.Instance.ClearSessionToken(); }
        if (AuthenticationService.Instance.SessionTokenExists)
        {
            Sign_In_Cached_Player();
            //Load GPS scene
        }
        else
        {
            No_Token_Path.SetActive(true);
            Attempting_Log_In.SetActive(false);
            //Load SignIn/LogIn
        }
    }

    async private void Sign_In_Cached_Player()
    {
        // Check if a cached player already exists by checking if the session token exists
        if (!AuthenticationService.Instance.SessionTokenExists)
        {
            // if not, then do nothing
            return;
        }

        // Sign in Anonymously
        // This call will sign in the cached player.
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Cached user sign in succeeded!");

            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            Online_Initalize();
            SceneManager.LoadScene("GPSTesting", LoadSceneMode.Single);
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    private void Online_Initalize()
    {
        Player_Name = AuthenticationService.Instance.PlayerName;
        Load_Data();
    }

    private void Offline_Initalize()
    {
        Offline_Load();
    }

    public void Add_EXP(float score)
    {
        var tmp = score / 1000;
        Current_EXP += tmp;
        if(Current_EXP > Max_EXP)
        {
            Current_EXP -= Max_EXP;
            Level += 1;
            Max_EXP = Level * 1000;
        }
    }
    public void Add_Bird(string bird)
    {
        if(!Unique_Birds_Caught.Contains(bird)) { Unique_Birds_Caught.Add(bird); }
    }

    public void Add_Achievement(string achievementID)
    {
        if(!Achievements.Contains(achievementID))
        {
            Achievements.Add(achievementID);
        }
    }

    public async void Save_Data()
    {
        string uniqueBirds = string.Join(",", Unique_Birds_Caught);
        string totalAchievements = string.Join(",", Achievements);

        var timeNow = System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss,fff");

        var playerData = new Dictionary<string, object>()
        {
            {"time", timeNow },
            {"playerName", AuthenticationService.Instance.PlayerName},
            {"level",  Level},
            {"currentEXP", Current_EXP},
            {"maxEXP", Max_EXP},
            {"uniqueBirds", uniqueBirds},
            {"totalAchievements", totalAchievements},
        };

        //await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);

        try
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(playerData, new SaveOptions(new PublicWriteAccessClassOptions()));
        }
        catch (Exception ex)
        {
            //doesn't work but do offline save anyway
        }

        //Offline save
        PlayerPrefs.SetString("time", timeNow);
        PlayerPrefs.SetString("playerName", Player_Name);
        PlayerPrefs.SetInt("level", Level);
        PlayerPrefs.SetFloat("currentEXP", Current_EXP);
        PlayerPrefs.SetFloat("maxEXP", Max_EXP);
        PlayerPrefs.SetString("uniqueBirds", uniqueBirds);
        PlayerPrefs.SetString("totalAchievements", totalAchievements);
    }

    public void Offline_Save()
    {
        string uniqueBirds = string.Join(",", Unique_Birds_Caught);
        string totalAchievements = string.Join(",", Achievements);

        PlayerPrefs.SetString("time", System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss,fff"));
        PlayerPrefs.SetString("playerName", Player_Name);
        PlayerPrefs.SetInt("level", Level);
        PlayerPrefs.SetFloat("currentEXP", Current_EXP);
        PlayerPrefs.SetFloat("maxEXP", Max_EXP);
        PlayerPrefs.SetString("uniqueBirds", uniqueBirds);
        PlayerPrefs.SetString("totalAchievements", totalAchievements);
    }

    public async void Load_Data()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {
          "time", "playerName", "level", "currentEXP", "maxEXP", "uniqueBirds", "totalAchievements"
        }, new LoadOptions(new PublicReadAccessClassOptions()));

        if(playerData.TryGetValue("time", out var tim) && PlayerPrefs.HasKey("time"))
        {
            DateTime prefTime = DateTime.ParseExact(PlayerPrefs.GetString("time"), "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture);
            DateTime cloudTime = DateTime.ParseExact(tim.Value.GetAs<string>(), "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture);
            int result = DateTime.Compare(cloudTime, prefTime);
            if(result < 0) // cloudtime is earlier than preftime
            {
                Offline_Load();
                return;
            }
        }

        if (playerData.TryGetValue("level", out var lvl))
        {
            Level = lvl.Value.GetAs<int>();
        }

        if (playerData.TryGetValue("currentEXP", out var cur))
        {
            Current_EXP = cur.Value.GetAs<float>();
        }

        if (playerData.TryGetValue("maxEXP", out var mx))
        {
            Max_EXP = mx.Value.GetAs<float>();
        }

        if (playerData.TryGetValue("uniqueBirds", out var uni))
        {
            var separate = uni.Value.GetAs<string>().Split(',');
            Debug.Log("Separate: " + separate);
            foreach(var bird in separate)
            {
                if(!string.IsNullOrEmpty(bird)) { Unique_Birds_Caught.Add(bird); }
            }
            Unique_Birds_Caught = new List<string>(tempBirds);

            // var separate = uni.Value.GetAs<string>().Split(',');
            // foreach(var bird in separate)
            // {
            //     Unique_Birds_Caught.Add(bird);
            // }
        }

        if (playerData.TryGetValue("totalAchievements", out var ttl))
        {
            HashSet<string> tempAch = new HashSet<string>(Achievements);
            var separate = ttl.Value.GetAs<string>().Split(',');
            foreach (var ach in separate)
            {
                if (!string.IsNullOrEmpty(ach)) { Achievements.Add(ach); }
            }
            Achievements = new List<string>(tempAch);
        }
    }

    public void Offline_Load()
    {
        Player_Name = PlayerPrefs.GetString("playerName");
        Level = PlayerPrefs.GetInt("level");
        Current_EXP = PlayerPrefs.GetFloat("currentEXP");
        Max_EXP = PlayerPrefs.GetFloat("maxEXP");

        HashSet<string> tempBirds = new HashSet<string>(Unique_Birds_Caught);
        var separate = PlayerPrefs.GetString("uniqueBirds").Split(',');
        foreach(var bird in separate)
        {
            tempBirds.Add(bird);
        }
        Unique_Birds_Caught = new List<string>(tempBirds);

        HashSet<string> tempAch = new HashSet<string>(Achievements);
        separate = PlayerPrefs.GetString("totalAchievements").Split(',');
        foreach(var ach in separate)
        {
            tempAch.Add(ach);
        }
        Achievements = new List<string>(tempAch);
    }

    private void OnApplicationPause(bool pause)
    {
        Save_Data();
    }

    private void OnApplicationQuit()
    {
        Save_Data();
    }
}
