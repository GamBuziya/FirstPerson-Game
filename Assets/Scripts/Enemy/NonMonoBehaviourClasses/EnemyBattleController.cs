﻿using System;
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
        private int counter = 0;
        public EnemyBattleController(AnimatorManager Animation, StaminaController StaminaController, float time)
        {
            this.Animation = Animation;
            this.StaminaController = StaminaController;
            _time = time;
        }
        
        public override async void Attack()
        {
            var randomMove = (PartsOfBattleMoves)Random.Range(1, 4);
            await DelayedAttack(randomMove);
        }
        
        private async Task DelayedAttack(PartsOfBattleMoves randomMove)
        {
            float delayTime = _time;

            if (randomMove == PartsOfBattleMoves.Up && StaminaController.Stamina >= _forceAttackStaminaCost)
            {
                counter++;
                Debug.Log("A " + counter++);
                SetAttackData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(delayTime));
                Animation.PlayAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_forceAttackStaminaCost);
            }
            else if (randomMove == PartsOfBattleMoves.Up && StaminaController.Stamina >= _basicAttackStaminaCost)
            {
                SetAttackData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(delayTime));
                Animation.PlayAnimation((PartsOfBattleMoves)Random.Range(1, 3), TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_basicAttackStaminaCost);
            }
            else if (randomMove != PartsOfBattleMoves.Up && StaminaController.Stamina >= _basicAttackStaminaCost)
            {
                SetAttackData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(delayTime));
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