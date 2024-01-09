using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class WeaponTaker
    {
        private Player _player;

        public WeaponTaker(Player player)
        {
            _player = player;
        }

        public void TakeWeapon()
        {
            var weaponSlot = GameObject.Find("WeaponSlot");

            if (weaponSlot != null)
            {
                int childCount = weaponSlot.transform.childCount;

                if (childCount > 0)
                {
                    var childTransform = weaponSlot.transform.GetChild(0);

                    var weapon = childTransform.gameObject;
                    
                    if (_player.BattleController is PlayerBattleController playerBattleController)
                    {
                        playerBattleController.SetAnimator(weapon.GetComponent<Animator>());
                    }
                    
                }
            }
            else
            {
                Debug.Log("PlayerAnimation");
            }
        }
    }
}