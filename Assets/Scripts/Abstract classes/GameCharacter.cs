using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class GameCharacter : MonoBehaviour
    {
        [Header("GameStats")]
        [SerializeField] protected int MaxHealth = 100;
        [SerializeField] protected float MaxStamina = 100;
        [SerializeField] protected LayerMask EnemyLayer;
        
        [Header("Attack Event Controller")]
        [SerializeField] protected UnityEvent _attackEvent;
        
        
        protected StaminaController Stamina;
        protected BattleController BattleController;
        protected HealthPoints Health;
        protected AnimatorManager Animator;
        protected HealthUI _healthUI;
        protected bool IsStun = false;

        public int CurrentHealth { get; protected set; }
        public float CurrentStamina { get; protected set; }

        
        public UnityEvent GetAttackEvent() => _attackEvent;
        public StaminaController GetStamina() => Stamina;
        public LayerMask GetEnemyLayer() => EnemyLayer;
        public BattleController GetBattleController() => BattleController;

        public HealthPoints GetHealthPoints() => Health;

        public AnimatorManager GetAnimatorManager() => Animator;

        public bool GetStun() => IsStun;
        public void SetStun(bool temp) => IsStun = temp;


    }
}