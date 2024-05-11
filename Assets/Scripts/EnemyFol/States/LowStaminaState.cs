using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace.Enemy.States
{
    public class LowStaminaState : BaseState
    {
        private float _speed = 0.5f;

        public override void Enter()
        {
        }
        

        public override void Perform()
        {
            if (GameCharacter != null && StateMachine != null && Player != null && GameCharacter.GetCurrentStamina() < 30)
            {
                var transform = Player.transform;
                Vector3 directionToPlayer = transform.position - GameCharacter.transform.position;
                
                GameCharacter.transform.LookAt(transform);
                
                directionToPlayer.Normalize();
                
                GameCharacter.transform.Translate(-directionToPlayer * (_speed * Time.deltaTime), Space.World);
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