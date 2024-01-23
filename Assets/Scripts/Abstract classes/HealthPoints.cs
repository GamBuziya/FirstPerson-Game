using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class HealthPoints
    {
        public UnityEvent DeathEvent;
        protected int MaxHealth;
        protected int Health;

        public int GetHealth() => Health;
        public int GetMaxHealth() => MaxHealth;

        public HealthPoints(int health)
        {
            DeathEvent = new UnityEvent();
            MaxHealth = health;
            Health = health;
        }

        public void BasicTakeDamage(int damage)
        {
            TakeDamage(damage);
        }
        
        public void BasicTakeHeal(int points)
        {
            TakeHeal(points);
        }

        protected virtual void TakeDamage(int damage)
        {
            if(Health > 0) Health -= damage;
            DeathChecker();
        }

        protected virtual void TakeHeal(int points)
        {
            if(Health < MaxHealth) Health += points;
        }
        
        private void DeathChecker()
        {
            if (Health <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            
            DeathEvent.Invoke();
        }
    }
}