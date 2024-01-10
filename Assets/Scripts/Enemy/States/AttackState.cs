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
                    StateMachine.ChangeState(new PatrolState());
                }
            }
        }

        public override void Exit()
        {
        }

        private void Attack()
        {
            if (Vector3.Distance(Enemy.gameObject.transform.position, Enemy.Player.transform.position) <= 2.3f)
            {
                Enemy.Agent.speed = 0f;
                _attackTimer += Time.deltaTime;
                    
                if (_attackTimer > 0.5f + Random.Range(0, 0.2f))
                {
                    Debug.Log("A");
                    StateMachine.Enemy.BattleController.Attack();
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