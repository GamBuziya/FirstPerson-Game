using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent _OnWeaponCollisionEnter;
    private PlayerHealthController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerHealthController>();
        SubscribeToCollisionEvent(() => _player._playerHealth.BasicTakeDamage(30));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon") && other.gameObject.layer == 7)
        {
            _OnWeaponCollisionEnter.Invoke();
        }
    }
    
    
    private void SubscribeToCollisionEvent(UnityAction action)
    {
        _OnWeaponCollisionEnter.AddListener(action);
    } 
}
