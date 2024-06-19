using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    
    [CreateAssetMenu(menuName = "Weapon")]
    public class WeaponSO : ScriptableObject
    {
        public string Name;
        public int BasicStaminaCost;
        public int PowerStaminaCost;
        public int BasicDamage;
        public int PowerDamage;

        public int DamageLevel;
        public int StaminaLevel;
    }
}