using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    
    private NavMeshAgent _agent;    
    private PlayerController _player;
	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<PlayerController>();
        _agent = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
        TargetPlayer();
	}
    void TargetPlayer()
    {
        _agent.SetDestination(_player.transform.position);
    }
}
