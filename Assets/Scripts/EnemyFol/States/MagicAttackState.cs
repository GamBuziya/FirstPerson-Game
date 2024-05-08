using Managers;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace DefaultNamespace.Enemy.States
{
    public class MagicAttackState : BaseState
    {
        private float _firstTimer = 1;
        private float _secondTimer = 1;
        private float _attackTimer = 1f;
        private float _goTimer = 2f;
        private bool _needGo = false;
        public override void Enter()
        {
        }

        public override void Perform()
        {
            Attack();
            if (Vector3.Distance(Player.transform.position, GameCharacter.transform.position) > 10f)
            {
                GameCharacter.Agent.speed = 3.5f;
            }
            else
            {
                GameCharacter.Agent.speed = 0f;
            }
            
            GameCharacter.Agent.SetDestination(GameCharacter.Player.transform.position);
        }

        // ReSharper disable Unity.PerformanceAnalysis
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
                    GameCharacter.transform.Translate(directionToLeft * (2f * Time.deltaTime), Space.World);
                    _needGo = true;
                    return;
                }

                if (_needGo)
                {
                    Vector3 directionToLeft = Quaternion.Euler(0, -90 + Random.Range(-40, 40), 0) * directionToPlayer;
                    GameCharacter.transform.Translate(directionToLeft * (2f * Time.deltaTime), Space.World);
                    
                    _firstTimer += Time.deltaTime;
                    if (_firstTimer >= _goTimer)
                    {
                        _needGo = false;
                        _firstTimer = 0;
                    }
                    return;
                }
                
                _secondTimer += Time.deltaTime;
                if (_secondTimer >= _attackTimer + Random.Range(0, 1f))
                {
                    GameCharacter.GetComponent<EnemyMagicManager>().Attack();
                    _secondTimer = 0;
                }
                
                
            }
        }

        public override void Exit()
        {
            
        }
    }
}