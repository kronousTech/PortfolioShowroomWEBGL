using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace KronosTech.ShowroomGeneration.Room.Videoplayer
{
    public class RoomVideosController : MonoBehaviour
    {
        [SerializeField] private ContentData[] _videoData;
        [SerializeField] private VideoPlayer _videoPlayer;

        [Header("Buttons")]
        [SerializeField] private Button _buttonPlay;
        [SerializeField] private Button _buttonPause;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonNext;
        [SerializeField] private Button _buttonPrev;

        private int _index;
        private int Index
        {
            get => _index;
            set
            {
                if (value >= _videoData.Length)
                {
                    _index = 0;
                }
                else if (value < 0)
                {
                    _index = _videoData.Length - 1;
                }
                else
                {
                    _index = value;
                }

                OnVideoChange?.Invoke(_index, _videoData[_index].title);

                _videoPlayer.url = _videoData[_index].url;

                _videoPlayer.Prepare();
                OnPrepare?.Invoke();
            }
        }

        private GalleryRoom _room;

        private static Action<RoomVideosController> OnVideoStart;

        public event Action<int> OnInitialize;
        public event Action<int, string> OnVideoChange;

        public event Action OnPrepare;
        public event Action<double> OnPrepareCompleted;
        public event Action OnPlayInput;
        public event Action OnPlay;
        public event Action OnRestartInput;
        public event Action OnRestart;
        public event Action OnPause;
        public event Action<double> OnPlaying;

        private void OnEnable()
        {
            _buttonNext.onClick.AddListener(NextVideo);
            _buttonPrev.onClick.AddListener(PreviousVideo);

            _buttonPlay.onClick.AddListener(() => StartCoroutine(PlayCoroutine()));
            _buttonPause.onClick.AddListener(Pause);
            _buttonRestart.onClick.AddListener(() => StartCoroutine(Restart()));

            _videoPlayer.prepareCompleted += (source) => Prepared();
            _videoPlayer.prepareCompleted += (source) => Pause();

            _room.OnPlacement += Prepare;

            OnVideoStart += DisableOtherVideoPlayers;
        }
        private void OnDisable()
        {
            _buttonNext.onClick.RemoveListener(NextVideo);
            _buttonPrev.onClick.RemoveListener(PreviousVideo);

            _buttonPlay.onClick.RemoveListener(() => StartCoroutine(PlayCoroutine()));
            _buttonPause.onClick.RemoveListener(Pause);
            _buttonRestart.onClick.RemoveListener(() => StartCoroutine(Restart()));

            _videoPlayer.prepareCompleted -= (source) => Prepared();
            _videoPlayer.prepareCompleted -= (source) => Pause();

            _room.OnPlacement -= Prepare;

            OnVideoStart -= DisableOtherVideoPlayers;
        }
        private void Awake()
        {
            _room = GetComponent<GalleryRoom>();
        }
        private void Start()
        {
            var renderTexture = new RenderTexture(1920, 1080, 0);
            _videoPlayer.targetTexture = renderTexture;
            _videoPlayer.GetComponent<RawImage>().texture = renderTexture;

            OnInitialize?.Invoke(_videoData.Length);
        }
        private void Update()
        {
            if(_videoPlayer.isPlaying && _videoPlayer.isPrepared)
            {
                OnPlaying?.Invoke(_videoPlayer.time);
            }
        }

        private void DisableOtherVideoPlayers(RoomVideosController controller)
        {
            if(controller != this)
            {
                _videoPlayer.Pause();
            }
        }

        private void NextVideo() => Index++;
        private void PreviousVideo() => Index--;
        private void Prepare()
        {
            if (_videoData.Length >= 1)
            {
                Index = 0;
            }
        }
        private void Prepared()
        {
            OnPrepareCompleted?.Invoke(_videoPlayer.length);
        }
        private void Pause()
        {
            _videoPlayer.Pause();

            OnPause?.Invoke();
        }
        private IEnumerator Restart()
        {
            _videoPlayer.Stop();
            _videoPlayer.Play();

            OnRestartInput?.Invoke();

            while (!_videoPlayer.isPlaying)
            {
                yield return null;
            }

            _videoPlayer.Pause();
            _videoPlayer.Prepare();

            OnRestart?.Invoke();
        }
        private IEnumerator PlayCoroutine()
        {
            _videoPlayer.Play();

            OnPlayInput?.Invoke();

            while (!_videoPlayer.isPlaying)
            {
                yield return null;
            }

            OnPlay?.Invoke();

            OnVideoStart?.Invoke(this);
        }
    }
}