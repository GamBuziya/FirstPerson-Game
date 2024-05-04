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
            //Робимо вигляд що отримали усіх найближчих ворогів
            Enemy.GetCanvasDisabler().CanvasEnable();
            Attack();
            Enemy.Agent.SetDestination(Enemy.Player.transform.position);
        }

        public override void Exit()
        {
        }
        
        private float timer = 0f;
        private float interval = 5f; 
        private void Attack()
        {
            if(Enemy.GetCurrentStamina() < 30) StateMachine.ChangeState(new LowStaminaState());

            var closestEnemie = EnemiesManager.Instance.GetClosestEnemy(Enemy);
            
            if (Vector3.Distance(Enemy.transform.position, closestEnemie.transform.position) < 5f
                && Vector3.Distance(Enemy.transform.position, Player.transform.position) > 2f)
            {
                
                var tempZ = 0;
                var transform = Player.transform;
                Enemy.transform.LookAt(transform);
                Vector3 directionToPlayer = (Player.transform.position - Enemy.transform.position).normalized;
                
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
                        averageDirection = directionToLeft + directionToPlayer;
                        break;
                    case SideToGo.right:
                        averageDirection = directionToRight + directionToPlayer;
                        break;
                    default:
                        averageDirection = Quaternion.Euler(0,  tempZ, tempZ) * directionToPlayer;
                        break;
                }
                
                Debug.Log("Side" + Side);
                
                Enemy.transform.Translate(averageDirection * Time.deltaTime, Space.World);
                return;
            }
            
            
            
            if (Vector3.Distance(Enemy.gameObject.transform.position, Enemy.Player.transform.position) <= 2.3f)
            {
                Enemy.Agent.speed = 0f;
                _attackTimer += Time.deltaTime;
                
                if (StateMachine.Enemy.GetBattleController().GetCurrentTypeOfMove() == TypeOfMove.IsBlock) _attackTimer = 0;
                    
                
                if (_attackTimer > Random.Range(0, 0.3f) && StateMachine.Enemy.GetBattleController().GetCurrentTypeOfMove() == TypeOfMove.Nothing)
                {
                    StateMachine.Enemy.GetBattleController().Attack();
                    _attackTimer = 0;
                }
                    
            }
            else
            {
                Enemy.Agent.speed = 2.5f;
            }
        }

    }
}