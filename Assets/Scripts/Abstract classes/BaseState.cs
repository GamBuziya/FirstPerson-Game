using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public abstract class BaseState
{
    public StateMachine StateMachine;
    public Enemy Enemy;
    public Player Player;
    public bool RightRegroup = false;
    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
