using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Options
{
    public class MouseSensivitySlider : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _sensivityText;
        private StarterAssets.FirstPersonController _firstPersonController;
        private void Awake()
        {
            _firstPersonController = GameObject.FindObjectOfType<StarterAssets.FirstPersonController>();
            if (_firstPersonController == null)
            {
                Debug.LogError("Didn't found FirstPersonController on MouseSensivitySlider");
                return;
            }

            GetComponent<Slider>().onValueChanged.AddListener(SetMouseSensivity);
            GetComponent<Slider>().value = _firstPersonController.RotationSpeed / 100f;
        }

        private void SetMouseSensivity(float value)
        {
            var newMouseSensivity = value * 100f;

            _firstPersonController.RotationSpeed = newMouseSensivity;
            _sensivityText.text = Mathf.Round(_firstPersonController.RotationSpeed).ToString();
        }
    }
}