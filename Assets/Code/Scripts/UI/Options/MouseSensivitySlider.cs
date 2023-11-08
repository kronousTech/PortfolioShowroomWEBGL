using KronosTech.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Options
{
    public class MouseSensivitySlider : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _sensivityText;
        private FirstPersonController _firstPersonController;
        private void Awake()
        {
            _firstPersonController = GameObject.FindObjectOfType<FirstPersonController>();
            if (_firstPersonController == null)
            {
                Debug.LogError("Didn't found FirstPersonController on MouseSensivitySlider");
                return;
            }

            GetComponent<Slider>().onValueChanged.AddListener(SetMouseSensivity);
            GetComponent<Slider>().value = _firstPersonController.RotationSpeed / 10f;
        }

        private void SetMouseSensivity(float value)
        {
            var newMouseSensivity = value * 10f;

            _firstPersonController.RotationSpeed = newMouseSensivity;

            _sensivityText.text = _firstPersonController.RotationSpeed.ToString("0.0#");
        }
    }
}