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
    public BirdSelectionManager bsm;
    public GameObject BirdPrefab;
    [SerializeField] private ARPlaneManager planeManager;
    public List<ARAnchor> Anchors = new List<ARAnchor>();

    private ARAnchorManager anchorManager;
    [SerializeField] private Camera arCamera;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        anchorManager = GetComponent<ARAnchorManager>();
        if(bsm.selectedBird != null)
        {
            BirdPrefab = bsm.selectedBird.birdPrefab;
            Spawn_Bird(BirdPrefab);
        }
        

    }

    public void Spawn_Bird(GameObject BirdObj)
    {
        var bird = Instantiate(BirdObj, new Vector3(0, 5, 0), Quaternion.identity);
        bird.AddComponent<ARAnchor>();
        if(!bird.GetComponent<Bird_Movement>())
        {
            bird.AddComponent<Bird_Movement>();
        }
        bird.GetComponent<Bird_Movement>().Target_Position(new Vector3(
            Random.Range(-3, 3),
            Random.Range(-3, 3),
            Random.Range(-3, 3)));
        bird.GetComponent<Bird_Movement>().Circulate = true;
        Camera.main.GetComponent<Camera_Manager>().objCollider = bird.GetComponentInChildren<Collider>();
        Camera.main.GetComponent<Camera_Manager>().Bird_Name = bsm.selectedBird.Name;
    }
}
