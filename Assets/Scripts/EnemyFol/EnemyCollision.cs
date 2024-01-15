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
            _enemyLayer = _hero.GetEnemyLayer();
            SubscribeToAttack(() => _hero.GetHealthPoints().BasicTakeDamage(30));
        }
    }
}