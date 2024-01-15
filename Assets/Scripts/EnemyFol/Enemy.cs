using System;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enemy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : GameCharacter
{
    [SerializeField] private Image _arrowImage;

    [SerializeField] private float _timeForArrow;
    // Enemy Moves
    public GameObject Player { get; set; }
    public NavMeshAgent Agent { get => _agent;}
    public Path Path;   
    
    
    private float _sightDistance = 13f;
    private float _fieldOfView = 70f;
    
    private StateMachine _stateMachine;
    private NavMeshAgent _agent;
    
    //-----------------
    
    private EnemySideAttackUI _sideAttackUI;

    public EnemySideAttackUI GetEnemySideAttackUI() => _sideAttackUI;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Stamina = GetComponent<EnemyStaminaController>();
        Health = new EnemyHealth(MaxHealth);
        
        Animator = GetComponent<EnemyAnimation>();
        _stateMachine = GetComponent<StateMachine>();
        _agent = GetComponent<NavMeshAgent>();
        
        _stateMachine.Initialise();

        _sideAttackUI = new EnemySideAttackUI(_arrowImage);
        BattleController = new EnemyBattleController(this, _timeForArrow);
    }
    
    private void Update()
    {
        CanSee();
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
    
}
