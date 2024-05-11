using System;
using DefaultNamespace.Enums;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Enemy.States
{
    public class AttackState : BaseState
    {
        private float _moveTimer;
        private float _attackTimer = 0f;
        public override void Enter()
        {
        }

        public override void Perform()
        {
            GameCharacter.GetCanvasDisabler().CanvasEnable();
            Attack();
            GameCharacter.Agent.SetDestination(GameCharacter.Player.transform.position);
        }
        
        public override void Exit()
        {
        }
        
        private float timer = 0f;
        private float interval = 3f; 
        
        
        
        private void Attack()
        {
            if(GameCharacter.GetCurrentStamina() < 30) StateMachine.ChangeState(new LowStaminaState());

            var closestEnemie = EnemiesManager.Instance.GetClosestEnemy(GameCharacter as SwordEnemy);
            
            var transform = Player.transform;
            GameCharacter.transform.LookAt(transform);
            
            
            if (Vector3.Distance(GameCharacter.transform.position, closestEnemie.transform.position) < 5f
                && Vector3.Distance(GameCharacter.transform.position, Player.transform.position) > 2f)
            {
                GameCharacter.Agent.speed = 2f;
                var tempZ = 0;
                
                
                Vector3 directionToPlayer = (Player.transform.position - GameCharacter.transform.position).normalized;

                Vector3 directionToLeft = Quaternion.Euler(0, -90 + tempZ , tempZ) * directionToPlayer; // Поворот на 90 градусів ліворуч
                Vector3 directionToRight = Quaternion.Euler(0, 90 + tempZ, tempZ) * directionToPlayer; // Поворот на 90 градусів праворуч

                Vector3 averageDirection;


                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    tempZ = Random.Range(-60, 60);
                    var rand = Random.Range(0, 3);
                    if (rand == 0)
                    {
                        Side = SideToGo.forward;
                    }
                    else if (rand == 1)
                    {
                        Side = SideToGo.left;
                    }
                    else
                    {
                        Side = SideToGo.right;
                    }

                    timer = 0;
                }


                switch (Side)
                {
                    case SideToGo.forward:
                        averageDirection = Quaternion.Euler(0,  tempZ, tempZ) * directionToPlayer;
                        break;
                    case SideToGo.left:
                        averageDirection = 2 * directionToLeft + directionToPlayer;
                        break;
                    case SideToGo.right:
                        averageDirection = 2 * directionToRight + directionToPlayer;
                        break;
                    default:
                        averageDirection = Quaternion.Euler(0,  tempZ, tempZ) * directionToPlayer;
                        break;
                }

                GameCharacter.transform.Translate(averageDirection * Time.deltaTime, Space.World);
                return;
            }



            if (Vector3.Distance(GameCharacter.gameObject.transform.position, GameCharacter.Player.transform.position) <= 2.3f)
            {
                GameCharacter.Agent.speed = 0f;
                _attackTimer += Time.deltaTime;

                if (StateMachine.gameCharacter.GetBattleController().GetCurrentTypeOfMove() == TypeOfMove.IsBlock) _attackTimer = 0;


                if (_attackTimer > Random.Range(0, 0.3f) && StateMachine.gameCharacter.GetBattleController().GetCurrentTypeOfMove() == TypeOfMove.Nothing)
                {
                    StateMachine.gameCharacter.GetBattleController().Attack();
                    _attackTimer = 0;
                }

            }
            else
            {
                GameCharacter.Agent.speed = 2f;
            }
        }

    }
}