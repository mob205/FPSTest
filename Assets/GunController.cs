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
    [SerializeField] GameObject shooter;

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
    }

    public void Fire()
    {
        if(!canFire) { return; }
        ShootRay();
        ShootBullet();
        FireParticles();
        PlayShootSound();
        ToggleFire();
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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            Debug.Log("Hit something");
            Destroy(hit.collider.gameObject);
        }
        else
        {
            Debug.Log("Did not hit something");
            Debug.DrawRay(shooter.transform.position, transform.forward * 200, Color.white, 200);
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
