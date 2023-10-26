using System.Collections;
using System.Collections.Generic;
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

            GetComponent<Toggle>().onValueChanged.AddListener(TogglePlayerSounds);
            GetComponent<Toggle>().isOn = _playerSounds.GetState();
        }

        private void TogglePlayerSounds(bool value)
        {
            if (value)
                _playerSounds.EnablePlayerSounds();
            else
                _playerSounds.DisablePlayerSounds(); 
        }
    }
}