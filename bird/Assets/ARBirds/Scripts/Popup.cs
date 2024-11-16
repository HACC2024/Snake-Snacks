using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Image Photo;
    public TMP_Text Title;
    public TMP_Text Description;
    public void Info_Dump(Sprite photo, string title, string description)
    {
        Photo.sprite = photo;
        Title.text = title;
        Description.text = description;
    }

    public void Exit_Popup()
    {
        Destroy(this.gameObject);
    }
}
