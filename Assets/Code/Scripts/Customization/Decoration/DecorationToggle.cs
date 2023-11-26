using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Customization.Decoration
{
    public class DecorationToggle : MonoBehaviour
    {
        private Toggle _toggle;

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(DecorationController.SetVisibility);
        }
        private void OnDisable()
        {
            _toggle.onValueChanged.AddListener(DecorationController.SetVisibility);
        }
        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }
    }
}