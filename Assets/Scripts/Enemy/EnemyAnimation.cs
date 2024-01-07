using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;


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
        _animator.SetBool(PartsOfBattleMoves.Left.ToString(), true);
        _animator.SetBool(TypeOfMove.IsAttack.ToString(), true);
        StartCoroutine(WaitAndReset());
    }

    public void ResetBlock()
    {
        _animator.SetBool(TypeOfMove.IsBlock.ToString(), false);
    }

    public void ResetAttack()
    {
        StopCoroutine(WaitAndReset());
        _animator.SetBool(TypeOfMove.IsAttack.ToString(), false);
        _animator.SetBool(PartsOfBattleMoves.Left.ToString(), false);
    }
    
    
    
    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(0.4f);
    }
}
