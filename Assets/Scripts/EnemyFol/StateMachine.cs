using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Enemy.States;
using GameCharacters;
using UnityEngine;
using UnityEngine.Serialization;

public class StateMachine : MonoBehaviour
{
    public BaseState ActiveState;
    public EnemyGameCharacter gameCharacter;

    private void Start()
    {
        gameCharacter = GetComponent<EnemyGameCharacter>();
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
            ActiveState.GameCharacter = GetComponent<EnemyGameCharacter>();
            ActiveState.Player = GameObject.Find("Player").GetComponent<Player>();
            ActiveState.Enter();
        }
    }
    
}
