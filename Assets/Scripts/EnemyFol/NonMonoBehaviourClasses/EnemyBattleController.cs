using System;
using System.Threading.Tasks;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;
using Update = Unity.VisualScripting.Update;

namespace DefaultNamespace.Enemy
{
    public class EnemyBattleController : BattleController
    {
        private SideOfMove _enemySideOfMove;
        private TypeOfMove _enemyTypeOfMove;
        
        private bool _isBlock = false;
        private float _time;
        private float _stunTime = 0;

        private GameCharacter _enemy;
        public EnemyBattleController(GameCharacter gameCharacter, float time, GameCharacter player)
        {
            _time = time;
            _gameCharacter = gameCharacter; 
            _animator = gameCharacter.GetAnimatorManager();
            //StaminaController = enemy.GetStamina();
            _enemySideOfMove = player.GetBattleController().GetCurrentMove();
            _enemyTypeOfMove = player.GetBattleController().GetCurrentTypeOfMove();
  
            _enemy = player;
        }

        public async void BattleControllerUpdate()
        {
            _enemySideOfMove = _enemy.GetBattleController().GetCurrentMove();
            _enemyTypeOfMove = _enemy.GetBattleController().GetCurrentTypeOfMove();

            var enemy = (global::Enemy)_gameCharacter;
            if (_enemyTypeOfMove == TypeOfMove.IsAttack && !_isBlock && enemy.CanSee() && !enemy.GetStun())
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
            
            if (enemy.GetStun())
            {
                _stunTime += Time.deltaTime;
                Debug.Log(_stunTime);
                if (_stunTime > 1f)
                {
                    Debug.Log("StunGetOut");
                    enemy.SetStun(false);
                    _stunTime = 0;
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
            
            if (randomMove == SideOfMove.Up && _gameCharacter.GetCurrentStamina() >= _forceAttackStaminaCost)
            {
                Debug.Log("aaaaaaaa");
                SetData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                _animator.PlayFightAnimation(randomMove, TypeOfMove.IsAttack);
                _gameCharacter.StaminaDamage(_forceAttackStaminaCost);
            }
            else if (randomMove == SideOfMove.Up && _gameCharacter.GetCurrentStamina() >= _basicAttackStaminaCost)
            {
                Debug.Log("aaaaaaaa");
                randomMove = (SideOfMove)Random.Range(1, 3);
                SetData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                _animator.PlayFightAnimation(randomMove, TypeOfMove.IsAttack);
                _gameCharacter.StaminaDamage(_basicAttackStaminaCost);
            }
            else if (randomMove != SideOfMove.Up && _gameCharacter.GetCurrentStamina() >= _basicAttackStaminaCost)
            {
                Debug.Log("aaaaaaaa");
                SetData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                _animator.PlayFightAnimation(randomMove, TypeOfMove.IsAttack);
                _gameCharacter.StaminaDamage(_basicAttackStaminaCost);
            }
        }
        


        private async Task SetCorrectBlock(SideOfMove side)
        {
            
            if (side == SideOfMove.Up)
            {
                _animator.PlayFightAnimation(side, TypeOfMove.IsBlock);
                SetData(side, TypeOfMove.IsBlock);
                await Task.Delay(TimeSpan.FromSeconds(0.5f));
                ResetBlock();
            }
            else if(side == SideOfMove.Left )
            {
                _animator.PlayFightAnimation(SideOfMove.Right, TypeOfMove.IsBlock);
                SetData(SideOfMove.Right, TypeOfMove.IsBlock);
                await Task.Delay(TimeSpan.FromSeconds(0.5f));
                ResetBlock();
            }
            else
            {
                _animator.PlayFightAnimation(SideOfMove.Left, TypeOfMove.IsBlock);
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
            _animator.ResetBlock();
            _isBlock = false;
        }
        
    }
}