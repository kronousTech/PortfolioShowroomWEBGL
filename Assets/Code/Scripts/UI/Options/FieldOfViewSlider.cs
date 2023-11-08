using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Options
{
    public class FieldOfViewSlider : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fieldOfViewText;
        private CinemachineVirtualCamera _camera;

        private const float MAX_FOV = 110f;
        private const float MIN_FOV = 60f;

        private void Awake()
        {
            _camera = FindObjectOfType<CinemachineVirtualCamera>();
            if (_camera == null)
            {
                Debug.LogError("Didn't found Camera on FieldOfViewSlider");
                return;
            }

            GetComponent<Slider>().onValueChanged.AddListener(SetFieldOfView);
            GetComponent<Slider>().value = Remap(_camera.m_Lens.FieldOfView, MIN_FOV, MAX_FOV, 0f, 1f);
        }

        private void SetFieldOfView(float value)
        {
            var newFieldOfView = Remap(value, 0f, 1f, MIN_FOV, MAX_FOV);

            _camera.m_Lens.FieldOfView = newFieldOfView;
            _fieldOfViewText.text = Mathf.Round(newFieldOfView).ToString();
        }
        public float Remap( float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}