using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign_In_Toggle : MonoBehaviour
{
    [SerializeField] private GameObject Sign_In_Path;
    [SerializeField] private GameObject Sign_Up_Path;
    [SerializeField] private Button SU_Button;
    [SerializeField] private Button SI_Button;


    public void Sign_Up_Button()
    {
        Sign_Up_Path.gameObject.SetActive(true);
        Sign_In_Path.gameObject.SetActive(false);
        SU_Button.interactable = false;
        SI_Button.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        SU_Button.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        SI_Button.interactable = true;
    }

    public void Sign_In_Button()
    {
        Sign_Up_Path.gameObject.SetActive(false);
        Sign_In_Path.gameObject.SetActive(true);
        SI_Button.interactable = false;
        SU_Button.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        SI_Button.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        SU_Button.interactable = true;
    }
}
