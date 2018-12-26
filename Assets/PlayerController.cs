﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField] float speed = 10f;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] Transform ADSLocation;
    [SerializeField] Transform hipLocation;
    [SerializeField] float hipAimDeviation = 0.1f;
    [SerializeField] Text ammoDisplay;

    GunController gun;
    Camera playerCamera;
    bool isGrounded;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        gun = GetComponentInChildren<GunController>();
        playerCamera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
	}
 
    // Update is called once per frame
    void Update()
    {
        ProcessGunInput();
        ProcessMovement();
        ResetRotation();
        ProcessForce();
        AimDownSights();
        UpdateAmmoDisplay();
    }
    private void FixedUpdate()
    {
        Jump();
    }
    private void UpdateAmmoDisplay()
    {
        ammoDisplay.text = gun.GetAmmoCount().ToString();
    }
    void ProcessForce()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }
    void ProcessGunInput()
    {
        if (Input.GetMouseButton(0))
        {
            gun.Fire();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.StartCoroutine("Reload");
        }
    }
    void AimDownSights()
    {
        if (Input.GetMouseButton(1))
        {
            gun.transform.position = ADSLocation.transform.position;
            gun.transform.rotation = ADSLocation.transform.rotation;
            gun.aimDeviation = 0f;
        }
        else
        {
            gun.transform.position = hipLocation.transform.position;
            gun.transform.rotation = hipLocation.transform.rotation;
            gun.aimDeviation = hipAimDeviation;
        }
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Space pressed");
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
           //transform.position += (transform.up * jumpHeight);
            isGrounded = false;
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

    private void OnCollisionStay()
    {
        isGrounded = true;
    }
}
