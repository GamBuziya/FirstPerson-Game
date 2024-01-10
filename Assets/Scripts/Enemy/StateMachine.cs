using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState ActiveState;
    public Enemy Enemy;
    
    private void Start()
    {
        Enemy = GetComponent<Enemy>();
    }

    public void Initialise()
    {
        ChangeState(new PatrolState());
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
            //Setup new state
            ActiveState.StateMachine = this;
            ActiveState.Enemy = GetComponent<Enemy>();
            ActiveState.Enter();
        }
    }
}
