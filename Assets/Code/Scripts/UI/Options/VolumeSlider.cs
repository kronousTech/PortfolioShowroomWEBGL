using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Options
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private float _startingVolume;
        [SerializeField] private TextMeshProUGUI _percentageText;
        private void Awake()
        {
            GetComponent<Slider>().onValueChanged.AddListener(SetGlobalVolume);
            GetComponent<Slider>().value = _startingVolume;
        }

        private void SetGlobalVolume(float volume)
        {
            AudioListener.volume = volume;
            _percentageText.text = Mathf.Round(volume * 100f) + "%";
        }
    }
}