using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    [Header("General")]
    [SerializeField] Transform rayTransform;
    [Header("Stats")]
    [SerializeField] float maxHealth = 10;
    [SerializeField] float fireRange = 55f;

    private float _health;
    private GunController _gun;
    private NavMeshAgent _agent;    
    private PlayerController _player;

    bool isAggroed;
	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<PlayerController>();
        _agent = GetComponent<NavMeshAgent>();
        _gun = GetComponentInChildren<GunController>();
        _health = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isAggroed)
        {
            MoveToPlayer();
            DecideShoot();
        }

        
	}
    void DecideShoot()
    {
        Ray ray = new Ray(rayTransform.position, rayTransform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, fireRange))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _gun.Fire();
            }
        }
    }
    void MoveToPlayer()
    {
        transform.LookAt(_player.transform.position);
        _agent.SetDestination(_player.transform.position);
    }
    void ProcessRayHit(float damage)
    {
        Damage(damage);
        
    }
    void Damage(float damage)
    {
        
        isAggroed = true;
        _health -= damage;
        Debug.Log("damaged. " + _health);
        if (_health <= 0)
        {
            InitiateDeathSequence();
        }
    }
    void InitiateDeathSequence()
    {
        Destroy(gameObject);
    }
    public void Aggro()
    {
        isAggroed = true;
    }
}
