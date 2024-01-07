using System;
using System.Security.Cryptography.X509Certificates;
using DefaultNamespace.Enums;
using DefaultNamespace.NonMonobehaviourClasses;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBattleController : MonoBehaviour
    {
        [SerializeField] private float _basicAttackStaminaCost = 30f;
        [SerializeField] private float _ForceAttackStaminaCost = 40f;
        
        public PlayerAnimation Animation;
        public PlayerStamina Stamina;
        private MoveParametrsController _battleMoves;
        private bool _force = false;
        
        
        //Треба мати назву удару що він зараз буде робити, щоб мати можливість в майбутньому парувати її
        
        public PartsOfBattleMoves _currentMove;
        public TypeOfMove _currentTypeOfMove;
        

        public void UpdateMousePosition(Vector2 readValue)
        {
            _battleMoves.UpdateSide(readValue);
        }
        
        private void Awake()
        {
            Animation = GetComponent<PlayerAnimation>();
            Stamina = GetComponent<PlayerStamina>();
            _battleMoves = new MoveParametrsController();
        }
        public void Attack()
        {
            _battleMoves.GetMoveParametrs(true, _force, out _currentMove, out _currentTypeOfMove);
            Animation.PlayAnimation(_currentMove, _currentTypeOfMove);
            
        }
        public void Block()
        {
            _battleMoves.GetMoveParametrs(false, _force,  out _currentMove, out _currentTypeOfMove);
            Animation.PlayAnimation(_currentMove, _currentTypeOfMove);
        }

        
        public void ResetBlock()
        {
            Animation.ResetBlock();
        }

        public void ChangeForce()
        {
            _force = !_force;
        }
        
        public void SetAnimator(Animator animator)
        {
            Animation.Animator = animator;
        }
    }
}