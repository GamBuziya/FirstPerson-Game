using System;
using System.Security.Cryptography.X509Certificates;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using DefaultNamespace.NonMonobehaviourClasses;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PlayerBattleController : BattleController
    {
        private MoveParametrsController _battleMoves;
        
        public PlayerBattleController(PlayerAnimation Animation, StaminaController StaminaController)
        {
            this.Animation = Animation;
            this.StaminaController = StaminaController;
            _battleMoves = new MoveParametrsController();
        }
        
        public void UpdateMousePosition(Vector2 readValue)
        {
            _battleMoves.UpdateSide(readValue);
        }

        public override void Attack()
        {
            if (Animation.GetAnimator() == null) return;

            if (_force)
            {
                if (StaminaController.Stamina >= _forceAttackStaminaCost)
                {
                    _battleMoves.GetMoveParametrs(true, _force, out _currentMove, out _currentTypeOfMove);
                    Animation.PlayFightAnimation(_currentMove, _currentTypeOfMove);
                    StaminaController.StaminaDamage(_forceAttackStaminaCost);
                }
            }
            else
            {
                if (StaminaController.Stamina >= _basicAttackStaminaCost)
                {
                    _battleMoves.GetMoveParametrs(true, _force, out _currentMove, out _currentTypeOfMove);
                    Animation.PlayFightAnimation(_currentMove, _currentTypeOfMove);
                    StaminaController.StaminaDamage(_basicAttackStaminaCost);
                }
            }

        }

        public void Block()
        {
            if (Animation.GetAnimator() == null) return;

            _battleMoves.GetMoveParametrs(false, _force, out _currentMove, out _currentTypeOfMove);
            Animation.PlayFightAnimation(_currentMove, _currentTypeOfMove);
        }


        public void ResetBlock()
        {
            Animation.ResetBlock();
        }
        
        
        public void SetAnimator(Animator animator)
        {
            Animation.SetAnimator(animator);
        }
    }
}