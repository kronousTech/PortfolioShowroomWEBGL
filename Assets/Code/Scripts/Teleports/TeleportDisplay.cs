using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Teleports
{
    public class TeleportDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        public void Init(TeleportInfo info, FirstPersonController playerController, UiDisplay teleportPanel)
        {
            _nameText.text = info.name;

            _button.onClick.AddListener(delegate { playerController.Teleport(info.location.position); });
            _button.onClick.AddListener(teleportPanel.ClosePanel);
            _button.onClick.AddListener(info.events.Invoke);
        }
    }
}