using System;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.NonMonobehaviourClasses;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player: MonoBehaviour
    {
        [SerializeField] private int MaxHealth = 100;
        
        [SerializeField] public PlayerStaminaContoller Stamina;
        public PlayerBattleController BattleController;
        public PlayerHealth PlayerHealth;
        public WeaponTaker WeaponTaker;

        private void Awake()
        {
            var Animation = GetComponent<PlayerAnimation>();
            var StaminaController = GetComponent<StaminaController>();
            BattleController = new PlayerBattleController(Animation, StaminaController);
            PlayerHealth = new PlayerHealth(MaxHealth);
            WeaponTaker = new WeaponTaker(this);
        }

        private void Update()
        {
            
        }

        public void TakeWeapon()
        {
            WeaponTaker.TakeWeapon();
        }
    }
}