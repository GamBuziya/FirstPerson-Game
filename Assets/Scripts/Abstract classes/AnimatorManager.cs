using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class AnimatorManager : MonoBehaviour
    {
        protected Animator Animator;
        protected GameCharacter _person;

        public Animator GetAnimator() => Animator;
        public void SetAnimator(Animator animator) => Animator = animator;
        
        public void PlayAnimation(SideOfMove sideOfMove, TypeOfMove typeOfMove)
        {
            if(Animator == null) return;
            Animator.SetBool(sideOfMove.ToString(), true);
            Animator.SetBool(typeOfMove.ToString(), true);
        }
        
        public void ResetBlock()
        {
            if (Animator == null) return;
            Animator.SetBool("IsBlock", false);
            _person.GetBattleController().ResetMoves();
            ResetParts();
        }

        public void ResetAttack()
        {
            Animator.SetBool("IsAttack", false);
            _person.GetBattleController().ResetMoves();
            ResetParts();
        }

        public void ResetParts()
        {
            Animator.SetBool(SideOfMove.Left.ToString(), false);
            Animator.SetBool(SideOfMove.Right.ToString(), false);
            Animator.SetBool(SideOfMove.Up.ToString(), false);
        }

        public void EnemyParriedEffect()
        {
            if (_person is global::Enemy enemy)
            {
                enemy.GetEnemySideAttackUI().DisableUI();
            }
            Animator.SetBool("IsParried", true);
            ResetAttack();
        }
        
        
        
    }
}