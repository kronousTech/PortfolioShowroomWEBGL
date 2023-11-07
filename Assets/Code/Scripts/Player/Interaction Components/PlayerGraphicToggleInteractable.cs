using Core.Player.Interactions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace KronosTech.PlayerInteraction
{
    public class PlayerGraphicToggleInteractable : MonoBehaviour
    {
        private Toggle _selectedToggle;

        [SerializeField] private InputActionReference _actionRef;

        private void Awake()
        {
            GetComponent<PlayerGraphicRay>().OnElementsUpdate += CheckForButton;

            _actionRef.action.performed += (a) => ClickToggle();
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
                if (item.TryGetComponent<Toggle>(out var toggle))
                {
                    _selectedToggle = toggle;

                    return;
                }
            }

            _selectedToggle = null;
        }

        private void ClickToggle()
        {
            if (_selectedToggle == null)
            {
                return;
            }

            ExecuteEvents.Execute(_selectedToggle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);

            _selectedToggle.isOn = !_selectedToggle.isOn;

            ExecuteEvents.Execute(_selectedToggle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
        }
    }
}

