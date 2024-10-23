using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bird", menuName = "Bird")]
public class Avidex : ScriptableObject
{
    public string Name;
    public Sprite Photo;
    public Conservation_Status Status;
    public string Hawaiian_Name;
    public string Species_Description;
    public bool Native;
}

public enum Conservation_Status
{
    LC, // Least Concern
    NT, // Near Threatened
    VU, // Vulnerable
    EN // Endangered
}