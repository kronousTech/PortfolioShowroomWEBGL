using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Teleports
{
    public class TeleportDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _tagsText;

        [HideInInspector] public Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }
        public void Init(string name, string tags)
        {
            _nameText.text = name;
            _tagsText.text = tags;
        }
    }
}