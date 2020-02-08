using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Canvas))]
public class PlanetInputPanel : MonoBehaviour
{
    private readonly int _openTrigger = Animator.StringToHash("Open");
    private readonly int _closeTrigger = Animator.StringToHash("Close");

    private Animator _animator;
    private Canvas _canvas;
    private bool _isOpen;
    private int _originalSorting;
    private int _inFrontSorting = 100;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvas = GetComponent<Canvas>();
        _originalSorting = _canvas.sortingOrder;
    }

    public void OnClickButton()
    {
        int trigger;
        int sorting;

        if (!_isOpen)
        {
            trigger = _openTrigger;
            sorting = _inFrontSorting;
        }
        else
        {
            trigger = _closeTrigger;
            sorting = _originalSorting;
        }

        _animator.SetTrigger(trigger);
        _canvas.sortingOrder = sorting;

        _isOpen = !_isOpen;
    }

    public void OnMouseEnter()
    {
        Debug.Log("ENTERED");
    }
}
