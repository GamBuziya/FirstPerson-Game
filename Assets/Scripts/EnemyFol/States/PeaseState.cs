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
            if(Enemy.Path != null) StateMachine.ChangeState(new PatrolState());
        }

        public override void Exit()
        {
        }
    }
}