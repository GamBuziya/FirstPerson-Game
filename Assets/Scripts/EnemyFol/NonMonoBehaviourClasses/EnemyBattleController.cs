using System;
using System.Threading.Tasks;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Enemy
{
    public class EnemyBattleController : BattleController
    {
        private global::Enemy _hero;
        private SideOfMove _enemySideOfMove;
        private TypeOfMove _enemyTypeOfMove;
        
        private bool _isBlock = false;
        private float _time;
        private float _stunTime = 0;

        private GameCharacter _enemy;
        public EnemyBattleController(global::Enemy enemy, float time, GameCharacter player)
        {
            _time = time;
            _hero = enemy; 
            Animation = enemy.GetAnimatorManager();
            StaminaController = enemy.GetStamina();
            _enemySideOfMove = player.GetBattleController().GetCurrentMove();
            _enemyTypeOfMove = player.GetBattleController().GetCurrentTypeOfMove();
  
            _enemy = player;
        }

        public async void BattleControllerUpdate()
        {
            _enemySideOfMove = _enemy.GetBattleController().GetCurrentMove();
            _enemyTypeOfMove = _enemy.GetBattleController().GetCurrentTypeOfMove();
            
            if (_enemyTypeOfMove == TypeOfMove.IsAttack && !_isBlock && _hero.CanSee() && !_hero.GetStun())
            {
                Debug.Log("Block1");
                _isBlock = true;
                var temp = Random.Range(0f, 4f);
                if (temp < 4f)
                {
                    await SetCorrectBlock(_enemySideOfMove);
                }
                else
                {
                    await SetCorrectBlock((SideOfMove)Random.Range(1,4));
                }
            }
            
            if (_hero.GetStun())
            {
                _stunTime += Time.deltaTime;
                Debug.Log(_stunTime);
                if (_stunTime > 1f)
                {
                    Debug.Log("StunGetOut");
                    _hero.SetStun(false);
                    _stunTime = 0;
                }
            }
        }
        public override async void Attack()
        {
            Debug.Log("Enemy Attack");
            SideOfMove randomMove;
            if(_currentMove == SideOfMove.Nothing)  randomMove = (SideOfMove)Random.Range(1, 4);
            else randomMove = _currentMove;
            await DelayedAttack(randomMove);
        }
        
        private async Task DelayedAttack(SideOfMove randomMove)
        {
            float delayTime = _time;

            if (_hero.GetStun())
            {
                //_hero.GetBattleController().ResetMoves();
                //await Task.Delay(TimeSpan.FromSeconds(delayTime));
                //_hero.SetStun(false);
                return;
            }
            
            
            if (randomMove == SideOfMove.Up && StaminaController.Stamina >= _forceAttackStaminaCost)
            {
                SetData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                Animation.PlayFightAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_forceAttackStaminaCost);
            }
            else if (randomMove == SideOfMove.Up && StaminaController.Stamina >= _basicAttackStaminaCost)
            {
                randomMove = (SideOfMove)Random.Range(1, 3);
                SetData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                Animation.PlayFightAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_basicAttackStaminaCost);
            }
            else if (randomMove != SideOfMove.Up && StaminaController.Stamina >= _basicAttackStaminaCost)
            {
                SetData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                Animation.PlayFightAnimation(randomMove, TypeOfMove.IsAttack);
                StaminaController.StaminaDamage(_basicAttackStaminaCost);
            }
        }
        


        private async Task SetCorrectBlock(SideOfMove side)
        {
            
            if (side == SideOfMove.Up)
            {
                Animation.PlayFightAnimation(side, TypeOfMove.IsBlock);
                SetData(side, TypeOfMove.IsBlock);
                await Task.Delay(TimeSpan.FromSeconds(0.5f));
                ResetBlock();
            }
            else if(side == SideOfMove.Left )
            {
                Animation.PlayFightAnimation(SideOfMove.Right, TypeOfMove.IsBlock);
                SetData(SideOfMove.Right, TypeOfMove.IsBlock);
                await Task.Delay(TimeSpan.FromSeconds(0.5f));
                ResetBlock();
            }
            else
            {
                Animation.PlayFightAnimation(SideOfMove.Left, TypeOfMove.IsBlock);
                SetData(SideOfMove.Left, TypeOfMove.IsBlock);
                await Task.Delay(TimeSpan.FromSeconds(0.5f));
                ResetBlock();
            }
        }

        private void SetData(SideOfMove move, TypeOfMove type)
        {
            _currentMove = move;
            _currentTypeOfMove = type;
        }

        private void ResetBlock()
        {
            Animation.ResetBlock();
            _isBlock = false;
        }
        
    }
}