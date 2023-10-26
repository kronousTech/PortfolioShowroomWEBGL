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
            GetComponent<Slider>().value = _firstPersonController.GetMouseSensivity() / 100f;
        }

        private void SetMouseSensivity(float value)
        {
            var newMouseSensivity = value * 100f;

            _firstPersonController.SetMouseSensivity(newMouseSensivity);
            _sensivityText.text = Mathf.Round(_firstPersonController.GetMouseSensivity()).ToString();
        }
    }
}