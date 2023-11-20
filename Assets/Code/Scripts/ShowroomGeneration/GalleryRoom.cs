using System;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class GalleryRoom : MonoBehaviour
    {
        [SerializeField] private GameObject _holder;
        [SerializeField] private GameObject _line;

        public event Action OnPlacement;

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

            OnPlacement?.Invoke();
        }

        public void ToggleVisibility(bool state)
        {
            // TODO: FIX LATER
            if(_holder != null)
            {
                _holder.SetActive(state);
            }
        }
    }
}