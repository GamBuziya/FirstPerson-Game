using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Abstract_classes;
using Unity.VisualScripting;
using UnityEngine;

public class Resets : MonoBehaviour
{
    private AnimatorManager _controller;

    private void Awake()
    {
        if (GetComponentInParent<AnimatorManager>() != null)
        {
            SetController();
        }
        
    }

    public void SetController()
    {
        _controller = GetComponentInParent<AnimatorManager>();
    }

    public void ResetBlock()
    {
        _controller.ResetBlock();
    }

    public void ResetAttack()
    {
        _controller.ResetAttack();
    }
    
}
