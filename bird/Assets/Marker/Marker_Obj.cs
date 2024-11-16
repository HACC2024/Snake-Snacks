using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Marker", menuName = "Marker")]
public class Marker_Obj : ScriptableObject
{
    public Sprite Photo;
    public string Title;
    public string Description;
    public string LatLong;
}
