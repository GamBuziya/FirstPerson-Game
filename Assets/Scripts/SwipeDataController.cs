using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeDataController : MonoBehaviour, IEndDragHandler
{ 
    [SerializeField] private Vector3 _pageStep;
    [SerializeField] private RectTransform _levelPagesRect;
    [SerializeField] private float _tweenTime;
    [SerializeField] private Ease _tweenType;

    [Header("Buttons")] 
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    
    private Vector3 _targetPos;
    private float dragThreshuld;
    private IDataReturner _manager;
    private int _maxWeapons;

    private void Start()
    { 
        _manager = GetComponentInChildren<IDataReturner>();
        _targetPos = _levelPagesRect.localPosition;
        dragThreshuld = Screen.width / 10;
        _maxWeapons = _manager.GetMaxCount();
        UpdateArrowButton();
        
    }
    

    public void Next()
    {
        if (_manager.GetCurrent() < _maxWeapons )
        {
            var currentPage = _manager.GetCurrent() + 1;
            _manager.SetCurrent(currentPage);
            _targetPos += _pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (_manager.GetCurrent()  > 0)
        {
            var currentPage = _manager.GetCurrent()  - 1;
            _manager.SetCurrent(currentPage);
            _targetPos -= _pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        _levelPagesRect.DOLocalMove(_targetPos, _tweenTime).SetEase(_tweenType);
        UpdateArrowButton();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (MathF.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshuld)
        {
            if(eventData.position.x > eventData.pressPosition.x) Previous();
            else Next();
        }
        else
        {
            MovePage();
        }
    }

    private void UpdateArrowButton()
    {
        _nextButton.interactable = true;
        _previousButton.interactable = true;
        
        if (_manager.GetCurrent() == 0) _previousButton.interactable = false;
        else if(_manager.GetCurrent() == _maxWeapons - 1) _nextButton.interactable = false;
        
    }
}
