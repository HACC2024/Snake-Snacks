using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test_Tween : MonoBehaviour
{
    [SerializeField] private Transform Cube;
    [SerializeField] private float _cycleLength = 6;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(new Vector3(6, 6, 6), _cycleLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
