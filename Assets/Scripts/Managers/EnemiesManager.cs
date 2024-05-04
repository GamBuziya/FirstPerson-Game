using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class EnemiesManager : MonoBehaviour
    {
        private List<Enemy> _enemies;

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                Enemy enemyComponent;
                
                if (child.TryGetComponent(out enemyComponent))
                {
                    _enemies.Add(enemyComponent);
                }
            }
        }

        public Enemy GetClosestEnemy(Enemy currentEnemy)
        {
            Enemy closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (Enemy enemy in _enemies)
            {
                if (enemy == currentEnemy)
                    continue;
                
                float distance = Vector3.Distance(currentEnemy.transform.position, enemy.transform.position);
                
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }
    }
}