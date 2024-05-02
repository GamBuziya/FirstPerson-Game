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
            if (Enemy != null && StateMachine != null && Player != null && Enemy.GetCurrentStamina() < 30)
            {
                var transform = Player.transform;
                Vector3 directionToPlayer = transform.position - Enemy.transform.position;
                
                Enemy.transform.LookAt(transform);
                
                directionToPlayer.Normalize();
                
                Enemy.transform.Translate(-directionToPlayer * (_speed * Time.deltaTime), Space.World);
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