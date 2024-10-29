using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Camera_Manager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Plane[] planes;
    [SerializeField] public Collider objCollider;
    [SerializeField] private TMP_Text Debug_Logger;
    [SerializeField] private float Score = 0;
    [SerializeField] private float Timer = 30;
    private void Start()
    {
        cam = Camera.main;
    }

    public void Update()
    {
        Timer -= Time.deltaTime;
    }


    public void TakePhoto()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
        {
            var dis = (this.gameObject.transform.position - objCollider.gameObject.transform.position).magnitude;
            Score += 1000 / dis;
            Score += 1000;
            Score += 1000 / (30 - Timer);
            Debug_Logger.text = objCollider.gameObject.name + " has been detected!\n Total Score: " + Score;
            //Debug.Log(_bird.name + " has been detected!");
            //Point breakdown:
                //Distance = 1000 / distance
                //Onscreen = 1000
                //Timed = 1000 / elapsed
                //Correct bird ID = 1000
        }
        else
        {
            Debug_Logger.text = "Nothing has been detected\nTotal Score: " + Score;
            //Debug.Log("Nothing has been detected");
        }
        Time.timeScale = 0;
    }

    public void Save_To_Camera_Roll()
    {
        StartCoroutine(Capture_Screen());
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

        camera.targetTexture = null;

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        byte[] bytes = image.EncodeToPNG();
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        File.WriteAllBytes(filePath, bytes);

        Destroy(rt);
        Destroy(image);
    }
}
