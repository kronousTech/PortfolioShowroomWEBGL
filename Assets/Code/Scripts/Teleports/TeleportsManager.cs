using KronosTech.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Teleports
{
    public class TeleportsManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _displaysParent;
        [SerializeField] private TeleportDisplay _display;
        [SerializeField] private UiDisplay _teleportPanel;

        private FirstPersonController _playerController;

        private readonly Dictionary<Transform, GameObject> _locationsDictionary = new();

        private void Awake()
        {
            if(_playerController == null)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            _playerController = GameObject.FindObjectOfType<FirstPersonController>();
        }

        public void AddTeleport(Transform location, string name, string tags)
        {
            if (_playerController == null)
            {
                Initialize();
            }

            var newDisplay = Instantiate(_display, _displaysParent);
            newDisplay.Init(name, tags);
            newDisplay.button.onClick.AddListener(() =>
            {
                _playerController.Teleport(location);
                _teleportPanel.ClosePanel();
            });

            _locationsDictionary.Add(location, newDisplay.gameObject);
        }
        public void RemoveTeleport(Transform location)
        {
            if(_locationsDictionary.ContainsKey(location) )
            {
                Destroy(_locationsDictionary[location]);
            }

            _locationsDictionary.Remove(location);
        }
    }
}