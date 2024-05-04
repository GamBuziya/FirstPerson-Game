namespace DefaultNamespace.Enemy.States
{
    public class PeaseState : BaseState
    {
        public override void Enter()
        {
            Enemy.Agent.SetDestination(Enemy.transform.position);
        }

        public override void Perform()
        {
            if (Enemy.IsAngry) StateMachine.ChangeState(new AttackState());
        }

        public override void Exit()
        {
        }
    }
}