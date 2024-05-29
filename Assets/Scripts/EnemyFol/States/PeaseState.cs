using DefaultNamespace.Abstract_classes;

namespace DefaultNamespace.Enemy.States
{
    public class PeaseState : BaseState
    {
        public override void Enter()
        {
            GameCharacter.Agent.SetDestination(GameCharacter.transform.position);
        }

        public override void Perform()
        {
            if (GameCharacter as IMagic == null)
            {
                StateMachine.ChangeState(new AttackState());
            }
            else
            {
                StateMachine.ChangeState(new MagicAttackState());
            }
            
        }

        public override void Exit()
        {
        }
    }
}