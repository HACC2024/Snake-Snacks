using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapBirdObj : MonoBehaviour
{
    public Avidex birdEntry;
    public BirdSelectionManager bsm;
    private Scene scene;
    // private float jumpForce = 3f;
    // private float minInterval = 1f;
    // private float maxInterval = 5f;
    // private Rigidbody rb;

    // // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        // StartCoroutine(IdleAnim());
        scene = SceneManager.GetActiveScene();
    }

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
        if(scene.name == "GPSTesting")
        {
            birdEntry.Found = true;
            var PlayerInfo = GameObject.Find("--------Player Information---------").GetComponent<Player_Information>();
            PlayerInfo.Add_Bird(birdEntry.Name);
            bsm.selectedBird = birdEntry;
            SceneManager.LoadScene("AR_Bird");
        }
        
    }




    
}
