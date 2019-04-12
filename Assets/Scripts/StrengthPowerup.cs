using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPowerup : MonoBehaviour {

    [SerializeField] int damageModifier = 40;

    private PlayerController _player;
    private GunController _gun;

    // Use this for initialization
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _gun = _player.GetComponentInChildren<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        SpinPowerup();
    }
    void SpinPowerup()
    {
        transform.Rotate(new Vector3(0, 0, 90f * Time.deltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        IncreaseDamage();
        Debug.Log("damage increased");
    }
    void IncreaseDamage()
    {
        _gun.ModifyDamage(damageModifier);
        Destroy(gameObject);
    }
}
