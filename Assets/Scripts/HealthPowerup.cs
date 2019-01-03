using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour {

    [SerializeField] int healAmount = 50;

    private PlayerController _player;

	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        SpinPowerup();
	}
    void SpinPowerup()
    {
        transform.Rotate(new Vector3(0, 90f * Time.deltaTime, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        Heal();
    }
    void Heal()
    {
        _player.Heal(healAmount);
    }
}
