using System;
using System.Threading.Tasks;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Enemy
{
    public class EnemyBattleController : BattleController
    {
        private float _time;
        private global::Enemy enemy;
        public EnemyBattleController(global::Enemy enemy, float time)
        {
            this.enemy = enemy; 
            this.Animation = enemy.Animator;
            this.StaminaController = enemy.Stamina;
            _time = time;
        }
        
        public override async void Attack()
        {
            PartsOfBattleMoves randomMove;
            if(_currentMove == PartsOfBattleMoves.Nothing)  randomMove = (PartsOfBattleMoves)Random.Range(1, 4);
            else randomMove = _currentMove;
            await DelayedAttack(randomMove);
        }
        
        private async Task DelayedAttack(PartsOfBattleMoves randomMove)
        {
            float delayTime = _time;

            if (enemy.IsStun)
            {
                enemy.BattleController.ResetMoves();
                await Task.Delay(TimeSpan.FromSeconds(delayTime));
                enemy.IsStun = false;
                return;
            }
            
            
            if (randomMove == PartsOfBattleMoves.Up && StaminaController.Stamina >= _forceAttackStaminaCost)
            {
                SetAttackData(randomMove, TypeOfMove.IsAttack);
                Debug.Log("randomMove до" + randomMove);
                await Task.Delay(TimeSpan.FromSeconds(3));
                Debug.Log("randomMove після" + randomMove);
                Animation.PlayAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_forceAttackStaminaCost);
            }
            else if (randomMove == PartsOfBattleMoves.Up && StaminaController.Stamina >= _basicAttackStaminaCost)
            {
                randomMove = (PartsOfBattleMoves)Random.Range(1, 3);
                SetAttackData(randomMove, TypeOfMove.IsAttack);
                Debug.Log("randomMove до" + randomMove);
                await Task.Delay(TimeSpan.FromSeconds(3));
                Debug.Log("randomMove після" + randomMove);
                Animation.PlayAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_basicAttackStaminaCost);
            }
            else if (randomMove != PartsOfBattleMoves.Up && StaminaController.Stamina >= _basicAttackStaminaCost)
            {
                SetAttackData(randomMove, TypeOfMove.IsAttack);
                Debug.Log("randomMove до" + randomMove);
                await Task.Delay(TimeSpan.FromSeconds(3));
                Debug.Log("randomMove після" + randomMove);
                Animation.PlayAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_basicAttackStaminaCost);
            }
        }

        private void SetAttackData(PartsOfBattleMoves move, TypeOfMove type)
        {
            _currentMove = move;
            _currentTypeOfMove = type;
        }

        public override void Block()
        {
            throw new System.NotImplementedException();
        }
    }
}