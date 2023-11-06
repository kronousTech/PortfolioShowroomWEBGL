using TMPro;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class RoomTagsDisplay : MonoBehaviour
    {
        [SerializeField] private RoomTags _tags;

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