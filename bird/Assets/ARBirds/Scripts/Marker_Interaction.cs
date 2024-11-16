using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker_Interaction : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private float Speed;
    public Marker_Obj Event_Details;
    [SerializeField] private GameObject Popup_Prefab;
    void OnMouseDown()
    {
        var pop = Instantiate(Popup_Prefab);
        pop.GetComponent<Popup>().Info_Dump(Event_Details.Photo, Event_Details.Title, Event_Details.Description);
    }

    private void Update()
    {
        _model.gameObject.transform.Rotate(Vector3.forward, Speed * Time.deltaTime);
    }
}
