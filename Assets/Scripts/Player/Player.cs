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

        private new void Awake()
        {
            MagicManager = GetComponent<BasicMagicManager>();
            _maxHealth = 100 + GameStatsManager.Instance.LevelHealthBonus * 20;
            _maxStamina = 100 + GameStatsManager.Instance.LevelStaminaBonus * 10;
            _maxMagic = 100 + GameStatsManager.Instance.LevelMagicBonus * 10;
            MaxMagic = _maxMagic;
            CurrentMagic = _maxMagic;
            
            base.Awake();
        }

        private void Start()
        {
            Weapon = GetComponentInChildren<WeaponManager>();
        }

        private void OnEnable()
        {
            Animator = GetComponent<PlayerAnimation>();
            WeaponBattleController = new PlayerWeaponBattleController(this);
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
                damage = Weapon.BasicWeaponData.BasicStaminaCost;
            }
            else if (type == TypeOfStaminaDamage.PowerAttack)
            {
                damage = Weapon.BasicWeaponData.PowerStaminaCost;
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