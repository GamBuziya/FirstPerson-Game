using Managers;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace DefaultNamespace.Enemy.States
{
    public class MagicAttackState : BaseState
    {
        private float _timer = 1;
        private float _attackTimer = 3f;
        public override void Enter()
        {
        }

        public override void Perform()
        {
            Attack();
            GameCharacter.Agent.SetDestination(GameCharacter.Player.transform.position);
        }

        private void Attack()
        {
            var playerTransform = Player.transform;
            Vector3 directionToPlayer = playerTransform.position - GameCharacter.transform.position;
            GameCharacter.transform.LookAt(playerTransform);
            directionToPlayer.Normalize();
    
            RaycastHit hit;
            Ray ray = new Ray(GameCharacter.transform.position, directionToPlayer);
            
            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.gameObject.CompareTag("Player"))
                {
                    Vector3 directionToLeft = Quaternion.Euler(0, -90, 0) * directionToPlayer;
                    GameCharacter.transform.Translate(directionToLeft * 1.3f * Time.deltaTime, Space.World);
                }
                else
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _attackTimer)
                    {
                        GameCharacter.GetComponent<EnemyMagicManager>().Attack();
                        _timer = 0;
                    }
                    
                }
            }
        }

        public override void Exit()
        {
            
        }
    }
}