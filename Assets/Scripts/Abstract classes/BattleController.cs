using DefaultNamespace.Enemy.States;
using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class BattleController
    {
        protected int _basicAttackStaminaCost = 30;
        protected int _forceAttackStaminaCost = 40;
        
        
        protected SideOfMove _currentMove = SideOfMove.Nothing;
        protected TypeOfMove _currentTypeOfMove = TypeOfMove.Nothing;
        
        protected StaminaController StaminaController;
        protected AnimatorManager Animation;
        
        protected bool _force = false;
        
        public SideOfMove GetCurrentMove() => _currentMove;
        public TypeOfMove GetCurrentTypeOfMove() => _currentTypeOfMove;
        
        public void ResetMoves()
        {
            _currentMove = SideOfMove.Nothing;
            _currentTypeOfMove = TypeOfMove.Nothing;
        }
        
        public void ChangeForce()
        {
            _force = !_force;
        }
        
        public abstract void Attack();
        
    }
}