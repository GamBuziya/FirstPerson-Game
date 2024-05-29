using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool UseEvents;
    public string BasicCondition;
    public string SecondCondition;
    private bool _basicCond = true;
    
    
    public void BaseInteract()
    {
        _basicCond = !_basicCond;
        if(UseEvents) gameObject.GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
        
    }

    public string GetActionName()
    {
        if (_basicCond) return BasicCondition;
        return SecondCondition;

    }
}
