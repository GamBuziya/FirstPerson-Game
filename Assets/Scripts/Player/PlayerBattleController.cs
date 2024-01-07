﻿using System;
using System.Security.Cryptography.X509Certificates;
using DefaultNamespace.Enums;
using DefaultNamespace.NonMonobehaviourClasses;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBattleController : MonoBehaviour
    {
        [SerializeField] private int _basicAttackStaminaCost = 30;
        [SerializeField] private int _forceAttackStaminaCost = 40;
        
        public PlayerAnimation Animation;
        public PlayerStamina Stamina;
        private MoveParametrsController _battleMoves;
        private bool _force = false;
        
        
        //Треба мати назву удару що він зараз буде робити, щоб мати можливість в майбутньому парувати її
        public PartsOfBattleMoves _currentMove = PartsOfBattleMoves.Nothing;
        public TypeOfMove _currentTypeOfMove = TypeOfMove.Nothing;
        

        public void UpdateMousePosition(Vector2 readValue)
        {
            _battleMoves.UpdateSide(readValue);
        }
        
        private void Awake()
        {
            Animation = GetComponent<PlayerAnimation>();
            Stamina = GetComponent<PlayerStamina>();
            _battleMoves = new MoveParametrsController();
        }
        public void Attack()
        {
            if(Animation == null) return;
            
            if (_force)
            {
                if (Stamina.Stamina >= _forceAttackStaminaCost)
                {
                    _battleMoves.GetMoveParametrs(true, _force, out _currentMove, out _currentTypeOfMove);
                    Animation.PlayAnimation(_currentMove, _currentTypeOfMove);
                    Stamina.StaminaDamage(_forceAttackStaminaCost);
                }
            }
            else
            {
                if(Stamina.Stamina >= _basicAttackStaminaCost)
                {
                    _battleMoves.GetMoveParametrs(true, _force, out _currentMove, out _currentTypeOfMove);
                    Animation.PlayAnimation(_currentMove, _currentTypeOfMove);
                    Stamina.StaminaDamage(_basicAttackStaminaCost);
                }
            }
            
        }
        public void Block()
        {
            if(Animation == null) return;
            
            _battleMoves.GetMoveParametrs(false, _force,  out _currentMove, out _currentTypeOfMove);
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