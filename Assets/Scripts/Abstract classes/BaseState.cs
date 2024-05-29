using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using GameCharacters;
using UnityEngine;

public abstract class BaseState
{
    public StateMachine StateMachine;
    public EnemyGameCharacter GameCharacter;
    public Player Player;
    public SideToGo Side;
    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
    
    public enum SideToGo
    {
        forward,
        left,
        right
    }
}
