using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Enemy.States;
using UnityEngine;



public class PatrolState : BaseState
{
    private int _currentWaypointIndex;

    private float _timeForWaiting = 3f;
    private float _tempTime = 0;
    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        PatrolCycle();
        if (Enemy.CanSee())
        {
            if(Enemy.IsAngry) StateMachine.ChangeState(new AttackState());
            else if(Enemy.Path != null)
            {
                StateMachine.ChangeState(new PeaseState());
            }
            
        }
    }

    public override void Exit()
    {

    }

    private void PatrolCycle()
    {
        if (Enemy.Agent.remainingDistance < 0.2f)
        {
            _tempTime += Time.deltaTime;
            
            if(_tempTime < _timeForWaiting) return;
            if (_currentWaypointIndex < Enemy.Path.WayPoints.Count - 1)
            {
                _currentWaypointIndex++;
            }
            else _currentWaypointIndex = 0;

            Enemy.Agent.SetDestination(Enemy.Path.WayPoints[_currentWaypointIndex].position);
            _tempTime = 0;
        }
    }
        
}
