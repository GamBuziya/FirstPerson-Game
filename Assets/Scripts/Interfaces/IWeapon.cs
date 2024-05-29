using DefaultNamespace.Enums;
using Managers;

namespace DefaultNamespace.Abstract_classes
{
    public interface IWeapon
    {
        public WeaponManager Weapon { get; set; }

        public void StaminaDamage(TypeOfStaminaDamage type);
    }
}