using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class PlayerAnimation : MonoBehaviour, IAnimationReset
    {
        public Animator Animator;
        public bool isAttack = false;
        public bool secondAttack = false;
        public int StaminaCost = 30;

        private PlayerStamina _stamina;


        private void Awake()
        {
            _stamina = GetComponent<PlayerStamina>();
        }

        public void Attack()
        {
            if(Animator == null) return;
            if (!isAttack && _stamina.Stamina >= StaminaCost)
            {
                Animator.SetBool("FirstAttack", true);
                isAttack = true;
                return;
            }
            
            if (isAttack && _stamina.Stamina >= 2*StaminaCost)
            {
                Animator.SetBool("SecondAttack", true);
                secondAttack = true;
            }

        }

        public void Block()
        {
            if(Animator == null) return;
            Animator.SetBool("Block", true);
        }


        public void ResetBlock()
        {
            Animator.SetBool("Block", false);
        }

        public void ResetFirstAttack()
        {
            _stamina.StaminaDamage(StaminaCost);
            if (!secondAttack)
            {
                Animator.SetBool("FirstAttack", false);
                isAttack = false;
                
            }
        }

        public void ResetSecondAttack()
        {
            _stamina.StaminaDamage(StaminaCost);
            Animator.SetBool("FirstAttack", false);
            Animator.SetBool("SecondAttack", false);
            isAttack = false;
            secondAttack = false;
        }
    }
}