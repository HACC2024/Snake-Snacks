using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AvidexDisplay : MonoBehaviour
{
    public Avidex avidex;
    public TMP_Text name;
    public Image image;

    public event Action<Avidex> OnEntrySelected;

    // Start is called before the first frame update
    public void SetBirdData(Avidex birdData)
    {
        avidex = birdData;
        name.text = avidex.Name;
        image.sprite = avidex.Photo;

        Color gray = new Color(0.5f, 0.5f, 0.5f, 1f);
        if (!avidex.Found)
            image.color = gray;
        else
            image.color = Color.white;
    }

    public void OnBirdEntryClicked()
    {
        OnEntrySelected?.Invoke(avidex);
    }
}
