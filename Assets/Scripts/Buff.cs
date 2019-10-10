using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {

    [SerializeField] public BuffType buffType = BuffType.Damage;
    [SerializeField] public int buffStrength = 40;

    private PlayerController _player;

    public enum BuffType { Damage, Speed, Health };
    
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
        transform.Rotate(new Vector3(0,90f * Time.deltaTime, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        _player.UpdateBuffs(buffType, buffStrength);
        Destroy(gameObject);
    }

}
