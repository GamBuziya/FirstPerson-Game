using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class GameCharacter : MonoBehaviour
    {
        [SerializeField] public StaminaController Stamina;
        [SerializeField] protected int MaxHealth = 100;
        
        public LayerMask EnemyLayer;
        public BattleController BattleController;
        public HealthPoints Health;
        public AnimatorManager Animator;
        public bool IsStun = false;
    }
}