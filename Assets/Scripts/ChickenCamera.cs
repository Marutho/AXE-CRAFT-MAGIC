using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCamera : MonoBehaviour
{
    public float minX = -60;
    public float maxX = 60;
    public float minY = -360;
    public float maxY = 360;

    public Vector3 vector;

    public float dpi = 1;

    //public new GameObject camera;

    private float rotY = 0;
    private float rotX = 0;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        rotY += Input.GetAxis("Mouse X") * dpi;
        rotX += Input.GetAxis("Mouse Y") * dpi;

        rotX = Mathf.Clamp(rotX, minX, maxX);

        transform.localEulerAngles = new Vector3(0, rotY, 0);
        //camera.transform.localEulerAngles = new Vector3(-rotX, rotY, 0);

        //camera.transform.position = transform.position + vector;
    }
}
