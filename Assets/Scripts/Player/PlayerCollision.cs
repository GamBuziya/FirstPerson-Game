using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Abstract_classes;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : CollisionManager
{

    private void Awake()
    {
        _hero = GetComponent<Player>();
        _checker = new BlockChecker(_hero);
        _enemyLayer = _hero.EnemyLayer;
        SubscribeToAttack(() => _hero.Health.BasicTakeDamage(30));
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon") && (_enemyLayer & (1 << other.gameObject.layer)) != 0)
        {
            var isBlock = _checker.IsBlock(other.gameObject);
            if (!isBlock)
            {
                _IsDamaged.Invoke();
            }
            else
            {
                PlayParticleEffect(other.contacts[0].point);
                var enemyCharacter = other.gameObject.GetComponentInParent<GameCharacter>();
                enemyCharacter.IsStun = true;
                _IsBlocked.Invoke();
                enemyCharacter.Animator.EnemyParriedEffect();
            }
            
        }
    }
    
}
