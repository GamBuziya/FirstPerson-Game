
using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAnimation : MonoBehaviour, IAnimationReset
{
    private Animator _animator;

    void Start()
    {
        Animator[] _animators;
        _animators = GetComponentsInChildren<Animator>();
        if (_animators.Length > 0)
        {
            _animator = _animators[0];
        }
        else
        {
            Debug.LogError("No animators found in the hierarchy.");
        }
    }
    

    public void Attack()
    {
        _animator.SetBool("FirstAttack", true);
        StartCoroutine(WaitAndReset());
    }

    public void ResetBlock()
    {
        _animator.SetBool("Block", false);
    }

    public void ResetAttack()
    {
        StopCoroutine(WaitAndReset());
        if (Random.Range(0, 1f) < 0.3)
        {
            _animator.SetBool("SecondAttack", true);
            return;
        }
        _animator.SetBool("FirstAttack", false);
    }
    
    
    
    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(0.4f);
    }
}
