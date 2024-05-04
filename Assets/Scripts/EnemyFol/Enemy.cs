using System;
using System.Collections.Generic;
using Abstract_classes;
using DefaultNamespace;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enemy;
using DefaultNamespace.EnemyFol;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : GameCharacter
{
    [SerializeField] private Image _arrowImage;

    [SerializeField] private float _timeForArrow;
    
    [SerializeField] public bool IsAngry;

    // Enemy Moves
    public GameObject Player { get; set; }
    public NavMeshAgent Agent { get => _agent;}
    
    private StateMachine _stateMachine;
    private NavMeshAgent _agent;
    
    //-----------------
    
    private EnemySideAttackUI _sideAttackUI;

    private CanvasDisabler _canvasDisabler;

    public StateMachine GetStateMachine() => _stateMachine;
    public CanvasDisabler GetCanvasDisabler() => _canvasDisabler;

    public EnemySideAttackUI GetEnemySideAttackUI() => _sideAttackUI;
    

    private bool _isDead = false;
    
    private float _timer = 0f;
    private float _interval = 0.1f;

    private new void Awake()
    {
        base.Awake();
        
        Player = GameObject.FindGameObjectWithTag("Player");
        
    
        Animator = GetComponent<EnemyAnimation>();
        _stateMachine = GetComponent<StateMachine>();
        _agent = GetComponent<NavMeshAgent>();
    
        _stateMachine.Initialise();

        _sideAttackUI = new EnemySideAttackUI(_arrowImage);

        var temp = gameObject.GetComponentInChildren<Canvas>();
        _canvasDisabler = new CanvasDisabler(temp);
        
        Health.DeathEvent.AddListener(_canvasDisabler.CanvasDisabled);
        Health.DeathEvent.AddListener(Death);
    }

    void Start()
    {
        //Спробувати передавати Delegate
        BattleController = new EnemyBattleController(this, _timeForArrow, GameObject.Find("Player").GetComponent<GameCharacter>());
    }
    
    private void Update()
    {
        if(!_isDead)
        {
            ((EnemyBattleController)BattleController).BattleControllerUpdate();
        }
        
        _timer += Time.deltaTime;
        if (_timer >= _interval)
        {
            _staminaManager.RegenerateStamina(ref _currentStamina);
            _timer = 0f;
        }
    }

    private void LateUpdate()
    {
        _sideAttackUI.ChangeUI(BattleController.GetCurrentTypeOfMove(), BattleController.GetCurrentMove());
    }
    

    private void Death()
    {
        _isDead = true;
        GetComponent<EnemyCollision>().enabled = false;
        _stateMachine.enabled = false;
        _agent.enabled = false;
    }
    
}
