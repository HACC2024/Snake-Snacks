using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Save : MonoBehaviour
{
    //name (string)
    //Birds captured (int)
    //Birds found (string of birds separated by commas); i.e. bird1,bird2,bird3
    //EXP (float)
    private void SaveProgress()
    {
        //PlayerPrefs.SetInt("Captured", 0);
        //PlayerPrefs.SetFloat("EXP", 0);
        //PlayerPrefs.SetString("Birds", "");
        //string saveValue = "your JSON string";
        //string loadValue = PlayerPrefs.GetString(saveKey);
        //if (!saveValue.Equals(loadValue))
        //{
        //    PlayerPrefs.SetString(saveKey, saveValue);
        //    PlayerPrefs.Save();
        //}
    }

    public void Initialize_Save(string name)
    {
        PlayerPrefs.SetString("Name", name);
    }
}
