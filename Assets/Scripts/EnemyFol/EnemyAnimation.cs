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
        
        var temp = _person.GetComponentsInChildren<Transform>(true);

        foreach (var childTransform in temp)
        {
            var childGameObject = childTransform.gameObject;

            if (childGameObject.CompareTag("Weapon"))
            {
                WeaponAnimator = childGameObject.GetComponent<Animator>();
                break;  // Зупинити цикл, якщо знайдено потрібний об'єкт
            }
        }
        
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
