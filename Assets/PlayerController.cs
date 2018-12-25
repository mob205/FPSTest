using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float speed = 10;

    GunController gun;
    Camera playerCamera;
	// Use this for initialization
	void Start () {
        gun = GetComponentInChildren<GunController>();
        playerCamera = FindObjectOfType<Camera>();
	}

    // Update is called once per frame
    void Update()
    {
        ProcessFire();
        ProcessMovement();
        ResetRotation();
    }
    void ProcessFire()
    {
        if (Input.GetMouseButton(0))
        {
            gun.Fire();
        }
    }
    void ProcessMovement()
    {
        float mSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        { 
            mSpeed = mSpeed * 2;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Debug.Log(mSpeed);

        transform.position += (transform.forward * vertical * mSpeed * Time.deltaTime);
        transform.position += (transform.right * horizontal * mSpeed * Time.deltaTime);
    }
    void ResetRotation()
    {
        Quaternion cameraRotation = playerCamera.gameObject.transform.rotation;
        Quaternion rot = transform.rotation;
        rot = Quaternion.Euler(0, cameraRotation.eulerAngles.y, cameraRotation.eulerAngles.z);
        transform.rotation = rot;
    }
}
