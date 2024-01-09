using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class AnimatorManager : MonoBehaviour
    {
        public Animator Animator;
        protected Player _person; // Зробити абстрактно
        
        public void PlayAnimation(PartsOfBattleMoves partsOfBattleMoves, TypeOfMove typeOfMove)
        {
            if(Animator == null) return;
            Animator.SetBool(partsOfBattleMoves.ToString(), true);
            Animator.SetBool(typeOfMove.ToString(), true);
        }
        
        public void ResetBlock()
        {
            if (Animator == null) return;
            Animator.SetBool("IsBlock", false);
            _person.BattleController.ResetMoves();
            ResetParts();
        }

        public void ResetAttack()
        {
            Animator.SetBool("IsAttack", false);
            _person.BattleController.ResetMoves();
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