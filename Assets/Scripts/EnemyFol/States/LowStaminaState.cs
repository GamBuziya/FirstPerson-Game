using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace.Enemy.States
{
    public class LowStaminaState : BaseState
    {
        private float _speed = 1f;

        public override void Enter()
        {
        }
        

        public override void Perform()
        {
            if (SwordEnemy != null && StateMachine != null && Player != null && SwordEnemy.GetCurrentStamina() < 30)
            {
                var transform = Player.transform;
                Vector3 directionToPlayer = transform.position - SwordEnemy.transform.position;
                
                SwordEnemy.transform.LookAt(transform);
                
                directionToPlayer.Normalize();
                
                SwordEnemy.transform.Translate(-directionToPlayer * (_speed * Time.deltaTime), Space.World);
            }
            else
            {
                StateMachine.ChangeState(new PeaseState());
            }
        }

        public override void Exit()
        {
        }
    }
}