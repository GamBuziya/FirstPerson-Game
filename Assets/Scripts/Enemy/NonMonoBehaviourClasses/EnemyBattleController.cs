using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Enemy
{
    public class EnemyBattleController : BattleController
    {
        
        
        public EnemyBattleController(AnimatorManager Animation, StaminaController StaminaController)
        {
            this.Animation = Animation;
            this.StaminaController = StaminaController;
        }
        
        public override void Attack()
        {
            var randomMove = (PartsOfBattleMoves)Random.Range(1, 4);
            
            
            if (randomMove == PartsOfBattleMoves.Up && StaminaController.Stamina >= _forceAttackStaminaCost)
            {
                Animation.PlayAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_forceAttackStaminaCost);
                return;
            }

            if (randomMove == PartsOfBattleMoves.Up && StaminaController.Stamina >= _basicAttackStaminaCost)
            {
                Animation.PlayAnimation((PartsOfBattleMoves)Random.Range(1, 3), TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_basicAttackStaminaCost);
            }
            
            if (randomMove != PartsOfBattleMoves.Up && StaminaController.Stamina >= _basicAttackStaminaCost)
            {
                Animation.PlayAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_basicAttackStaminaCost);
            }
            
        }

        public override void Block()
        {
            throw new System.NotImplementedException();
        }
    }
}