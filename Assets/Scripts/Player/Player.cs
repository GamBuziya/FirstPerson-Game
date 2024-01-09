using System;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.NonMonobehaviourClasses;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player: GameCharacter
    {
        public WeaponTaker WeaponTaker;

        private void Awake()
        {
            var Animation = GetComponent<PlayerAnimation>();
            var StaminaController = GetComponent<StaminaController>();
            BattleController = new PlayerBattleController(Animation, StaminaController);
            PlayerHealth = new PlayerHealth(MaxHealth);
            WeaponTaker = new WeaponTaker(this);
        }
        
        public void TakeWeapon()
        {
            WeaponTaker.TakeWeapon();
        }
    }
}