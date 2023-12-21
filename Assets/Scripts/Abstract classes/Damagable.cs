using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class Damagable
    {
        public int Health;
        public int MaxHealth;

        public Damagable(int health)
        {
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
        }

        protected virtual void TakeHeal(int points)
        {
            if(Health < MaxHealth) Health += points;
        }
        
        
    }
}