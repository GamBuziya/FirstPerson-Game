using DefaultNamespace.Abstract_classes;
using GameCharacters;
using UnityEngine;

namespace DefaultNamespace.EnemyFol
{
    public class EnemyCollision : CollisionManager
    {
        private void Awake()
        {
            _gameCharacter = GetComponent<EnemyGameCharacter>();
            _checker = new BlockChecker();
            _enemyLayer = _gameCharacter.GetEnemyLayer();
            SubscribeToAttack(() => _gameCharacter.GetHealthPoints().TakeDamage(30));
        }
    }
}