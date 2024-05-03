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
        
        [Header("Attack Event Controller")]
        [SerializeField] protected UnityEvent _attackEvent;

        //protected StaminaController Stamina;
        protected BattleController BattleController;
        protected HealthManager Health;
        protected AnimatorManager Animator;
        protected HealthUI _healthUI;
        protected bool IsStun = false;

        protected int _currentHealth;
        protected float _currentStamina;

        
        
        public UnityEvent DeathEvent;
        
        
        
        

        public StaminaManager _staminaManager { get; protected set; }

        public int GetCurrentHealth() => _currentHealth;
        public void SetCurrentHealth(int currentHealth) => _currentHealth = currentHealth;
        public float GetCurrentStamina() => _currentStamina;
        
        public int GetMaxHealth() => _maxHealth;
        public float GetMaxStamina() => _maxStamina;
        
        public float GetAttackResist() => _attackResist;
        
        public void SetCurrentStamina(float stamina) => _currentStamina = stamina;
        public UnityEvent GetAttackEvent() => _attackEvent;
        public LayerMask GetEnemyLayer() => EnemyLayer;
        public BattleController GetBattleController() => BattleController;

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