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
    public Path Path;
    public Path PointsList;
    
    private float _sightDistance = 13f;
    private float _fieldOfView = 70f;
    
    private StateMachine _stateMachine;
    private NavMeshAgent _agent;
    
    //-----------------
    
    private EnemySideAttackUI _sideAttackUI;

    private CanvasDisabler _canvasDisabler;

    public StateMachine GetStateMachine() => _stateMachine;
    public CanvasDisabler GetCanvasDisabler() => _canvasDisabler;

    public EnemySideAttackUI GetEnemySideAttackUI() => _sideAttackUI;
    

    private bool IsDead = false;
    
    private float timer = 0f;
    private float interval = 0.1f;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        
        
        _staminaManager = new StaminaManager(_maxStamina, 1.2f, 0.2f);
        _currentStamina = _maxStamina;
        _currentHealth = _maxHealth;
        
        
        Health = new HealthManager(this);
        
    
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
        if(!IsDead)
        {
            ((EnemyBattleController)BattleController).BattleControllerUpdate();
        }
        
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            _staminaManager.RegenerateStamina(ref _currentStamina);
            timer = 0f;
        }
    }

    private void LateUpdate()
    {
        _sideAttackUI.ChangeUI(BattleController.GetCurrentTypeOfMove(), BattleController.GetCurrentMove());
    }

    public bool CanSee()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < _sightDistance)
        {
            Vector3 targetDirection = Player.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, targetDirection);

            if (angle >= -_fieldOfView && angle <= _fieldOfView)
            {
                Ray ray = new Ray(transform.position, targetDirection);
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(ray, out hitInfo, _sightDistance))
                {
                    if (hitInfo.transform.gameObject == Player)
                    {
                        Debug.DrawRay(ray.origin, ray.direction* _sightDistance);
                        return true;
                    }
                }
                
            }
        }

        return false;
    }

    private void Death()
    {
        IsDead = true;
        GameObject.Find("SoundSystem").GetComponent<Sounds>().PlayDeathSound();
        GetComponent<EnemyCollision>().enabled = false;
        _stateMachine.enabled = false;
        _agent.enabled = false;
    }
    
}
