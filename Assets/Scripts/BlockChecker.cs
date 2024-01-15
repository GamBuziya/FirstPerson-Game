using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class BlockChecker
    {
        private GameCharacter _hero;
        
        public BlockChecker(GameCharacter hero)
        {
            _hero = hero;
        }

        public bool IsBlock(GameObject enemy)
        {
            var enemyCharacter = enemy.GetComponentInParent<GameCharacter>();
            if (enemyCharacter == null)
            {
                Debug.Log("IsEmpty");
                return false;
            }
            
            if (_hero.BattleController.GetCurrentTypeOfMove() != TypeOfMove.IsBlock) return false;

            if (_hero.BattleController.GetCurrentMove() == enemyCharacter.BattleController.GetCurrentMove() &&  _hero.BattleController.GetCurrentMove() == SideOfMove.Up)
            {
                return true;
            }

            if(_hero.BattleController.GetCurrentMove() == SideOfMove.Left && enemyCharacter.BattleController.GetCurrentMove() == SideOfMove.Right) 
                return true;
            if (_hero.BattleController.GetCurrentMove() == SideOfMove.Right &&
                enemyCharacter.BattleController.GetCurrentMove() == SideOfMove.Left)
                return true;

            return false;
        }
    }
}