using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class GameCharacter : MonoBehaviour
    {
        [SerializeField] public StaminaController Stamina;
        [SerializeField] protected int MaxHealth = 100;
        
        public BattleController BattleController;
        public Damagable PlayerHealth;
        
    }
}