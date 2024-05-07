using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Abstract_classes
{
    public class HealthManager
    {
        public UnityEvent DeathEvent;
        private GameCharacter _gameCharacter;
        
        
        public HealthManager(GameCharacter gameCharacter)
        {
            DeathEvent = new UnityEvent();
            _gameCharacter = gameCharacter;
        }
        
        public void TakeDamage(int damage)
        {
            Debug.Log("damage " + damage);
            Debug.Log("_gameCharacter.GetCurrentHealth() " + _gameCharacter.GetCurrentHealth());
            var temp = _gameCharacter.GetCurrentHealth();
            if(_gameCharacter.GetCurrentHealth() > 0) _gameCharacter.SetCurrentHealth(temp - damage);
            DeathChecker();
        }

        public void TakeHeal(int points, ref int currentHealth)
        {
            if(currentHealth < _gameCharacter.GetMaxHealth()) currentHealth += points;
        }
        
        private void DeathChecker()
        {
            if (_gameCharacter.GetCurrentHealth() <= 0)
            {
                DeathEvent.Invoke();
            }
        }
    }
}