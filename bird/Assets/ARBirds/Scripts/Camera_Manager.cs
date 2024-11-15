using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class Camera_Manager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Plane[] planes;
    [SerializeField] public Collider objCollider;
    //[SerializeField] private TMP_Text Debug_Logger;
    [SerializeField] private float Score = 0;
    [SerializeField] private float Timer = 30;
    [SerializeField] private GameObject After_Snap;
    [SerializeField] private Image Captured_Image;
    [SerializeField] private TMP_Text Timer_Text;
    [SerializeField] private Image Fade;
    [SerializeField] private GameObject[] Sequence;
    [SerializeField] private int Sequence_Index = 0;
    [SerializeField] private InputAction Tap;
    [SerializeField] private TMP_InputField Guess;
    public string Bird_Name;
    [SerializeField] private TMP_Text Display_EXPScore;
    [SerializeField] bool OnOff = false;
    private void Start()
    {
        cam = Camera.main;
        Tap.Enable();
        OnOff = true;
    }

    public void Update()
    {
        if (OnOff)
        {
            Timer -= Time.deltaTime;
            Timer_Text.text = Timer.ToString("0.00");
        }

        if(Timer <= 0)
        {
            Load_GPS();
            //Escaped situation
        }
    }


    public void TakePhoto()
    {
        StartCoroutine(FadeImage());
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
        {
            var dis = (this.gameObject.transform.position - objCollider.gameObject.transform.position).magnitude;
            Score += 1000 / dis;
            Score += 1000;
            if(Timer > 0) { Score += 1000 / (30 - Timer); }
            //Debug_Logger.text = objCollider.gameObject.name + " has been detected!\n Total Score: " + Score;
            //Debug.Log(_bird.name + " has been detected!");
            //Point breakdown:
            //Distance = 1000 / distance
            //Onscreen = 1000
            //Timed = 1000 / elapsed
            //Correct bird ID = 1000

            OnOff = false;
            Timer_Text.gameObject.SetActive(false);
            StartCoroutine(Capture_Screen());
            Sequence[Sequence_Index].gameObject.SetActive(true);
            StartCoroutine(Tap_Next());
            //Display image
        }
        else
        {
            //Try again
            //Debug_Logger.text = "Nothing has been detected\nTotal Score: " + Score;
            //StartCoroutine(Capture_Screen());
            //Debug.Log("Nothing has been detected");
        }
    }

    IEnumerator Capture_Screen()
    {
        yield return new WaitForEndOfFrame();
        Camera camera = Camera.main;
        int width = Screen.width;
        int height = Screen.height;
        RenderTexture rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = rt;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();
        After_Snap.gameObject.SetActive(true);
        Captured_Image.sprite = Sprite.Create(image, new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), new Vector2(0, 0));
        Captured_Image.color = new Color(1, 1, 1, 1);

        camera.targetTexture = null;

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        //byte[] bytes = image.EncodeToPNG();
        //Destroy(rt);
    }

    IEnumerator FadeImage()
    {
        Fade.gameObject.SetActive(true);
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            Fade.color = new Color(1, 1, 1, i);
            yield return null;
        }

        Fade.color = new Color(1, 1, 1, 1);
        Fade.gameObject.SetActive(false);
    }

    public void Save_Image()
    {
        byte[] bytes = Captured_Image.sprite.texture.EncodeToPNG();
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(filePath);

        File.WriteAllBytes(filePath, bytes);

        Destroy(Captured_Image);
    }

    IEnumerator Tap_Next()
    {
        yield return new WaitForSeconds(1.0f);
        yield return new WaitUntil(() => Tap.triggered);
        Sequence[Sequence_Index].gameObject.SetActive(false);
        Sequence_Index++;
        Sequence[Sequence_Index].gameObject.SetActive(true);
    }

    public void Confirm_Guess()
    {
        if(Guess.text.Equals(Bird_Name)) { Score += 1000; }
        Sequence[Sequence_Index].gameObject.SetActive(false);
        Sequence_Index++;
        Sequence[Sequence_Index].gameObject.SetActive(true);

        Display_EXPScore.text = "Score: " + Score + "  EXP: " + Score / 1000;
        //GameObject.FindObjectOfType<Player_Information>().Add_EXP(Score);
        //GameObject.FindObjectOfType<Player_Information>().Add_Bird(Bird_Name);
        StartCoroutine(Tap_Next());
    }

    public void Load_GPS()
    {
        SceneManager.LoadScene("GPSTesting", LoadSceneMode.Single);
    }
}
