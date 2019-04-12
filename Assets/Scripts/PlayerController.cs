using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour {


    [Header("General")]
    [SerializeField] Transform ADSLocation;
    [SerializeField] Transform hipLocation;
    [SerializeField] TextMeshProUGUI ammoDisplay;
    [SerializeField] Slider healthDisplay;
    [SerializeField] Slider staminaDisplay;
    [Header("Stats")]
    [SerializeField] int maxStamina = 100;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int staminaUsage = 25;
    [SerializeField] int staminaRegen = 15;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float hipAimDeviation = 0.1f;
    [SerializeField] float sprintSpeedMultiplier = 2f;

    float stamina;
    float health;
    GunController gun;
    Camera playerCamera;
    bool isGrounded;
    bool isSprinting;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        gun = GetComponentInChildren<GunController>();
        playerCamera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        stamina = maxStamina;
	}
 
    // Update is called once per frame
    void Update()
    {
        ProcessGunInput();
        ResetRotation();
        AimDownSights();
        UpdateAmmoDisplay();
        UpdateHealthDisplay();
        UpdateStaminaDisplay();
        ProcessSprint();
    }
    private void FixedUpdate()
    {
        Jump();
        ProcessMovement();
        LimitVelocity();
    }
    private void UpdateAmmoDisplay()
    {
        ammoDisplay.text = gun.GetAmmoCount().ToString() + " / " + gun.magSize;
        if(gun.CheckReloadStatus()) {
            ammoDisplay.text += " Reloading...";
        }
    }
    private void UpdateHealthDisplay()
    {
        healthDisplay.value = (float)health / (float)maxHealth;
    }
    private void UpdateStaminaDisplay()
    {
        staminaDisplay.value = (float)stamina / (float)maxStamina;
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
    void Damage(int damage)
    {
        health -= damage;
    }
    public void Heal(int heal)
    {
        health = Mathf.Clamp(health + heal, 0, maxHealth);
    }
    void ProcessSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            isSprinting = true;
            stamina -= staminaUsage * Time.deltaTime;
        }
        else
        {
            isSprinting = false;
            stamina += staminaRegen * Time.deltaTime;
        }
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
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

    void LimitVelocity()
    {
        //To fix glitch where player may be launched upward when colliding with low walls
        rb.velocity = new Vector3(0, Mathf.Clamp(rb.velocity.y, -15, jumpHeight * 2), 0);
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && stamina > staminaUsage)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
            stamina -= staminaUsage / 2;
        }
    }
    void ProcessMovement()
    {
        float mSpeed = speed;
        if (isSprinting & stamina > 0) 
        { 
            mSpeed = mSpeed * sprintSpeedMultiplier;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(0, 0, 0);
        movement += transform.forward * vertical;
        movement += transform.right * horizontal;
        movement.Normalize();
        transform.position += movement * mSpeed * Time.deltaTime;
        
    }
    void ResetRotation()
    {
        Quaternion cameraRotation = playerCamera.gameObject.transform.rotation;
        Quaternion rot = transform.rotation;
        rot = Quaternion.Euler(0, cameraRotation.eulerAngles.y, 0);
        transform.rotation = rot;
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }

    public void ProcessRayHit(int damage)
    {
        Damage(damage);
    }
   
    
}
