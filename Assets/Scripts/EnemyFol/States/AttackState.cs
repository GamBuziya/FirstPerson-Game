using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace.Enemy.States
{
    public class AttackState : BaseState
    {
        private float _moveTimer;
        private float _losePlayerTimer;
        private float _attackTimer = 0f;
        public override void Enter()
        {
        }

        public override void Perform()
        {
            if (Enemy.CanSee())
            {
                Enemy.GetCanvasDisabler().CanvasEnable();
                Attack();
                _losePlayerTimer = 0;
                _moveTimer += Time.deltaTime;
                if (_moveTimer >= Random.Range(0, 1f))
                {
                    Enemy.Agent.SetDestination(Enemy.Player.transform.position);
                }
            }
            else
            {
                _losePlayerTimer += Time.deltaTime;
                if (_losePlayerTimer > 7)
                {
                    StateMachine.ChangeState(new PeaseState());
                    Enemy.GetCanvasDisabler().CanvasDisabled();
                }
            }
        }

        public override void Exit()
        {
        }

        private void Attack()
        {
            if(Enemy.GetStamina().Stamina < 30) StateMachine.ChangeState(new LowStaminaState());
            
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