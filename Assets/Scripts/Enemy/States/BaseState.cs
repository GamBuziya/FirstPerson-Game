using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public StateMachine StateMachine;
    public Enemy Enemy;
    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
