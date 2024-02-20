using UnityEngine;

namespace DefaultNamespace.Enemy.States
{
    public class MoveToState : BaseState
    {
        private Transform _destinationPoint;

        public MoveToState(Transform destinationPoint)
        {
            _destinationPoint = destinationPoint;
        }
        public override void Enter()
        {
        }

        public override void Perform()
        {
            Debug.Log("AAAA");
            Debug.Log("Destination point:" + _destinationPoint.position);
            Enemy.Agent.SetDestination(_destinationPoint.position);
            if (Vector3.Distance(Enemy.transform.position, _destinationPoint.position) <= 1f)
            {
                StateMachine.ChangeState(new PeaseState());
            }
        }
        
        public override void Exit()
        {
        }
    }
}