using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Teleports
{
    public class TeleportDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        public Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }
        public void Init(string name)
        {
            _nameText.text = name;
        }
    }
}