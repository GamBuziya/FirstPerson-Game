using DefaultNamespace.Abstract_classes;
using UnityEngine;

namespace DefaultNamespace.EnemyFol
{
    public class EnemyCollision : CollisionManager
    {
        private void Awake()
        {
            _hero = GetComponent<global::Enemy>();
            _checker = new BlockChecker(_hero);
            _enemyLayer = _hero.EnemyLayer;
            SubscribeToAttack(() => _hero.Health.BasicTakeDamage(30));
        }
    }
}