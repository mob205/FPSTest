using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    [Header("General")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] ParticleSystem gunFire;
    [SerializeField] AudioSource gunAudio;

    [Header("Gun Stats")]
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float timeBetweenFire = 0.5f;
    [SerializeField] float bulletLifetime = 5f;

    bool canFire = true;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Fire();
    }

    void Fire()
    {
        if(!canFire) { return; }
        if (Input.GetMouseButton(0))
        {
            ShootRay();
            ShootBullet();
            FireParticles();
            PlayShootSound();
            ToggleFire();
        }
    }
    private void ToggleFire()
    {
        canFire = false;
        StartCoroutine("EnableFire");
    }
    private void PlayShootSound()
    {
        gunAudio.Play();
    }
    private void ShootRay()
    {
        RaycastHit hit;
        Debug.Log(transform.position);
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Hit something");
            Destroy(hit.collider.gameObject);
        }
        else
        {
            Debug.Log("Did not hit something");
            Debug.DrawRay(transform.position, transform.forward * 200, Color.white, 200);
        }
    }
    private void ShootBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
        Destroy(bullet, bulletLifetime);
    }
    private void FireParticles()
    {
        gunFire.Play();
    }
    IEnumerator EnableFire()
    {
        yield return new WaitForSeconds(timeBetweenFire);
        canFire = true;
    }
}
