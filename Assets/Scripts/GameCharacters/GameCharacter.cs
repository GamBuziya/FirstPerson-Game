using System;
using System.Collections.Generic;
using Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class GameCharacter : MonoBehaviour
    {
        [Header("GameStats")]
        [SerializeField] protected int _maxHealth = 100;
        [SerializeField] protected float _maxStamina = 100;
        [SerializeField] protected LayerMask EnemyLayer;
        [Range(0f, 100f)] [SerializeField] protected float _attackResist;
        [SerializeField] private List<MagicResistance> _magicResistances;

        [field: Range(1f, 3f)] [SerializeField]
        protected float _speed;
        
        
        [Serializable]
        public struct MagicResistance
        {
            public TypeMagicAttack MagicType;
            [Range(0f, 100f)] public float Resistance;
        }
        
        
        protected WeaponBattleController WeaponBattleController;
        protected HealthManager Health;
        protected AnimatorManager Animator;
        protected bool IsStun = false;

        protected int _currentHealth;
        protected float _currentStamina;
        
        public StaminaManager _staminaManager { get; protected set; }


        protected void Awake()
        {
            _currentStamina = _maxStamina;
            _currentHealth = _maxHealth;
            
            Health = new HealthManager(this);
            _staminaManager = new StaminaManager(_maxStamina, 1.2f, 0.2f);
        }


        public float GetSpeed() => _speed;
        public int GetCurrentHealth() => _currentHealth;
        public void SetCurrentHealth(int currentHealth) => _currentHealth = currentHealth;
        public float GetCurrentStamina() => _currentStamina;
        
        public int GetMaxHealth() => _maxHealth;
        public float GetAttackResist() => _attackResist;
        
        public void SetCurrentStamina(float stamina) => _currentStamina = stamina;
        public LayerMask GetEnemyLayer() => EnemyLayer;
        public WeaponBattleController GetBattleController() => WeaponBattleController;

        public HealthManager GetHealthPoints() => Health;

        public AnimatorManager GetAnimatorManager() => Animator;

        public bool GetStun() => IsStun;
        public void SetStun(bool temp) => IsStun = temp;
        

        public float GetCurrentMagicResist(IMagic attacker)
        { 
            var typeOfMagic = attacker.MagicManager.GetMagicData().TypeMagic;
            
            var magicResistance = _magicResistances.Find(magic => magic.MagicType == typeOfMagic);
            var resist = magicResistance.Equals(default(MagicResistance)) ? 0f : magicResistance.Resistance;

            return resist;
        }


    }
    
}