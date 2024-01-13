﻿using System;
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
            Animator = GetComponent<PlayerAnimation>();
            var playerAnimator = (PlayerAnimation)Animator;
            var StaminaController = GetComponent<StaminaController>();
            BattleController = new PlayerBattleController(playerAnimator, StaminaController);
            Health = new PlayerHealth(MaxHealth);
            WeaponTaker = new WeaponTaker(this);
        }
        
        public void TakeWeapon()
        {
            WeaponTaker.TakeWeapon();
        }
    }
}