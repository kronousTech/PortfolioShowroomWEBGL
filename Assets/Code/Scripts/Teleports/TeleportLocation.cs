using Teleports;
using UnityEngine;

public class TeleportLocation : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private RoomTags _tags;

    private TeleportsManager _teleportsManager;

    public string Name => _name;

    private void Awake()
    {
        Initialize();
    }
    private void OnEnable()
    {
        if(_teleportsManager == null)
        {
            Initialize();
        }

        _teleportsManager.AddTeleport(transform, _name, _tags != null ? _tags.Tags.ToString() : string.Empty);
    }
    private void OnDisable()
    {
        _teleportsManager.RemoveTeleport(transform);
    }

    private void Initialize()
    {
        if(_teleportsManager == null)
        {
            _teleportsManager = FindObjectOfType<TeleportsManager>();
        }
    }
}
