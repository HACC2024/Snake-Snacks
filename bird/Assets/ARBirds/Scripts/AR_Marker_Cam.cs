using Mapbox.Map;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class AR_Marker_Cam : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Plane[] planes;
    [SerializeField] public Collider objCollider;
    [SerializeField] private GameObject After_Snap;
    [SerializeField] private Image Captured_Image;
    [SerializeField] private Image Fade;
    [SerializeField] private GameObject[] Sequence;
    [SerializeField] private int Sequence_Index = 0;
    [SerializeField] private InputAction Tap;
    private void Start()
    {
        cam = Camera.main;
        Tap.Enable();
    }
    public void TakePhoto()
    {
        StartCoroutine(FadeImage());
        StartCoroutine(Capture_Screen());
        Sequence[Sequence_Index].gameObject.SetActive(true);
        StartCoroutine(Tap_Next());
    }

    IEnumerator Tap_Next()
    {
        yield return new WaitForSeconds(1.0f);
        yield return new WaitUntil(() => Tap.triggered);
        Sequence[Sequence_Index].gameObject.SetActive(false);
        Sequence_Index++;
        Sequence[Sequence_Index].gameObject.SetActive(true);
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
        Texture2D ss = Captured_Image.sprite.texture;
        ss.ReadPixels(Captured_Image.sprite.rect, 0, 0);
        ss.Apply();

        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, Application.productName + " Captures", fileName));
    }
}
