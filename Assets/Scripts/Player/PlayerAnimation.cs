﻿using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class PlayerAnimation : MonoBehaviour, IAnimationReset
    {
        public Animator Animator;
        private PlayerBattleController _battleController;

        private void Awake()
        {
            _battleController = GetComponent<PlayerBattleController>();
        }

        public void PlayAnimation(PartsOfBattleMoves partsOfBattleMoves, TypeOfMove typeOfMove)
        {
            if(Animator == null) return;
            Animator.SetBool(partsOfBattleMoves.ToString(), true);
            Animator.SetBool(typeOfMove.ToString(), true);
        }
        
        
        public void ResetBlock()
        {
            Animator.SetBool("IsBlock", false);
            _battleController.ResetMoves();
            ResetParts();
        }

        public void ResetAttack()
        {
            Animator.SetBool("IsAttack", false);
            _battleController.ResetMoves();
            ResetParts();
        }

        public void ResetParts()
        {
            Animator.SetBool(PartsOfBattleMoves.Left.ToString(), false);
            Animator.SetBool(PartsOfBattleMoves.Right.ToString(), false);
            Animator.SetBool(PartsOfBattleMoves.Up.ToString(), false);
        }
        
    
    }
}