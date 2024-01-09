using System;
using System.Security.Cryptography.X509Certificates;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using DefaultNamespace.NonMonobehaviourClasses;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PlayerBattleController : BattleController
    {
        [SerializeField] private int _basicAttackStaminaCost = 30;
        [SerializeField] private int _forceAttackStaminaCost = 40;

        public PlayerAnimation Animation; //Треба отримати
        public StaminaController StaminaController; //Підійде для Enemy
        private MoveParametrsController _battleMoves;
        private bool _force = false;


        //Треба мати назву удару що він зараз буде робити, щоб мати можливість в майбутньому парувати її
        public PartsOfBattleMoves _currentMove = PartsOfBattleMoves.Nothing;
        public TypeOfMove _currentTypeOfMove = TypeOfMove.Nothing;

        
        public void UpdateMousePosition(Vector2 readValue)
        {
            _battleMoves.UpdateSide(readValue);
        }

        public PlayerBattleController(PlayerAnimation Animation, StaminaController StaminaController)
        {
            this.Animation = Animation;
            this.StaminaController = StaminaController;
            _battleMoves = new MoveParametrsController();
        }
        

        public override void Attack()
        {
            if (Animation.Animator == null) return;

            if (_force)
            {
                if (StaminaController.Stamina >= _forceAttackStaminaCost)
                {
                    _battleMoves.GetMoveParametrs(true, _force, out _currentMove, out _currentTypeOfMove);
                    Animation.PlayAnimation(_currentMove, _currentTypeOfMove);
                    StaminaController.StaminaDamage(_forceAttackStaminaCost);
                }
            }
            else
            {
                if (StaminaController.Stamina >= _basicAttackStaminaCost)
                {
                    _battleMoves.GetMoveParametrs(true, _force, out _currentMove, out _currentTypeOfMove);
                    Animation.PlayAnimation(_currentMove, _currentTypeOfMove);
                    StaminaController.StaminaDamage(_basicAttackStaminaCost);
                }
            }

        }

        public override void Block()
        {
            if (Animation.Animator == null) return;

            _battleMoves.GetMoveParametrs(false, _force, out _currentMove, out _currentTypeOfMove);
            Animation.PlayAnimation(_currentMove, _currentTypeOfMove);
        }


        public void ResetBlock()
        {
            Animation.ResetBlock();
        }

        public void ResetMoves()
        {
            _currentMove = PartsOfBattleMoves.Nothing;
            _currentTypeOfMove = TypeOfMove.Nothing;
        }

        public void ChangeForce()
        {
            _force = !_force;
        }

        public void SetAnimator(Animator animator)
        {
            Animation.Animator = animator;
        }
        }
}