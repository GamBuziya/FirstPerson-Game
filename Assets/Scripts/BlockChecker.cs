using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class BlockChecker
    {

        public bool IsBlock(GameObject enemy, GameObject hero)
        {
            var enemyCharacter = enemy.GetComponentInParent<GameCharacter>();
            var heroCharacter = hero.GetComponent<GameCharacter>();
            
            //Debug.Log("enemy.name" + enemyCharacter);
            //Debug.Log("hero.name" + heroCharacter);
            Debug.Log("enemyCharacter" + enemyCharacter.GetBattleController().GetCurrentMove());
            Debug.Log("heroCharacter" + heroCharacter.GetBattleController().GetCurrentMove());
            if (enemyCharacter == null)
            {
                Debug.Log("IsEmpty");
                return false;
            }
            
            if (heroCharacter.GetBattleController().GetCurrentTypeOfMove() != TypeOfMove.IsBlock) return false;

            if (heroCharacter.GetBattleController().GetCurrentMove() == enemyCharacter.GetBattleController().GetCurrentMove() &&  heroCharacter.GetBattleController().GetCurrentMove() == SideOfMove.Up)
            {
                return true;
            }

            if(heroCharacter.GetBattleController().GetCurrentMove() == SideOfMove.Left && enemyCharacter.GetBattleController().GetCurrentMove() == SideOfMove.Right) 
                return true;
            if (heroCharacter.GetBattleController().GetCurrentMove() == SideOfMove.Right &&
                enemyCharacter.GetBattleController().GetCurrentMove() == SideOfMove.Left)
                return true;

            return false;
        }
    }
}