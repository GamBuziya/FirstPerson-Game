using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent _IsDamaged;
    [SerializeField] private UnityEvent _IsBlocked;
    
    private Player _player;
    private BlockChecker _checker;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _checker = new BlockChecker(_player);
        
        SubscribeToCollisionEvent(() => _player.Health.BasicTakeDamage(30));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon") && other.gameObject.layer == 7)
        {
            var isBlock = _checker.IsBlock(other.gameObject);
            if (!isBlock)
            {
                Debug.Log("Not blocked, invoking damage event");
                _IsDamaged.Invoke();
            }
            else
            {
                Debug.Log("Blocked, no damage");
            }
            
        }
    }
    
    
    private void SubscribeToCollisionEvent(UnityAction action)
    {
        _IsDamaged.AddListener(action);
    } 
}
