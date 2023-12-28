using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBattleController : MonoBehaviour
    {
        public PlayerAnimation Animation;
        public PlayerStamina Stamina;
        private bool _force;

        private void Awake()
        {
            Animation = GetComponent<PlayerAnimation>();
            Stamina = GetComponent<PlayerStamina>();
        }
        public void Attack()
        {
            Animation.Attack();
        }

        public void Block()
        {
            Animation.Block();
        }

        public void ResetBlock()
        {
            Animation.ResetBlock();
        }

        public void ActivatePower()
        {
            //Костили переробити force нормально
            Animation.ChangePower();
        }

        public void DeactivatePower()
        {
            //Костили переробити force нормально
            Animation.ChangePower();
        }
        
        public void SetAnimator(Animator animator)
        {
            Animation.Animator = animator;
        }
    }
}