using DefaultNamespace.Enemy.States;
using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class BattleController
    {
        private int _basicAttackStaminaCost = 30;
        private int _forceAttackStaminaCost = 40;
        
        public PartsOfBattleMoves _currentMove = PartsOfBattleMoves.Nothing;
        public TypeOfMove _currentTypeOfMove = TypeOfMove.Nothing;

        public abstract void Attack();

        public abstract void Block();
    }
}