using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator), typeof(Canvas))]
public class PlanetInputPanel : MonoBehaviour, IPointerExitHandler
{
    private static readonly int _opening = Animator.StringToHash("Opening");
    private static readonly int _open = Animator.StringToHash("Open");
    private static readonly int _close = Animator.StringToHash("Close");

    private Animator _animator;
    private Canvas _canvas;
    private int _originalSorting;
    
    private const int InFrontSorting = 100;

    private bool IsOpen
    {
        get
        {
            int currentState = _animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            return currentState == _open || currentState == _opening;
        }
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvas = GetComponent<Canvas>();
        _originalSorting = _canvas.sortingOrder;
    }

    public void HoveringArrowButton()
    {
        if (!IsOpen)
        {
            _animator.SetTrigger(_open);
            _canvas.sortingOrder = InFrontSorting;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(CloseOnRelease());
        }
        else if (IsOpen)
        {
            Close();
        }
    }

    private IEnumerator CloseOnRelease()
    {
        while (Input.GetMouseButton(0))
        {
            yield return null;
        }

        if (IsOpen)
        {
            Close();
        }
    }

    private void Close()
    {
        _animator.SetTrigger(_close);
        _canvas.sortingOrder = _originalSorting;
    }
}