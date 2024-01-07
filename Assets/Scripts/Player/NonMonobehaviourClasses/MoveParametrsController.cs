using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace.NonMonobehaviourClasses
{
    public class MoveParametrsController
    {
        private float _mouseX = 0;
        private float _prevMouseX = 0;
        
        public void UpdateSide(Vector2 input)
        {
            input.Normalize();
            _prevMouseX = _mouseX;
            _mouseX -= MoreAccuracy(input.x);
        }
        
        
        public void GetMoveParametrs(bool isAttack, bool force, out PartsOfBattleMoves partsOfBattleMoves, out TypeOfMove typeOfMove)
        {
            float deltaX = _mouseX - _prevMouseX;
            deltaX = MoreAccuracy(deltaX);

            if(isAttack) typeOfMove = TypeOfMove.IsAttack;
            else typeOfMove = TypeOfMove.IsBlock;
            
                if (force)
                {
                    partsOfBattleMoves = PartsOfBattleMoves.Up;
                    return;
                }

            if (isAttack)
            {
                if (deltaX > 0) partsOfBattleMoves = PartsOfBattleMoves.Right;
                else partsOfBattleMoves = PartsOfBattleMoves.Left;
            }
            else
            {
                if (deltaX < 0) partsOfBattleMoves = PartsOfBattleMoves.Right;
                else partsOfBattleMoves = PartsOfBattleMoves.Left;
            }
        }
        
        
        private float MoreAccuracy(float num)
        {
            return num * Time.deltaTime * 100;
        }
    }
}