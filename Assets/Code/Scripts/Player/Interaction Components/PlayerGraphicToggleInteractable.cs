using Core.Player.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace KronosTech.PlayerInteraction
{
    public class PlayerGraphicToggleInteractable : MonoBehaviour
    {
        [SerializeField] private InputActionReference _actionRef;

        private Toggle _selectedToggle;
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
            _actionRef.action.performed += (a) => ClickToggle();
            _actionRef.action.Enable();

            GameEvents.OnPanelOpen += (value) => _uiOpen = value;

            Application.focusChanged += (focus) => StartCoroutine(ToggleFocus(focus));
        }
        private void OnDisable()
        {
            _ray.OnElementsUpdate -= CheckForButton;
            _actionRef.action.performed -= (a) => ClickToggle();
            _actionRef.action.Disable();

            GameEvents.OnPanelOpen -= (value) => _uiOpen = value;

            Application.focusChanged += (focus) => StartCoroutine(ToggleFocus(focus));
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
            if (_selectedToggle == null || _uiOpen || !_isFocused)
            {
                return;
            }

            ExecuteEvents.Execute(_selectedToggle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);

            _selectedToggle.isOn = !_selectedToggle.isOn;

            ExecuteEvents.Execute(_selectedToggle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
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
}