using System;
using Abstract_classes;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.NonMonobehaviourClasses;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    public class Player: MagicGameCharacter
    {
        public WeaponTaker WeaponTaker;
        
        private float timer = 0f;
        private float interval = 0.1f;

        private new void Awake()
        {
            base.Awake();
            
            Animator = GetComponent<PlayerAnimation>();
            WeaponBattleController = new PlayerWeaponBattleController(this);
            WeaponTaker = new WeaponTaker(this);
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