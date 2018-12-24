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

    private void ShootBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
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
