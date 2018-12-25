using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float clampAngle = 80f;

    float rotX;
    float rotY;
    
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
	}
	
	// Update is called once per frame
	void Update () {
        FollowMouse();
	}
    void FollowMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

       rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
