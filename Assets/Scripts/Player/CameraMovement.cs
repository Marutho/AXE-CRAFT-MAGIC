using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float minX = -60;
    public float maxX = 60;
    public float minY = -360;
    public float maxY = 360;

    public float dpi;

    public new GameObject camera,pause,mainCanvas;

    private float rotY = 0;
    private float rotX = 0;
    private bool paused;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        Time.timeScale = 1;
    }
    
    void Update()
    {
        if (!paused)
        {
            Cursor.visible = false;

            rotY += Input.GetAxis("Mouse X") * dpi;
            rotX += Input.GetAxis("Mouse Y") * dpi;

            rotX = Mathf.Clamp(rotX, minX, maxX);

            transform.localEulerAngles = new Vector3(0, rotY, 0);
            camera.transform.localEulerAngles = new Vector3(-rotX, rotY, 0);

            camera.transform.position = transform.position;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Play();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        paused = true;
        mainCanvas.SetActive(false);
    }

    public void Play()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        mainCanvas.SetActive(true);
    }
}
