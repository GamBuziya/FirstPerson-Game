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
            this.Animation = enemy.GetAnimatorManager();
            this.StaminaController = enemy.GetStamina();
            _time = time;
        }
        
        public override async void Attack()
        {
            SideOfMove randomMove;
            if(_currentMove == SideOfMove.Nothing)  randomMove = (SideOfMove)Random.Range(1, 4);
            else randomMove = _currentMove;
            await DelayedAttack(randomMove);
        }
        
        private async Task DelayedAttack(SideOfMove randomMove)
        {
            float delayTime = _time;

            if (enemy.GetStun())
            {
                enemy.GetBattleController().ResetMoves();
                await Task.Delay(TimeSpan.FromSeconds(delayTime));
                enemy.SetStun(false);
                return;
            }
            
            
            if (randomMove == SideOfMove.Up && StaminaController.Stamina >= _forceAttackStaminaCost)
            {
                SetAttackData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                Animation.PlayAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_forceAttackStaminaCost);
            }
            else if (randomMove == SideOfMove.Up && StaminaController.Stamina >= _basicAttackStaminaCost)
            {
                randomMove = (SideOfMove)Random.Range(1, 3);
                SetAttackData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                Animation.PlayAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_basicAttackStaminaCost);
            }
            else if (randomMove != SideOfMove.Up && StaminaController.Stamina >= _basicAttackStaminaCost)
            {
                SetAttackData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                Animation.PlayAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_basicAttackStaminaCost);
            }
        }

        private void SetAttackData(SideOfMove move, TypeOfMove type)
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