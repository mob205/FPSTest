using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunController : MonoBehaviour {

    [Header("General")]
    [SerializeField] ParticleSystem gunFire;
    [SerializeField] AudioSource gunAudio;
    [SerializeField] GameObject shooter;
    [SerializeField] Transform rayTransform;

    [Header("Gun Stats")]
    [SerializeField] float timeBetweenFire = 0.5f;
    [SerializeField] public float aimDeviation = 0.5F;
    [SerializeField] public int magSize = 30;
    [SerializeField] float reloadTime;


    bool isReloading;
    bool canFire = true;
    private int _ammoCount;
    bool hasFiredSinceReload = false;

	// Use this for initialization
	void Start () {
        _ammoCount = magSize;
	}
	
	// Update is called once per frame
	void Update () {
    }
    public int GetAmmoCount()
    {
        return _ammoCount;
    }
    public void Fire()
    {
        if(!canFire) { return; }
        if (_ammoCount <= 0) { return; }
        hasFiredSinceReload = true;
        ShootRay();
        FireParticles();
        PlayShootSound();
        ToggleFire();
        ConsumeAmmo();
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
        Ray ray = new Ray(rayTransform.position, rayTransform.forward);
        
        ray.direction = ray.direction + (new Vector3(UnityEngine.Random.Range(-aimDeviation, aimDeviation), UnityEngine.Random.Range(-aimDeviation, aimDeviation), 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            Debug.Log("Hit something");
            //Destroy(hit.collider.gameObject);
        }
        else
        {
            Debug.Log("Did not hit something");
            Debug.DrawRay(ray.origin, ray.direction * 200, Color.red, 200);
        }
    }
    public bool CheckReloadStatus()
    {
        return isReloading;
    }
    private void FireParticles()
    {
        gunFire.Play();
    }
    private void ConsumeAmmo()
    {
        _ammoCount--;
        if(_ammoCount <= 0)
        {
            StartCoroutine("Reload");
        }
    }
    public IEnumerator Reload()
    {
        Debug.Log("Reloading");
        hasFiredSinceReload = false;
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        Debug.Log(hasFiredSinceReload);
        if (!hasFiredSinceReload)
        {
            _ammoCount = magSize;
            Debug.Log("Reload successful.");
        }
        else
        {
            Debug.Log("Reload not successful.");
        }
        isReloading = false;
    }
    IEnumerator EnableFire()
    {
        yield return new WaitForSeconds(timeBetweenFire);
        canFire = true;
    }
}
