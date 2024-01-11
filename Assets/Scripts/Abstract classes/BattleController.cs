﻿using DefaultNamespace.Enemy.States;
using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class BattleController
    {
        protected int _basicAttackStaminaCost = 30;
        protected int _forceAttackStaminaCost = 40;
        
        //Треба мати назву удару що він зараз буде робити, щоб мати можливість в майбутньому парувати її
        protected PartsOfBattleMoves _currentMove = PartsOfBattleMoves.Nothing;
        protected TypeOfMove _currentTypeOfMove = TypeOfMove.Nothing;

        public PartsOfBattleMoves GetCurrentMove() => _currentMove;
        public TypeOfMove GetCurrentTypeOfMove() => _currentTypeOfMove;
        
        protected StaminaController StaminaController;
        protected AnimatorManager Animation;
        
        protected bool _force = false;
        
        public void ResetMoves()
        {
            _currentMove = PartsOfBattleMoves.Nothing;
            _currentTypeOfMove = TypeOfMove.Nothing;
        }
        
        public void ChangeForce()
        {
            _force = !_force;
        }
        
        public abstract void Attack();

        public abstract void Block();
    }
}