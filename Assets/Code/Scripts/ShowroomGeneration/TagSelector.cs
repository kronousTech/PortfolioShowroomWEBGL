using KronosTech.ShowroomGeneration;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagSelector : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private TagToggle _togglePrefab;
    [SerializeField] private Button _generateButton;

    private readonly List<TagToggle> _toggles = new();

    private void OnEnable()
    {
        _generateButton.onClick.AddListener(RequestNewRooms);

        GenerateShowroom.OnGenerationStart += () => SetInteractivity(false);
        GenerateShowroom.OnGenerationEnd += (state) => SetInteractivity(true);
    }
    private void OnDisable()
    {
        _generateButton.onClick.RemoveListener(RequestNewRooms);

        GenerateShowroom.OnGenerationStart -= () => SetInteractivity(false);
        GenerateShowroom.OnGenerationEnd -= (state) => SetInteractivity(true);
    }
    private void Awake()
    {
        foreach (var tag in System.Enum.GetNames(typeof(RoomTagFlags)))
        {
            var toggle = Instantiate(_togglePrefab, _parent);
            toggle.Initialize(tag);

            _toggles.Add(toggle);
        }
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

    private void SetInteractivity(bool interactable)
    {
        foreach (var toggle in _toggles)
        {
            toggle.SetInteractability(interactable);
        }

        _generateButton.interactable = interactable;
    }
}