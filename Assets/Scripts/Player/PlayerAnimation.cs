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
                float deltaX = _mouseX - _prevMouseX;
                deltaX = MoreAccuracy(deltaX);

                if (Animator == null) return;
                if (true)
                {
                    Animator.SetBool("IsAttack", true);
                    if (_power)
                    {
                        Animator.SetBool("Up", true);
                        return;
                    }
                    
                    if (deltaX > 0)
                    {
                        Animator.SetBool("Right", true);
                    }
                    else
                    {
                        Animator.SetBool("Left", true);
                    }
                }
            }

            public void Block()
            {
                if(Animator == null) return;
                Animator.SetBool("Block", true);
            }


            public void ChangePower()
            {
                
                _power = !_power;
            }
            

            public void ResetBlock()
            {
                Animator.SetBool("Block", false);
            }

            public void ResetAttack()
            {
                //_stamina.StaminaDamage(StaminaCost);
                Animator.SetBool("Left", false);
                Animator.SetBool("Right", false);
                Animator.SetBool("Up", false);
                Animator.SetBool("IsAttack", false);
            }
            
            
            public float MoreAccuracy(float num)
            {
                return num * Time.deltaTime * 100;
            }
        
    }
}