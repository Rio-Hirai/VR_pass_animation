using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class recorder : MonoBehaviour
{
    public int framerate = 30;
    public int superSize;
    public bool autoRecord;
    int frameCount;
    bool recording;

    public Camera eyeCamera;
    private Texture2D texture;
    private int photoNumber = 1;

    void Start()
    {
        texture = new Texture2D(eyeCamera.targetTexture.width, eyeCamera.targetTexture.height, TextureFormat.RGB24, false);
        if (autoRecord) StartRecording();
    }

    void StartRecording()
    {
        Time.captureFramerate = framerate;
        frameCount = -1;
        recording = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("joystick button 17") && recording == true)
        {
            recording = false;
            enabled = false;
        } else if (Input.GetKeyDown("joystick button 17"))
        {
            StartRecording();
        }

        if (recording)
        {
            if (Input.GetMouseButtonDown(0))
            {
                recording = false;
                enabled = false;
            }
            else
            {
                if (frameCount > 0)
                {
                    SaveCameraImage();
                }

                frameCount++;

                if (frameCount > 0 && frameCount % 60 == 0)
                {
                    Debug.Log((frameCount / 60).ToString() + " seconds elapsed.");
                }
            }
        }

    }

    public void SaveCameraImage()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = eyeCamera.targetTexture;
        eyeCamera.Render();
        texture.ReadPixels(new Rect(0, 0, eyeCamera.targetTexture.width, eyeCamera.targetTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = currentRT;
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes("D:/Documents/projects_for_VR/S1225/Capture/Hoge" + photoNumber + ".png", bytes);
        photoNumber++;
    }

    void OnGUI()
    {
        if (!recording && GUI.Button(new Rect(0, 0, 200, 50), "Start Recording"))
        {
            StartRecording();
            Debug.Log("Click Game View to stop recording.");
        }
    }
}
