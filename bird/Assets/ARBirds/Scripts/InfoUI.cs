using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    public GameObject infoPrefab;
    private bool toggle = true;

    void Start()
    {
        ToggleUI();
    }

    public void ToggleUI()
    {
        toggle = !toggle;
        infoPrefab.SetActive(toggle);
    }

}
