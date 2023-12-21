using System;
using DefaultNamespace.Abstract_classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private TextMeshProUGUI _DeadMassage;
        public PlayerHealth _playerHealth;

        private void Awake()
        {
            _playerHealth = new PlayerHealth(_health);
        }

        private void Update()
        {
            DeathChecker();
        }

        private void DeathChecker()
        {
            if (_playerHealth.Health <= 0)
            {
                _DeadMassage.enabled = false;
            }
        }
    }
}