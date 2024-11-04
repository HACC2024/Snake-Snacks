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
    private bool Spawned = false;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        anchorManager = GetComponent<ARAnchorManager>();

        //planeManager.planesChanged += PlanesChanged;

        Spawn_Bird();
    }

    //void PlanesChanged(ARPlanesChangedEventArgs args)
    //{
    //    foreach(var plane in args.added)
    //    {
    //        if(!Spawned)
    //        {
    //            Spawn_Bird(plane);
    //        }
            
    //    }
    //}

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
        //Anchors.Add(bird.GetComponent<ARAnchor>());
        //Vector3 position = arCamera.ScreenToWorldPoint(Vector3.zero);

        ////public ARAnchor AttachAnchor(ARPlane plane, Pose pose)
        ////ARAnchor anchor = anchorManager.AddAnchor(new Pose(position, Quaternion.identity));
        //ARAnchor anchor = anchorManager.AttachAnchor(plane, new Pose(position, Quaternion.identity));
        //Anchors.Add(anchor);

        //GameObject go = Instantiate(Bird_Prefab);
        //go.transform.parent = anchorManager.transform ?? transform;
        //go.transform.position = position;
        //Spawned = true;
    }
}
