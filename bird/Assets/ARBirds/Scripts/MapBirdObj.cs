using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapBirdObj : MonoBehaviour
{
    public Avidex birdEntry;
    public BirdSelectionManager bsm;

    // private float jumpForce = 3f;
    // private float minInterval = 1f;
    // private float maxInterval = 5f;
    // private Rigidbody rb;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    //     StartCoroutine(IdleAnim());
    // }

    // //Jumps at random time intervals
    // IEnumerator IdleAnim()
    // {
    //     while (true)
    //     {
    //         // Wait for a random interval before performing the next jump
    //         float waitTime = Random.Range(minInterval, maxInterval);
    //         yield return new WaitForSeconds(waitTime);

    //         // Apply jump force
    //         if (rb != null)
    //         {
    //             rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //         }
    //     }
    // }

    void OnMouseDown()
    {
        birdEntry.Found = true;
        bsm.selectedBird = birdEntry;
        SceneManager.LoadScene("AR_Bird");
    }




    
}
