using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Enemy.States;
using DefaultNamespace.Events;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState ActiveState;
    public MoveToState MoveToState;
    public Enemy Enemy;

    private void Start()
    {
        Enemy = GetComponent<Enemy>();
        GameEventManager.Instance.MoveToEvent.onMoveToEvent += MoveTo;
    }

    private void MoveTo(Transform destination, string ObjectName)
    {
        Debug.Log("ObjectName " + ObjectName);
        Debug.Log("Enemy " + gameObject.name);
        if(gameObject.name.Equals(ObjectName)) ChangeState(new MoveToState(destination));
    }


    public void Initialise()
    {
        ChangeState(new PeaseState());
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveState != null)
        {
            ActiveState.Perform();
        }
    }

    public void ChangeState(BaseState otherState)
    {
        if (ActiveState != null)
        {
            ActiveState.Exit();
        }
        ActiveState = otherState;
        
        if (ActiveState != null)
        {
            ActiveState.StateMachine = this;
            ActiveState.Enemy = GetComponent<Enemy>();
            ActiveState.Player = GameObject.Find("Player").GetComponent<Player>();
            ActiveState.Enter();
        }
    }
    
}
