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
        public int StaminaCost = 30;
        
        private float _mouseX = 0;
        private float _prevMouseX = 0;
        private bool _power = false;


        private PlayerStamina _stamina;
        


        private void Awake()
        {
            _stamina = GetComponent<PlayerStamina>();
        }

        public void UpdateSide(Vector2 input)
        {
            input.Normalize();
            _prevMouseX = _mouseX;
            _mouseX -= MoreAccuracy(input.x);
        }
            
        public void Attack()
        {
            ChangeAnimation(true);
        }
        
        public void Block()
        {
            ChangeAnimation(false);
        }
        
        private void ChangeAnimation(bool isAttack)
        {
            float deltaX = _mouseX - _prevMouseX;
            deltaX = MoreAccuracy(deltaX);

            if (Animator == null) return;
            if (true)
            {
                if(isAttack) Animator.SetBool(TypeOfMove.IsAttack.ToString(), true);
                else Animator.SetBool(TypeOfMove.IsBlock.ToString(), true);
                
                
                if (_power)
                {
                    Animator.SetBool(PartsOfBattleMoves.Up.ToString(), true);
                    return;
                }

                if (isAttack)
                {
                    if (deltaX > 0)
                    {
                        Animator.SetBool(PartsOfBattleMoves.Right.ToString(), true);
                    }
                    else
                    {
                        Animator.SetBool(PartsOfBattleMoves.Left.ToString(), true);
                    }
                }
                else
                {
                    if (deltaX < 0)
                    {
                        Animator.SetBool(PartsOfBattleMoves.Right.ToString(), true);
                    }
                    else
                    {
                        Animator.SetBool(PartsOfBattleMoves.Left.ToString(), true);
                    }
                }
                
            }
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
        
        
        public float MoreAccuracy(float num)
        {
            return num * Time.deltaTime * 100;
        }
        
        public void ChangePower()
        {
            _power = !_power;
        }
    
    }
}