using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.ShowroomGeneration.Room
{
    public class RoomDisplayImages : MonoBehaviour
    {
        [SerializeField] private Image _display;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _indexDisplay;
        [SerializeField] private GameObject _downloadingImageGO;
        [SerializeField] private GameObject _buttonsHolder;
        [Header("Buttons")]
        [SerializeField] private Button _buttonNext;
        [SerializeField] private Button _buttonPrev;

        private RoomImageSpriteData[] _sprites;
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

                _indexDisplay.text = _sprites.Length > 1 ? (_index + 1).ToString() : string.Empty;

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
            _downloadingImageGO.SetActive(false);

            _sprites = sprites;

            Index = 0;

            UpdateImage();

            _buttonsHolder.SetActive(_sprites.Length > 1);
        }

        private void UpdateImage()
        {
            _title.text = _sprites[Index].title;
            _display.sprite = _sprites[Index].sprite;
        }
    }
}