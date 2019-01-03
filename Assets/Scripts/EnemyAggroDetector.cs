using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroDetector : MonoBehaviour {

    private EnemyAI _enemy;

	// Use this for initialization
	void Start () {
        _enemy = GetComponentInParent<EnemyAI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _enemy.Aggro();
        }
    }
}
