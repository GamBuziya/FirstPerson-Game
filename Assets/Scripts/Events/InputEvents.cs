using System;
using UnityEngine;

namespace DefaultNamespace.Events
{
    public class InputEvents
    {
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
    }
}