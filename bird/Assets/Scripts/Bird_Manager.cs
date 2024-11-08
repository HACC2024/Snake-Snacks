using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ARPlaneManager))]
public class Bird_Manager : MonoBehaviour
{
    public XROrigin XR_Origin;
    public GameObject Bird_Prefab;
    [SerializeField] private ARPlaneManager planeManager;
    public List<ARAnchor> Anchors = new List<ARAnchor>();

    private ARAnchorManager anchorManager;
    [SerializeField] private Camera arCamera;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        anchorManager = GetComponent<ARAnchorManager>();

        Spawn_Bird();
    }

    public void Spawn_Bird()
    {
        var bird = Instantiate(Bird_Prefab, new Vector3(0, 5, 0), Quaternion.identity);
        bird.AddComponent<ARAnchor>();
        bird.GetComponent<Bird_Movement>().Target_Position(new Vector3(
            Random.Range(-3, 3),
            Random.Range(-3, 3),
            Random.Range(-3, 3)));
        bird.GetComponent<Bird_Movement>().Circulate = true;
        Camera.main.GetComponent<Camera_Manager>().objCollider = bird.GetComponentInChildren<Collider>();
        Camera.main.GetComponent<Camera_Manager>().Bird_Name = bird.name;
    }
}
