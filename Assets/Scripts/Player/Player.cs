using System;
using Abstract_classes;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.NonMonobehaviourClasses;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    public class Player: GameCharacter
    {
        public WeaponTaker WeaponTaker;
        
        private float timer = 0f;
        private float interval = 0.1f;

        private void Awake()
        {
            Animator = GetComponent<PlayerAnimation>();
            
            _staminaManager = new StaminaManager(_maxStamina, 1.2f, 0.2f);
            BattleController = new PlayerBattleController(this);
            Health = new PlayerHealth(_maxHealth);
            
            WeaponTaker = new WeaponTaker(this);
            _currentStamina = _maxStamina;
        }


        private void Update()
        {
            
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                _staminaManager.RegenerateStamina(ref _currentStamina);
                timer = 0f;
            }
        }

        public void TakeWeapon()
        {
            WeaponTaker.TakeWeapon();
        }
        
        
    }
}