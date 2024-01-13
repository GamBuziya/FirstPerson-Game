using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Abstract_classes;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent _IsDamaged;
    [SerializeField] private UnityEvent _IsBlocked;
    [SerializeField] private ParticleSystem _particleEffect;
    
    private Player _player;
    private BlockChecker _checker;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _checker = new BlockChecker(_player);
        
        SubscribeToAttack(() => _player.Health.BasicTakeDamage(30));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon") && other.gameObject.layer == 7)
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
    
    private void PlayParticleEffect(Vector3 position)
    {
        if (_particleEffect != null)
        {
            ParticleSystem particleInstance = Instantiate(_particleEffect, position, Quaternion.identity);
            particleInstance.Play();
            Destroy(particleInstance.gameObject, particleInstance.main.duration);
        }
    }
    
    private void SubscribeToAttack(UnityAction action)
    {
        _IsDamaged.AddListener(action);
    }
}
