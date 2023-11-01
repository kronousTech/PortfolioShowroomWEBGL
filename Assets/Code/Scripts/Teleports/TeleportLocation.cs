using Teleports;
using UnityEngine;

public class TeleportLocation : MonoBehaviour
{
    [SerializeField] private string _name;

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

        _teleportsManager.AddTeleport(transform, _name);
    }
    private void OnDisable()
    {
        _teleportsManager.RemoveTeleport(transform, _name);
    }

    private void Initialize()
    {
        if(_teleportsManager == null)
        {
            _teleportsManager = FindObjectOfType<TeleportsManager>();
        }
    }
}
