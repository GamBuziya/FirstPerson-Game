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
        _checker = new BlockChecker();
        _enemyLayer = _hero.GetEnemyLayer();
        SubscribeToAttack(() => _hero.GetHealthPoints().BasicTakeDamage(30));
    }
}
