using System;
using Abstract_classes;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using DefaultNamespace.NonMonobehaviourClasses;
using Managers;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    public class Player: GameCharacter, IMagic, IWeapon
    {
        [SerializeField]
        private float _maxMagic;
        public float MaxMagic {get; set; }
        public float CurrentMagic { get; set; }
        public BasicMagicManager MagicManager { get; set; }
        
        public WeaponManager Weapon { get; set; }

        private float timer = 0f;
        private float interval = 0.1f;
        private float _maxMagic1;

        private new void Awake()
        {
            base.Awake();
            MaxMagic = _maxMagic;
            CurrentMagic = _maxMagic;
            MagicManager = GetComponent<BasicMagicManager>();
            Animator = GetComponent<PlayerAnimation>();
            WeaponBattleController = new PlayerWeaponBattleController(this);
            Weapon = GetComponentInChildren<WeaponManager>();
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
        
        public void StaminaDamage(TypeOfStaminaDamage type)
        {
            var damage = 0;
            if (type == TypeOfStaminaDamage.BasicAttack)
            {
                damage = Weapon.WeaponData.BasicStaminaCost;
            }
            else if (type == TypeOfStaminaDamage.PowerAttack)
            {
                damage = Weapon.WeaponData.PowerStaminaCost;
            }
            else
            {
                //Вартість ривка
                damage = 10;
            }
            _staminaManager.StaminaDamage(damage, ref _currentStamina);
        }
    }
}