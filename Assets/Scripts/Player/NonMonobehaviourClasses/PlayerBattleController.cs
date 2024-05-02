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
        
        public PlayerBattleController(Player player)
        {
            _gameCharacter = player;
            _animator = _gameCharacter.GetAnimatorManager();
            _battleMoves = new MoveParametrsController();
        }
        
        public void UpdateMousePosition(Vector2 readValue)
        {
            _battleMoves.UpdateSide(readValue);
        }

        public override void Attack()
        {
            if (_animator.GetAnimator() == null) return;

            if (_force)
            {
                if (_gameCharacter.CurrentStamina >= _forceAttackStaminaCost)
                {
                    _battleMoves.GetMoveParametrs(true, _force, out _currentMove, out _currentTypeOfMove);
                    _animator.PlayFightAnimation(_currentMove, _currentTypeOfMove);
                    _gameCharacter.StaminaDamage(_forceAttackStaminaCost);
                }
            }
            else
            {
                if (_gameCharacter.CurrentStamina >= _basicAttackStaminaCost)
                {
                    _battleMoves.GetMoveParametrs(true, _force, out _currentMove, out _currentTypeOfMove);
                    _animator.PlayFightAnimation(_currentMove, _currentTypeOfMove);
                    _gameCharacter.StaminaDamage(_basicAttackStaminaCost);
                }
            }

        }

        public void Block()
        {
            if (_animator.GetAnimator() == null) return;

            _battleMoves.GetMoveParametrs(false, _force, out _currentMove, out _currentTypeOfMove);
            _animator.PlayFightAnimation(_currentMove, _currentTypeOfMove);
        }


        public void ResetBlock()
        {
            _animator.ResetBlock();
        }
        
        
        public void SetAnimator(Animator animator)
        {
            _animator.SetAnimator(animator);
        }
    }
}