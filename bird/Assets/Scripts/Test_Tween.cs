using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test_Tween : MonoBehaviour
{
    [SerializeField] private GameObject Test1;
    [SerializeField] private GameObject Test2;
    public void Stop_Time()
    {
        Time.timeScale = 0;
    }

    public void Resume_Time()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        Test1.gameObject.transform.Rotate(Vector3.up * 60 * Time.deltaTime);
        Test2.gameObject.transform.Rotate(Vector3.up * 60 * Time.unscaledDeltaTime);
    }
}
