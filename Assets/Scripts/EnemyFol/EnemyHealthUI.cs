using DefaultNamespace.Abstract_classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

namespace EnemyFol
{
    public class EnemyHealthUI : HealthUI
    {
        private void Start()
        {
            _hero = GetComponent<Enemy>();
            _currentHealth = _hero.Health.Health;
        }

        private void Update()
        {
            _currentHealth = _hero.Health.Health;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _hero.Health.MaxHealth);
            UpdateHealthUI();
        }
    }
}