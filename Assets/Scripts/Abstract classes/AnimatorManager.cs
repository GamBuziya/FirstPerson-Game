using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class AnimatorManager : MonoBehaviour
    {
        protected Animator WeaponAnimator;
        protected GameCharacter _person;

        public Animator GetAnimator() => WeaponAnimator;
        public void SetAnimator(Animator animator) => WeaponAnimator = animator;
        
        public void PlayFightAnimation(SideOfMove sideOfMove, TypeOfMove typeOfMove)
        {
            if(WeaponAnimator == null) return;
            WeaponAnimator.SetBool(sideOfMove.ToString(), true);
            WeaponAnimator.SetBool(typeOfMove.ToString(), true);
        }
        
        
        public void ResetBlock()
        {
            if (WeaponAnimator == null) return;
            WeaponAnimator.SetBool("IsBlock", false);
            _person.GetBattleController().ResetMoves();
            ResetParts();
        }

        public void ResetAttack()
        {
            WeaponAnimator.SetBool("IsAttack", false);
            _person.GetBattleController().ResetMoves();
            ResetParts();
        }

        public void ResetParts()
        {
            WeaponAnimator.SetBool(SideOfMove.Left.ToString(), false);
            WeaponAnimator.SetBool(SideOfMove.Right.ToString(), false);
            WeaponAnimator.SetBool(SideOfMove.Up.ToString(), false);
        }

        public void EnemyParriedEffect()
        {
            if (_person is global::Enemy enemy)
            {
                enemy.GetEnemySideAttackUI().DisableUI();
            }
            WeaponAnimator.SetBool("IsParried", true);
            ResetAttack();
        }
        
        
        
    }
}