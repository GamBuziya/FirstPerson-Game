using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enemy;
using DefaultNamespace.EnemyFol;
using UnityEngine;
using UnityEngine.AI;

namespace GameCharacters
{
    public abstract class EnemyGameCharacter : GameCharacter
    {
        // Enemy Moves
        public GameObject Player { get; set; }
        public NavMeshAgent Agent { get => _agent;}
    
        protected StateMachine _stateMachine;
        
        protected NavMeshAgent _agent;
        //-----------------
        
        
        protected CanvasDisabler _canvasDisabler;
        protected bool _isDead = false;
        
        
        protected new void Awake()
        {
            base.Awake();
            
            Player = GameObject.FindGameObjectWithTag("Player");
        
    
            Animator = GetComponent<EnemyAnimation>();
            _stateMachine = GetComponent<StateMachine>();
            _agent = GetComponent<NavMeshAgent>();
            Health.DeathEvent.AddListener(Death);
            
            _stateMachine.Initialise();
            
            var temp = gameObject.GetComponentInChildren<Canvas>();
            _canvasDisabler = new CanvasDisabler(temp);
            
        }
        
        
        
        private void Death()
        {
            _isDead = true;
            GetComponent<EnemyCollision>().enabled = false;
            _stateMachine.enabled = false;
            _agent.enabled = false;
        }
        
        
        public StateMachine GetStateMachine() => _stateMachine;
        public CanvasDisabler GetCanvasDisabler() => _canvasDisabler;
    }
}