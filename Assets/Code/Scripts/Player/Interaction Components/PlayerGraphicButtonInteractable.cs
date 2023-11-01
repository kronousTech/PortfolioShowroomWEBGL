using Core.Player.Interactions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerGraphicButtonInteractable : MonoBehaviour
{
    private Button _selectedButton;

    private void Awake()
    {
        GetComponent<PlayerGraphicRay>().OnElementsUpdate += CheckForButton;
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

    private void Update()
    {
        if(_selectedButton == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ExecuteEvents.Execute(_selectedButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);

            _selectedButton.onClick.Invoke();

            ExecuteEvents.Execute(_selectedButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
        }
    }


    static bool IsMouseOverUI()
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
