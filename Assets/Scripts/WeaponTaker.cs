using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class WeaponTaker : MonoBehaviour
    {
        private PlayerBattleController _battleController;


        private void Start()
        {
            _battleController = GetComponent<PlayerBattleController>();
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
                    _battleController.SetAnimator(weapon.GetComponent<Animator>());
                }
            }
            else
            {
                Debug.Log("PlayerAnimation");
            }
        }
    }
}