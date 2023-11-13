using TMPro;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class RoomDisplayTags : MonoBehaviour
    {
        [SerializeField] private RoomInfoTags _tags;

        private TextMeshPro _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshPro>();
        }
        private void Start()
        {
            _text.text = "[" + _tags.Tags.ToString() + "]";
        }
    }
}