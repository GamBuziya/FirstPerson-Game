using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
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
    private int _maxPage = 3;
    private int _currentPage;

    private void Start()
    {
        _currentPage = 1;
        _targetPos = _levelPagesRect.localPosition;
        dragThreshuld = Screen.width / 10;
        UpdateArrowButton();
    }
    

    public void Next()
    {
        Debug.Log("Next");
        if (_currentPage < _maxPage)
        {
            _currentPage++;
            _targetPos += _pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        Debug.Log("Previous");
        if (_currentPage > 1)
        {
            _currentPage--;
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
        
        
        if (_currentPage == 1) _previousButton.interactable = false;
        else if(_currentPage == _maxPage) _nextButton.interactable = false;
        
    }
}
