using System;
using Abstract_classes;
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
        [Range(0f, 1f)] [SerializeField] protected float _attackResist;
        
        
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

        public int GetCurrentHealth() => _currentHealth;
        public void SetCurrentHealth(int currentHealth) => _currentHealth = currentHealth;
        public float GetCurrentStamina() => _currentStamina;
        
        public int GetMaxHealth() => _maxHealth;
        public float GetMaxStamina() => _maxStamina;
        
        public float GetAttackResist() => _attackResist;
        
        public void SetCurrentStamina(float stamina) => _currentStamina = stamina;
        public LayerMask GetEnemyLayer() => EnemyLayer;
        public WeaponBattleController GetBattleController() => WeaponBattleController;

        public HealthManager GetHealthPoints() => Health;

        public AnimatorManager GetAnimatorManager() => Animator;

        public bool GetStun() => IsStun;
        public void SetStun(bool temp) => IsStun = temp;


        public void StaminaDamage(int damage)
        {
            _staminaManager.StaminaDamage(damage, ref _currentStamina);
        }


    }
}