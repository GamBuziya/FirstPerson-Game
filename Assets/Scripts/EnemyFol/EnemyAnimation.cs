using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enemy.States;
using DefaultNamespace.Enums;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyAnimation : AnimatorManager
{
    private Animator _enemyAnimator;
    private Enemy _temp;

    void Start()
    {
        _person = GetComponent<Enemy>();
        _enemyAnimator = GetComponent<Animator>();
        WeaponAnimator = GameObject.FindWithTag("Weapon").GetComponent<Animator>();
        
        _person.GetHealthPoints().DeathEvent.AddListener(DeathAnimation);
        _temp = (Enemy)_person;
        
    }

    void LateUpdate()
    {
        if (_temp.GetStateMachine().ActiveState is AttackState || _temp.GetStateMachine().ActiveState is LowStaminaState)
        {
            StateAnimation(true);
        }
        else
        {
            StateAnimation(false);
        }
    }

    public void DeathAnimation()
    {
        Debug.Log("Death");
        _enemyAnimator.SetTrigger("IsDead");
    }

    public void StateAnimation(bool state)
    {
        _enemyAnimator.SetBool("IsAgressive", state);
    }
    
    
    
    
}
