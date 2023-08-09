using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LockCam : MonoBehaviour
{
    Player player;
    Camera cam;

    public float lookSensitivity = 180.0f;
    private float currentCamRotX;
    private float rotY = 0.0f;
    //private float rotX = 0.0f;
    private float rotationX = 0.0f;
    private Quaternion ion;

    private Vector2 limtCam = new Vector2(-20.0f, 30.0f);

    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.LockZoom += PositianingCamera;
        cam = GetComponentInChildren<Camera>();
        cam.gameObject.SetActive(false);
        ion = transform.rotation;
    }


    private void Update()
    {
        if(player.setup == true)
        {
            TargettingCamera();
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
    }

    private void TargettingCamera()
    {
        rotY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
        //rotX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        rotationX -= rotY;
        rotationX = Mathf.Clamp(rotationX, -30, 30);
        transform.localRotation = Quaternion.Euler(rotationX, -90, 0);
    }

    private void PositianingCamera()
    {
        if(player.setup == true)
        {
            cam.gameObject.SetActive(true);
        }
        else
        {
            cam.gameObject.SetActive(false);
        }
    }
}
