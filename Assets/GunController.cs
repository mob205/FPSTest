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
