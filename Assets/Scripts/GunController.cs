using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunController : MonoBehaviour {

    [Header("General")]
    [SerializeField] ParticleSystem gunFire;
    [SerializeField] AudioSource gunAudio;
    [SerializeField] Transform rayTransform;

    [Header("Gun Stats")]
    [SerializeField] float timeBetweenFire = 0.5f;
    [SerializeField] public float aimDeviation = 0.5F;
    [SerializeField] public int magSize = 30;
    [SerializeField] float reloadTime;
    [SerializeField] float baseDamage = 5;

    public float damage;
    bool isReloading;
    bool canFire = true;
    private int _ammoCount;
    bool hasFiredSinceReload = false;

	// Use this for initialization
	void Start () {
        damage = baseDamage; 
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
    public void ModifyDamage(float damageModifier)
    {
        damage = baseDamage * damageModifier;
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
        LayerMask mask = LayerMask.GetMask("AI Range");
        ray.direction = ray.direction + (new Vector3(UnityEngine.Random.Range(-aimDeviation, aimDeviation), UnityEngine.Random.Range(-aimDeviation, aimDeviation), 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~mask)) 
        {
            GameObject collisionGO = hit.collider.gameObject;
            if (collisionGO.GetComponent<RayDetector>())
            {
                collisionGO.GetComponent<RayDetector>().DetectRayHit(damage);
            }
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
