using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class EnemiesManager : MonoBehaviour
    {
        public static EnemiesManager Instance;
        private Enemy[] _enemies;

        private void Awake()
        {
            Instance = this;
            _enemies = GetComponentsInChildren<Enemy>();
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