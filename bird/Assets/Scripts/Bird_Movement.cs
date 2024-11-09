using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bird_Movement : MonoBehaviour
{
    [SerializeField] GameObject _bird;
    [SerializeField] public bool Circulate = false;
    [SerializeField] bool ExtendRetract = false;
    [SerializeField] bool Jumping = false;
    [SerializeField] GameObject Circulate_Center;
    [SerializeField] private Collider objCollider;
    void Awake()
    {
        //GameObject.Find("Circulate").gameObject.
        //    GetComponent<Button>().onClick.AddListener(
        //    () =>
        //    {
        //        ExtendRetract = !ExtendRetract;
        //        Circulate = !Circulate;
        //    }
        //    );
        //GameObject.Find("Reposition").gameObject.
        //    GetComponent<Button>().onClick.AddListener(
        //    () => Different_Location_Procedure());
        GameObject.Find("Main Camera").GetComponent<Camera_Manager>().objCollider = objCollider;
    }

    private void Update()
    {
        if (ExtendRetract) { StartCoroutine(Extend_From_Center()); }
        if (Circulate) 
        {
            float smooth = 100.0f;
            Circulate_Center.transform.RotateAround(
                Circulate_Center.transform.position, Vector3.up, Time.deltaTime * smooth);
        }

        if(Jumping) 
        {
            Jumping = false;
            Different_Location_Procedure();
        }
    }

    public void Different_Location_Procedure()
    {
        if (Circulate || ExtendRetract)
        {
            Circulate = false;
            ExtendRetract = false;
            if(_bird.transform.position != new Vector3(0, 0, 0)) { Return_To_Center(); }
        }

        var cam = Camera.main.transform.position;
        var loc = new Vector3(
            Random.Range(cam.x - 5, cam.x + 5), 
            Random.Range(cam.y - 1, cam.y + 2), 
            Random.Range(cam.z - 5, cam.z + 5));

        Circulate_Center.transform.DOJump(loc, 1, 1, 3);

        Invoke("Different_Location_Procedure", Random.Range(5.0f, 10.0f));
        //Circulate_Center.transform.DOLocalJump(loc, 1, 1, 3);
    }

    public void Target_Position(Vector3 target)
    {
        Circulate_Center.transform.DOJump(target, 1, 1, 5);

        Invoke("Different_Location_Procedure", Random.Range(5.0f, 10.0f));
    }

    IEnumerator Extend_From_Center()
    {
        _bird.transform.DOLocalMoveX(2, 2, false);
        yield return new WaitForSeconds(5);
        Circulate = true;
    }

    IEnumerator Return_To_Center()
    {
        _bird.transform.DOLocalMove(new Vector3(0, 0, 0), 2, false);
        yield return new WaitForSeconds(5);
        Circulate = false;
    }
}
