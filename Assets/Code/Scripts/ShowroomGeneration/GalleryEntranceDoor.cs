using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class GalleryEntranceDoor : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }
        private void OnEnable()
        {
            GenerateShowroom.OnGenerationStart += () => SetDoorState(false);
            GenerateShowroom.OnGenerationEnd += (state) => SetDoorState(state);
        }
        private void OnDisable()
        {
            GenerateShowroom.OnGenerationStart -= () => SetDoorState(false);
            GenerateShowroom.OnGenerationEnd -= (state) => SetDoorState(state);
        }

        private void SetDoorState(bool open)
        {
            _animator.SetBool("Open", open);

            if(open)
            {
                _audioSource.Play();
            }
        }
    }
}