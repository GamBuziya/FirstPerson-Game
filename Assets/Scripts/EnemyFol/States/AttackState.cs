using DefaultNamespace.Enums;
using UnityEngine;

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

        private void Attack()
        {
            if(Enemy.GetCurrentStamina() < 30) StateMachine.ChangeState(new LowStaminaState());
            
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