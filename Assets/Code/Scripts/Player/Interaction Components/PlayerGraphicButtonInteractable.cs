using Core.Player.Interactions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGraphicButtonInteractable : MonoBehaviour
{
    private Button _selectedButton;

    [SerializeField] private InputActionReference _actionRef;

    private void Awake()
    {
        GetComponent<PlayerGraphicRay>().OnElementsUpdate += CheckForButton;

        _actionRef.action.performed += (a) => ClickButton();
    }
    private void OnEnable()
    {
        _actionRef.action.Enable();
    }
    private void OnDisable()
    {
        _actionRef.action.Disable();
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
        if (_selectedButton == null)
        {
            return;
        }

        ExecuteEvents.Execute(_selectedButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);

        _selectedButton.onClick.Invoke();

        ExecuteEvents.Execute(_selectedButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
    }
}
