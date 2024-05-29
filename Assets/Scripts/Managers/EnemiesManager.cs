using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class EnemiesManager : MonoBehaviour
    {
        public static EnemiesManager Instance;
        private SwordEnemy[] _enemies;

        private void Awake()
        {
            Instance = this;
            _enemies = GetComponentsInChildren<SwordEnemy>();
        }

        public SwordEnemy GetClosestEnemy(SwordEnemy currentSwordEnemy)
        {
            SwordEnemy closestSwordEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (SwordEnemy enemy in _enemies)
            {
                if (enemy == currentSwordEnemy)
                    continue;
                
                float distance = Vector3.Distance(currentSwordEnemy.transform.position, enemy.transform.position);
                
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSwordEnemy = enemy;
                }
            }

            return closestSwordEnemy;
        }
    }
}