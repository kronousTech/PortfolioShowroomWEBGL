using TMPro;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class RoomDisplayTitle : MonoBehaviour
    {
        [SerializeField] private TeleportLocation _teleport;

        private TextMeshPro _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshPro>();
        }
        private void Start()
        {
            _text.text = _teleport.Name;
        }
    }
}