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
    [SerializeField] public float baseAimDeviation = 0.5F;
    [SerializeField] public int magSize = 30;
    [SerializeField] protected float reloadTime;
    [SerializeField] float baseDamage = 5;
    [SerializeField] int bulletsPerShot = 1;
    [SerializeField] public float aimDeviation;

    public float damage;
    protected bool isReloading;
    bool canFire = true;
    protected int _ammoCount;
    Coroutine reloadCoroutine;

	// Use this for initialization
	void Start () {
        damage = baseDamage; 
        _ammoCount = magSize;
        aimDeviation = baseAimDeviation;
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
        if(isReloading) { CancelReload(); }
        FireParticles();
        PlayShootSound();
        ToggleFire();
        ConsumeAmmo();
        for (int i = 0; i < bulletsPerShot; i++)
        {
            ShootRay();
        }
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
            StartReload();
            
        }
    }
    public void StartReload()
    {
        reloadCoroutine = StartCoroutine("Reload");
    }
    private void CancelReload()
    {
        StopCoroutine(reloadCoroutine);
        isReloading = false;
    }
    public virtual IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        //Pump-shotgun reload idea: If pump-reload is selected, add 1 to ammo counter and repeat coroutine if not full. 
        _ammoCount = magSize;
        isReloading = false;
    }
    IEnumerator EnableFire()
    {
        yield return new WaitForSeconds(timeBetweenFire);
        canFire = true;
    }
}
