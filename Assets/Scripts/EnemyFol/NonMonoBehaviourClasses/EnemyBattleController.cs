using System;
using System.Threading.Tasks;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;
using Update = Unity.VisualScripting.Update;

namespace DefaultNamespace.Enemy
{
    public class EnemyBattleController : BattleController
    {
        private global::Enemy enemy;
        private SideOfMove _enemySideOfMove;
        private TypeOfMove _enemyTypeOfMove;
        
        private bool IsBlock = false;
        private float _time;
        
        public EnemyBattleController(global::Enemy enemy, float time, GameCharacter player)
        {
            Debug.Log(player.name);
            this.enemy = enemy; 
            this.Animation = enemy.GetAnimatorManager();
            this.StaminaController = enemy.GetStamina();
            _enemySideOfMove = player.GetBattleController().GetCurrentMove();
            _enemyTypeOfMove = player.GetBattleController().GetCurrentTypeOfMove();
            _time = time;
        }

        public void BattleControllerUpdate()
        {
            if (_enemyTypeOfMove == TypeOfMove.IsAttack && !IsBlock)
            {
                IsBlock = true;
                var temp = Random.Range(0, 4);
                if (temp < 3)
                {
                    SetCorrectBlock(_enemySideOfMove);
                }
                else
                {
                    SetCorrectBlock((SideOfMove)Random.Range(1,4));
                }
            }
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


        private void SetCorrectBlock(SideOfMove side)
        {
            if (side == SideOfMove.Up)
            {
                Animation.PlayAnimation(side, TypeOfMove.IsBlock);
            }
            else if(side == SideOfMove.Left )
            {
                Animation.PlayAnimation(SideOfMove.Right, TypeOfMove.IsBlock);
            }
            else
            {
                Animation.PlayAnimation(SideOfMove.Left, TypeOfMove.IsBlock);
            }
        }

        private void SetAttackData(SideOfMove move, TypeOfMove type)
        {
            _currentMove = move;
            _currentTypeOfMove = type;
        }
        
    }
}