using UnityEngine;

namespace KronosTech.Customization.Pictures
{
    public class PictureDisplay : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        private void Awake()
        {
            PictureController.Add(this);
        }
        private void OnDestroy()
        {
            PictureController.Remove(this);
        }

        public void SetPicture(Sprite sprite)
        {
            _renderer.sprite = sprite;
        }
    }
}

