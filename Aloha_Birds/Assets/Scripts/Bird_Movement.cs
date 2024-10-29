using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bird_Movement : MonoBehaviour
{
    [SerializeField] GameObject _bird;
    [SerializeField] bool Circulate = false;
    [SerializeField] bool ExtendRetract = false;
    [SerializeField] bool Jumping = false;
    [SerializeField] GameObject Circulate_Center;

    //[SerializeField] private Renderer m_Renderer;
    //[SerializeField] private Camera cam;
    //[SerializeField] private Plane[] planes;
    [SerializeField] private Collider objCollider;

    //[SerializeField] private TMP_Text Debug_Logger;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Circulate());
        //Different_Location_Procedure();
        //cam = Camera.main;
        //planes = GeometryUtility.CalculateFrustumPlanes(cam);
        GameObject.Find("Circulate").gameObject.
            GetComponent<Button>().onClick.AddListener(
            () =>
            {
                ExtendRetract = !ExtendRetract;
                Circulate = !Circulate;
            }
            );
        GameObject.Find("Reposition").gameObject.
            GetComponent<Button>().onClick.AddListener(
            () => Different_Location_Procedure());
        //Debug_Logger = GameObject.Find("Debug_Finding").gameObject.GetComponent<TMP_Text>();
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

        //if (m_Renderer.isVisible)
        //{
        //    Debug.Log("Bird is visible");
        //}
        //else Debug.Log("Bird is no longer visible");
        //if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
        //{
        //    Debug_Logger.text = _bird.name + " has been detected!";
        //    //Debug.Log(_bird.name + " has been detected!");
        //}
        //else
        //{
        //    Debug_Logger.text = "Nothing has been detected";
        //    //Debug.Log("Nothing has been detected");
        //}
        //planes = GeometryUtility.CalculateFrustumPlanes(cam);
    }

    public void Different_Location_Procedure()
    {
        if (Circulate || ExtendRetract)
        {
            Circulate = false;
            ExtendRetract = false;
            if(_bird.transform.position != new Vector3(0, 0, 0)) { Return_To_Center(); }
        }

        var loc = new Vector3(Random.Range(-10, 10), Random.Range(1, 10), Random.Range(-10, 10));

        this.transform.DOLocalJump(loc, 1, 1, 3);
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

    //IEnumerator Circulate()
    //{
    //    _bird.transform.DOMoveX(2, 5, false);
    //    yield return new WaitForSeconds(5);
    //    this.transform.DORotate(new Vector3(0, 180, 0), 10);
    //    yield return new WaitForSeconds(10);
    //    this.transform.DORotate(new Vector3(0, 360, 0), 10);
    //}
}
