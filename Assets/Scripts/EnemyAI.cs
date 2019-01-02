using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    [SerializeField] int maxHealth;
    [SerializeField] int defense;
    [SerializeField] Transform rayTransform;
    [SerializeField] float fireRange = 55f;


    private GunController _gun;
    private NavMeshAgent _agent;    
    private PlayerController _player;
    
	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<PlayerController>();
        _agent = GetComponent<NavMeshAgent>();
        _gun = GetComponentInChildren<GunController>();
	}
	
	// Update is called once per frame
	void Update () {
        MoveToPlayer();
        DecideShoot();
	}
    void DecideShoot()
    {
        LayerMask mask = LayerMask.GetMask("Player");
        if (Physics.Raycast(rayTransform.position, rayTransform.forward, fireRange, mask))
        {
            _gun.Fire();
        }
    }
    void MoveToPlayer()
    {
        transform.LookAt(_player.transform.position);
        _agent.SetDestination(_player.transform.position);
    }
}
