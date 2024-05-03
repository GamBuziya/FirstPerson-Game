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
        _gameCharacter = GetComponent<Player>();
        _checker = new BlockChecker();
        _enemyLayer = _gameCharacter.GetEnemyLayer();
        SubscribeToAttack(() => _gameCharacter.GetHealthPoints().TakeDamage(30));
    }
}
