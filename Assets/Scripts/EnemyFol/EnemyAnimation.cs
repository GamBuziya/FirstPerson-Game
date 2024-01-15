using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;


public class EnemyAnimation : AnimatorManager
{
    private void Awake()
    {
        _person = GetComponent<Enemy>();
    }

    void Start()
    {
        Animator[] _animators;
        _animators = GetComponentsInChildren<Animator>();
        if (_animators.Length > 0)
        {
            Animator = _animators[0];
        }
        else
        {
            Debug.LogError("No animators found in the hierarchy.");
        }
    }
    

    public new void PlayAnimation(SideOfMove sideOfMove, TypeOfMove typeOfMove)
    {
        base.PlayAnimation(sideOfMove, typeOfMove); // Виклик базового методу

        StartCoroutine(WaitAndReset());
    }
    
    public new void ResetAttack()
    {
        base.ResetAttack();
        StopCoroutine(WaitAndReset());
    }
    
    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(0.4f);
    }
}
