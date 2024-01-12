using System;
using DefaultNamespace.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Enemy
{
    public class EnemySideAttackUI
    {
        private Image _arrow;

        public EnemySideAttackUI(Image arrow)
        {
            _arrow = arrow;
        }

        public void ChangeUI(TypeOfMove getCurrentTypeOfMove, PartsOfBattleMoves move)
        {
            if (getCurrentTypeOfMove != TypeOfMove.IsAttack)
            {
                _arrow.enabled = false;
                return;
            }

            Quaternion quaternion = _arrow.transform.rotation;
            switch (move)
            {
                case PartsOfBattleMoves.Nothing:
                    _arrow.enabled = false;
                    break;
                case PartsOfBattleMoves.Right:
                    _arrow.enabled = true;
                    quaternion.eulerAngles = new Vector3(quaternion.eulerAngles.x, quaternion.eulerAngles.y, 180f); 
                    _arrow.transform.rotation = quaternion;
                    break;
                case PartsOfBattleMoves.Left:
                    _arrow.enabled = true;
                    quaternion.eulerAngles = new Vector3(quaternion.eulerAngles.x, quaternion.eulerAngles.y, 0f);
                    _arrow.transform.rotation = quaternion;
                    break;
                case PartsOfBattleMoves.Up:
                    _arrow.enabled = true;
                    quaternion.eulerAngles = new Vector3(quaternion.eulerAngles.x, quaternion.eulerAngles.y, -90f);
                    _arrow.transform.rotation = quaternion;
                    break;
                default:
                    _arrow.enabled = false;
                    break;
            }
        }


    }
}