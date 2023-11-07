using TMPro;
using UnityEngine;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    //[SerializeField] private FirstPersonController _interpolateScript;

    [SerializeField] private TextMeshProUGUI _playerRotation;
    [SerializeField] private TextMeshProUGUI _playerEuler;
    [SerializeField] private TextMeshProUGUI _playerLocalEuler;
    [SerializeField] private TextMeshProUGUI _cameraRotation;
    [SerializeField] private TextMeshProUGUI _cameraEuler;
    [SerializeField] private TextMeshProUGUI _cameraLocalEuler;
    [SerializeField] private TextMeshProUGUI _interpolateValue;

    private void Update()
    {
        _playerRotation.text = "Player Rotation: " + _player.transform.rotation.ToString();
        _playerEuler.text = "Player Euler Angles: " + _player.transform.eulerAngles.ToString();
        _playerLocalEuler.text = "Player Local Euler Angles: " + _player.transform.localEulerAngles.ToString();
        _cameraRotation.text = "Camera Rotation: " + _camera.transform.rotation.ToString();
        _cameraEuler.text = "Camera Euler Angles: " + _camera.transform.eulerAngles.ToString();
        _cameraLocalEuler.text = "Camera Local Euler Angles: " + _camera.transform.localEulerAngles.ToString();
        //_interpolateValue.text = "Interpolation Value: " + _interpolateScript.interpolateValue;
    }
}