using System;
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

        public void PlayAnimation(PartsOfBattleMoves partsOfBattleMoves, TypeOfMove typeOfMove)
        {
            Animator.SetBool(partsOfBattleMoves.ToString(), true);
            Animator.SetBool(typeOfMove.ToString(), true);
        }
        
        
        public void ResetBlock()
        {
            Animator.SetBool("IsBlock", false);
            ResetParts();
        }

        public void ResetAttack()
        {
            Animator.SetBool("IsAttack", false);
            ResetParts();
        }

        public void ResetParts()
        {
            Animator.SetBool("Left", false);
            Animator.SetBool("Right", false);
            Animator.SetBool("Up", false);
        }
        
    
    }
}