namespace DefaultNamespace.Enemy.States
{
    public class PeaseState : BaseState
    {
        public override void Enter()
        {
            SwordEnemy.Agent.SetDestination(SwordEnemy.transform.position);
        }

        public override void Perform()
        {
            StateMachine.ChangeState(new AttackState());
        }

        public override void Exit()
        {
        }
    }
}