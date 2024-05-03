using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        Debug.Log("SubmitPressed");
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
