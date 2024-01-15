using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class GameCharacter : MonoBehaviour
    {
        [SerializeField] protected int MaxHealth = 100;
        [SerializeField] protected LayerMask EnemyLayer;
        
        protected StaminaController Stamina;
        protected BattleController BattleController;
        protected HealthPoints Health;
        protected AnimatorManager Animator;
        protected HealthUI _healthUI;
        protected bool IsStun = false;

        
        public StaminaController GetStamina() => Stamina;
        public LayerMask GetEnemyLayer() => EnemyLayer;
        public BattleController GetBattleController() => BattleController;

        public HealthPoints GetHealthPoints() => Health;

        public AnimatorManager GetAnimatorManager() => Animator;

        public bool GetStun() => IsStun;
        public void SetStun(bool temp) => IsStun = temp;


    }
}