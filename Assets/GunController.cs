using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    [Header("General")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] ParticleSystem gunFire;

    [Header("Gun Stats")]
    [SerializeField] float bulletSpeed = 10f;
    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Fire();
	}
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShootBullet();
            FireParticles();
        }
    }

    private void ShootBullet()
    {
        Debug.Log(bulletSpawn.position.x + ", " + bulletSpawn.position.y + ", " + bulletSpawn.position.z);
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
    }
    private void FireParticles()
    {
        gunFire.Play();
    }
}
