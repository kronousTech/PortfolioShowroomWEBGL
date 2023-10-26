using System;
using System.Collections.Generic;
using UnityEngine;

public class TagSelector : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _togglePrefab;

    private readonly List<TagToggle> _toggles = new();

    public static Action OnNewSelection;

    private void OnEnable()
    {
        OnNewSelection += RequestNewRooms;
    }
    private void OnDisable()
    {
        OnNewSelection -= RequestNewRooms;
    }
    private void Awake()
    {
        foreach (var tag in System.Enum.GetNames(typeof(RoomTagFlags)))
        {
            var toggle = Instantiate(_togglePrefab, _parent);
            toggle.GetComponent<TagToggle>().Initialize(tag);

            _toggles.Add(toggle.GetComponent<TagToggle>());
        }
    }

    private void Start()
    {
        RequestNewRooms();
    }

    private void RequestNewRooms()
    {
        var tags = new RoomTagFlags();

        foreach (var toggle in _toggles)
        {
            if(toggle.GetTag(out var tag))
            {
                tags |= tag;
            }
        }

        ShowroomGenerationEvents.OnNewRoomsRequest?.Invoke(tags);
    }
}
