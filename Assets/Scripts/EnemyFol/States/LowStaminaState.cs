using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace.Enemy.States
{
    public class LowStaminaState : BaseState
    {
        private float speed = 1f;

        public override void Enter()
        {
        }

        public override void Perform()
        {
            if (Enemy != null && StateMachine != null && Player != null && Enemy.GetStamina().Stamina < 30)
            {
                Vector3 directionToPlayer = Player.transform.position - Enemy.transform.position;
                
                Enemy.transform.LookAt(Player.transform);
                
                directionToPlayer.Normalize();
                
                Enemy.transform.Translate(-directionToPlayer * speed * Time.deltaTime, Space.World);
            }
            else
            {
                StateMachine.ChangeState(new PatrolState());
            }
        }

        public override void Exit()
        {
        }
    }
}