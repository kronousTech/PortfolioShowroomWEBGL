using UnityEngine;
using UnityEngine.UI;

namespace Ui.Options
{
    public class PlayerSoundsToggle : MonoBehaviour
    {
        private PlayerSounds _playerSounds;

        private void Awake()
        {
            _playerSounds = GameObject.FindObjectOfType<PlayerSounds>();
            if (_playerSounds == null)
            {
                Debug.LogError("Didn't found PlayerSounds on PlayerSoundsToggle");
                return;
            }

            GetComponent<Toggle>().onValueChanged.AddListener((value) => _playerSounds.enabled = value);
        }
    }
}