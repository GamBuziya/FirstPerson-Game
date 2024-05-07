using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    
    public event Action<string> onItemCollected;
    public void ItemCollected(string name)
    {
        if (onItemCollected != null)
        {
            onItemCollected(name);
        }
    }
    
    public event Action<GameCharacter, GameCharacter> onPhysicDamage;
    public void PhysicDamage(GameCharacter attacker, GameCharacter defender)
    {
        if (onPhysicDamage != null)
        {
            onPhysicDamage(attacker, defender);
        }
    }
    
    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        if (onSubmitPressed != null)
        {
            onSubmitPressed();
        }
    }
        
    public event Action onInteract;
    public void Interacted()
    {
        if (onInteract != null)
        {
            onInteract();
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
