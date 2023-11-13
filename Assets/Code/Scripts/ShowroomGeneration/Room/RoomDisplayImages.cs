using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.ShowroomGeneration.Room
{
    public class RoomDisplayImages : MonoBehaviour
    {
        [SerializeField] private RoomImageSpriteData[] _sprites;

        [SerializeField] private Image _display;
        [SerializeField] private TextMeshProUGUI _title;

        [SerializeField] private Button _buttonNext;
        [SerializeField] private Button _buttonPrev;

        private int _index;
        private int Index
        {
            get => _index;
            set
            {
                if(value >= _sprites.Length)
                {
                    _index = 0;
                }
                else if(value < 0)
                {
                    _index = _sprites.Length - 1;
                }
                else
                {
                    _index = value;
                }

                UpdateImage();
            }
        }

        private void OnEnable()
        {
            _buttonNext.onClick.AddListener(() => Index++);
            _buttonPrev.onClick.AddListener(() => Index--);
        }
        private void OnDisable()
        {
            _buttonNext.onClick.RemoveListener(() => Index++);
            _buttonPrev.onClick.RemoveListener(() => Index--);
        }

        public void AddSprites(RoomImageSpriteData[] sprites)
        {
            _sprites = sprites;

            UpdateImage();
        }

        private void UpdateImage()
        {
            _title.text = _sprites[Index].title;
            _display.sprite = _sprites[Index].sprite;
        }
    }
}