using Core.Player.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGraphicButtonInteractable : MonoBehaviour
{
    [SerializeField] private InputActionReference _actionRef;

    private Button _selectedButton;
    private PlayerGraphicRay _ray;
    private bool _uiOpen = false;
    private bool _isFocused = true;

    private void Awake()
    {
        _ray = GetComponent<PlayerGraphicRay>();
    }
    private void OnEnable()
    {
        _ray.OnElementsUpdate += CheckForButton;
        _actionRef.action.performed += (a) => ClickButton();
        _actionRef.action.Enable();

        GameEvents.OnPanelOpen += (value) => _uiOpen = value;

        Application.focusChanged += (focus) => StartCoroutine(ToggleFocus(focus));
    }
    private void OnDisable()
    {
        _ray.OnElementsUpdate -= CheckForButton;
        _actionRef.action.performed -= (a) => ClickButton();
        _actionRef.action.Disable();

        GameEvents.OnPanelOpen -= (value) => _uiOpen = value;

        Application.focusChanged -= (focus) => StartCoroutine(ToggleFocus(focus));
    }

    private void CheckForButton(List<GameObject> elements)
    {
        foreach (var item in elements)
        {
            if (item.TryGetComponent<Button>(out var button))
            {
                _selectedButton = button;
                _selectedButton.Select();

                return;
            }
        }

        _selectedButton = null;

        EventSystem.current.SetSelectedGameObject(null);
    }
    private void ClickButton()
    {
        if (_selectedButton == null || !_selectedButton.interactable || _uiOpen || !_isFocused)
        {
            return;
        }

        ExecuteEvents.Execute(_selectedButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);

        _selectedButton.onClick.Invoke();

        ExecuteEvents.Execute(_selectedButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
    }

    private IEnumerator ToggleFocus(bool focused)
    {
        if (focused)
        {
            yield return null;

            _isFocused = true;
        }
        else
        {
            _isFocused = false;
        }
    }
}
