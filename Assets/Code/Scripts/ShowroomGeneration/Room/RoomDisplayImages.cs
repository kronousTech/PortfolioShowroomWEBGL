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

        [SerializeField] private GameObject _buttonsHolder;
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
            _buttonNext.onClick.AddListener(NextImage);
            _buttonPrev.onClick.AddListener(PreviousImage);
        }
        private void OnDisable()
        {
            _buttonNext.onClick.RemoveListener(NextImage);
            _buttonPrev.onClick.RemoveListener(PreviousImage);
        }

        private void NextImage() => Index++;
        private void PreviousImage() => Index--;

        public void AddSprites(RoomImageSpriteData[] sprites)
        {
            _sprites = sprites;

            UpdateImage();

            if(_sprites.Length <= 1)
            {
                _buttonsHolder.SetActive(false);
            }
        }

        private void UpdateImage()
        {
            _title.text = _sprites[Index].title;
            _display.sprite = _sprites[Index].sprite;
        }
    }
}