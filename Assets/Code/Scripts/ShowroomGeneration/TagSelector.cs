using System.Collections.Generic;
using UnityEngine;

public class TagSelector : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private TagToggle _togglePrefab;

    private readonly List<TagToggle> _toggles = new();
    
    private void Awake()
    {
        foreach (var tag in System.Enum.GetNames(typeof(RoomTagFlags)))
        {
            var toggle = Instantiate(_togglePrefab, _parent);
            toggle.Initialize(tag);

            _toggles.Add(toggle);
        }
    }
    private void Start()
    {
        RequestNewRooms();
    }

    // Called by Unity Button
    public void RequestNewRooms()
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
