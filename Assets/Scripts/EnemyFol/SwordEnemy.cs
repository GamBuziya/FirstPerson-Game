using System;
using System.Collections.Generic;
using Abstract_classes;
using DefaultNamespace;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enemy;
using DefaultNamespace.EnemyFol;
using GameCharacters;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SwordEnemy : EnemyGameCharacter
{
    [SerializeField] private Image _arrowImage;

    [SerializeField] private float _timeForArrow;
    
    
    private EnemySideAttackUI _sideAttackUI;
    
    public EnemySideAttackUI GetEnemySideAttackUI() => _sideAttackUI;
    
    private float _timer = 0f;
    private float _interval = 0.1f;

    private new void Awake()
    {
        base.Awake();
        
        _sideAttackUI = new EnemySideAttackUI(_arrowImage);

        var temp = gameObject.GetComponentInChildren<Canvas>();
        _canvasDisabler = new CanvasDisabler(temp);
        
        //Health.DeathEvent.AddListener(_canvasDisabler.CanvasDisabled);
        
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
    

    
    
}
