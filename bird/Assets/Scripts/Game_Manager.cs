using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private float Timer = 30;
    [SerializeField] private TextMeshPro Timer_Text;

    private void Update()
    {
        Timer -= Time.deltaTime;
        Timer_Text.text = Timer.ToString();
    }
}
