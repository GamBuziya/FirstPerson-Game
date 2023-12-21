using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent { get => _agent;}
    public Path Path;   
    
    private StateMachine _stateMachine;
    private NavMeshAgent _agent;

    public GameObject Player { get; set; }
    private float _sightDistance = 13f;
    private float _fieldOfView = 70f;

    [SerializeField] private string _currentState;

    void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine.Initialise();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CanSee();
        _currentState = _stateMachine.ActiveState.ToString();
    }

    public bool CanSee()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < _sightDistance)
        {
            Vector3 targetDirection = Player.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, targetDirection);

            if (angle >= -_fieldOfView && angle <= _fieldOfView)
            {
                Ray ray = new Ray(transform.position, targetDirection);
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(ray, out hitInfo, _sightDistance))
                {
                    if (hitInfo.transform.gameObject == Player)
                    {
                        Debug.DrawRay(ray.origin, ray.direction* _sightDistance);
                        return true;
                    }
                }
                
            }
        }

        return false;
    }
    
}
