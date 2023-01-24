using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGraphicInteractable : MonoBehaviour
{
    private EventSystem _eventSystem;
    private PointerEventData _pointerEventData;
    [SerializeField] private List<GameObject> _uiElements = new List<GameObject>();

    private void Awake()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
        _pointerEventData = new PointerEventData(_eventSystem);
    }

    private void Start()
    {
        _pointerEventData.position = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        List<RaycastResult> results = new List<RaycastResult>();

        _eventSystem.RaycastAll(_pointerEventData, results);

        _uiElements.Clear();

        foreach (var item in results)
        {
            _uiElements.Add(item.gameObject);
        }
    }
}
