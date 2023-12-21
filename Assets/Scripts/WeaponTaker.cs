using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class WeaponTaker : MonoBehaviour
    {
        private PlayerAnimation _playerAnimation;


        private void Start()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
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
                    _playerAnimation.Animator = weapon.GetComponent<Animator>();
                }
            }
            else
            {
                Debug.Log("PlayerAnimation");
            }
        }
    }
}