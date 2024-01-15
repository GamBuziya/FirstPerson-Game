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
            
            if (_hero.GetBattleController().GetCurrentTypeOfMove() != TypeOfMove.IsBlock) return false;

            if (_hero.GetBattleController().GetCurrentMove() == enemyCharacter.GetBattleController().GetCurrentMove() &&  _hero.GetBattleController().GetCurrentMove() == SideOfMove.Up)
            {
                return true;
            }

            if(_hero.GetBattleController().GetCurrentMove() == SideOfMove.Left && enemyCharacter.GetBattleController().GetCurrentMove() == SideOfMove.Right) 
                return true;
            if (_hero.GetBattleController().GetCurrentMove() == SideOfMove.Right &&
                enemyCharacter.GetBattleController().GetCurrentMove() == SideOfMove.Left)
                return true;

            return false;
        }
    }
}