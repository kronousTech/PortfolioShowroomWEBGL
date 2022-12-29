using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Teleports
{
    public class TeleportsManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _displaysParent;
        [SerializeField] private TeleportDisplay _display;
        [SerializeField] private UiDisplay _teleportPanel;
        [Header("Settings")]
        [SerializeField] private bool _teleportToFirstIndexOnStart = true;
        [Header("Locations")]
        [SerializeField] private TeleportInfo[] _info;
        private List<TeleportDisplay> _locations = new List<TeleportDisplay>();

        private FirstPersonController _playerController;
        private void Awake()
        {
            _playerController = GameObject.FindObjectOfType<FirstPersonController>();
        }

        private void Start()
        {
            foreach (var item in _info)
            {
                var newDisplay = Instantiate(_display, _displaysParent);
                newDisplay.Init(item, _playerController, _teleportPanel);

                _locations.Add(newDisplay);
            }

            if(_teleportToFirstIndexOnStart && _info.Length > 0)
            {
                _locations[0].GetComponent<Button>().onClick.Invoke();
            }
        }

        public IEnumerable<TeleportInfo> GetLocationsInfo() => _info;
    }
}