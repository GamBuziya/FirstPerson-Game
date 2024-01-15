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
        
        
        public void GetMoveParametrs(bool isAttack, bool force, out SideOfMove sideOfMove, out TypeOfMove typeOfMove)
        {
            float deltaX = _mouseX - _prevMouseX;
            deltaX = MoreAccuracy(deltaX);

            if(isAttack) typeOfMove = TypeOfMove.IsAttack;
            else typeOfMove = TypeOfMove.IsBlock;
            
                if (force)
                {
                    sideOfMove = SideOfMove.Up;
                    return;
                }

            if (isAttack)
            {
                if (deltaX > 0) sideOfMove = SideOfMove.Right;
                else sideOfMove = SideOfMove.Left;
            }
            else
            {
                if (deltaX < 0) sideOfMove = SideOfMove.Right;
                else sideOfMove = SideOfMove.Left;
            }
        }
        
        
        private float MoreAccuracy(float num)
        {
            return num * Time.deltaTime * 100;
        }
    }
}