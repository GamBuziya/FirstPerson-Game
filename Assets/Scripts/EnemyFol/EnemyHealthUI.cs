using DefaultNamespace.Abstract_classes;
using UnityEngine;

namespace EnemyFol
{
    public class EnemyHealthUI : HealthUI
    {
        private void Start()
        {
            _hero = GetComponent<Enemy>();
        }

        public void SetCurrentHealthPoint(Enemy hero)
        {
            _currentHealth = hero.GetHealthPoints().GetHealth();
        }

        private void Update()
        {
            _currentHealth = _hero.GetHealthPoints().GetHealth();
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _hero.GetHealthPoints().GetMaxHealth());
            UpdateHealthUI();
        }
    }
}