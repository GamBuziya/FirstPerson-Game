using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Enemy.States;
using UnityEngine;
using UnityEngine.Serialization;

public class StateMachine : MonoBehaviour
{
    public BaseState ActiveState;
    [FormerlySerializedAs("Enemy")] public SwordEnemy swordEnemy;

    private void Start()
    {
        swordEnemy = GetComponent<SwordEnemy>();
    }

    public void Initialise()
    {
        ChangeState(new PeaseState());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ActiveState" + ActiveState);
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
            ActiveState.SwordEnemy = GetComponent<SwordEnemy>();
            ActiveState.Player = GameObject.Find("Player").GetComponent<Player>();
            ActiveState.Enter();
        }
    }
    
}
