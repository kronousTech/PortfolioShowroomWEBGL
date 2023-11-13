using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class GalleryRoom : MonoBehaviour
    {
        [SerializeField] private GameObject _holder;
        [SerializeField] private GameObject _line;

        private void OnEnable()
        {
            GenerateShowroom.OnGenerationStart += () => ToggleVisibility(false);
        }
        private void OnDisable()
        {
            GenerateShowroom.OnGenerationStart -= () => ToggleVisibility(false);
        }

        public void Place(GalleryTileExit exit)
        {
            transform.position = exit.Position;
            transform.rotation = exit.Rotation;

            if (_line != null)
            {
                _line.SetActive(exit.AddLines);
            }
        }

        public void ToggleVisibility(bool state)
        {
            _holder.SetActive(state);
        }
    }
}