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
    public class EnemyWeaponBattleController : WeaponBattleController
    {
        private SideOfMove _enemySideOfMove;
        private TypeOfMove _enemyTypeOfMove;
        
        private bool _isBlock = false;
        private float _time;
        private float _stunTime = 0;

        private GameCharacter _enemy;
        public EnemyWeaponBattleController(GameCharacter gameCharacter, float time, GameCharacter player)
        {
            _time = time;
            _gameCharacter = gameCharacter; 
            _animator = gameCharacter.GetAnimatorManager();
            _enemySideOfMove = player.GetBattleController().GetCurrentMove();
            _enemyTypeOfMove = player.GetBattleController().GetCurrentTypeOfMove();
  
            _enemy = player;
        }

        public async void BattleControllerUpdate()
        {
            _enemySideOfMove = _enemy.GetBattleController().GetCurrentMove();
            _enemyTypeOfMove = _enemy.GetBattleController().GetCurrentTypeOfMove();

            var tempGameCharacter = (global::SwordEnemy)_gameCharacter;
            if (_enemyTypeOfMove == TypeOfMove.IsAttack 
                && !_isBlock && !tempGameCharacter.GetStun()
                && Vector3.Distance(_gameCharacter.gameObject.transform.position, _enemy.transform.position) < 3)
            {
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
            
            if (tempGameCharacter.GetStun())
            {
                _stunTime += Time.deltaTime;
                
                if (_stunTime > 1f)
                {
                    tempGameCharacter.SetStun(false);
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
                SetData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                _animator.PlayFightAnimation(randomMove, TypeOfMove.IsAttack);
                _gameCharacter.StaminaDamage(_forceAttackStaminaCost);
            }
            else if (randomMove == SideOfMove.Up && _gameCharacter.GetCurrentStamina() >= _basicAttackStaminaCost)
            {
                randomMove = (SideOfMove)Random.Range(1, 3);
                SetData(randomMove, TypeOfMove.IsAttack);
                await Task.Delay(TimeSpan.FromSeconds(_time));
                _animator.PlayFightAnimation(randomMove, TypeOfMove.IsAttack);
                _gameCharacter.StaminaDamage(_basicAttackStaminaCost);
            }
            else if (randomMove != SideOfMove.Up && _gameCharacter.GetCurrentStamina() >= _basicAttackStaminaCost)
            {
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