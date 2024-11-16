using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achievement")]
public class Achievement : ScriptableObject
{
    public string AchievementID;
    public string Title;
    public string Description;
    public bool Unlocked;
}
