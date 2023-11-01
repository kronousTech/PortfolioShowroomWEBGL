using Core.Player.Interactions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KronosTech.PlayerInteraction
{
    public class PlayerGraphicToggleInteractable : MonoBehaviour
    {
        private Toggle _selectedToggle;

        private void Awake()
        {
            GetComponent<PlayerGraphicRay>().OnElementsUpdate += CheckForButton;
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

        private void Update()
        {
            if (_selectedToggle == null)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                ExecuteEvents.Execute(_selectedToggle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);

                _selectedToggle.isOn = !_selectedToggle.isOn;

                ExecuteEvents.Execute(_selectedToggle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }
        }

        bool IsMouseOverUI()
        {
            // Create a pointer event data
            PointerEventData eventData = new PointerEventData(EventSystem.current);

            // Set the event data's position to the current mouse position
            eventData.position = Input.mousePosition;

            // Perform a raycast to check for UI elements under the mouse
            RaycastResult[] results = new RaycastResult[1];
            EventSystem.current.RaycastAll(eventData, results.ToList());

            // If there are any results, the mouse is over a UI element
            return results.Length > 0;
        }
    }
}

