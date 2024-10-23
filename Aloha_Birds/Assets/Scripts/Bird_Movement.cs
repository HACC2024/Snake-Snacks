using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Movement : MonoBehaviour
{
    [SerializeField] GameObject _bird;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Circulate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Circulate()
    {
        _bird.transform.DOMoveX(2, 5, true);
        yield return new WaitForSeconds(5);
        this.transform.DORotate(new Vector3(0, 180, 0), 10);
    }
}
