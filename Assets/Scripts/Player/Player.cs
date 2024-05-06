using System;
using Abstract_classes;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.NonMonobehaviourClasses;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    public class Player: GameCharacter, IMagic
    {
        [SerializeField]
        private float _maxMagic;
        public float MaxMagic {get; set; }
        public float CurrentMagic { get; set; }
        
        private float timer = 0f;
        private float interval = 0.1f;
        private float _maxMagic1;

        private new void Awake()
        {
            base.Awake();
            MaxMagic = _maxMagic;
            CurrentMagic = _maxMagic;
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
        
    }
}