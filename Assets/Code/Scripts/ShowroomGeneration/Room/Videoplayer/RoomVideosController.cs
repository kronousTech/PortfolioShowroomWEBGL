using KronosTech.AssetManagement;
using KronosTech.Services;
using System;
using System.Collections;
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
                if (value >= _videoClips.Length)
                {
                    _index = 0;
                }
                else if (value < 0)
                {
                    _index = _videoClips.Length - 1;
                }
                else
                {
                    _index = value;
                }

                OnVideoChange?.Invoke(_index, _videoClips[_index].title);

                _videoPlayer.url = _videoClips[_index].url;

                if(_videoPlayer.isActiveAndEnabled)
                {
                    _videoPlayer.Prepare();
                }

                OnPrepare?.Invoke();
            }
        }

        private RoomVideoData[] _videoClips;

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
            AssetsLoader.OnBundlesDownload += LoadVideos;

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
            AssetsLoader.OnBundlesDownload -= LoadVideos;

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
            _videoPlayer.source = VideoSource.Url;

            var renderTexture = new RenderTexture(1024, 768, 0);
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

        private void LoadVideos()
        {
            if (_videoData.Length == 0)
                return;

            _videoClips = new RoomVideoData[_videoData.Length];

            for (int i = 0; i < _videoClips.Length; i++)
            {
                _videoClips[i].title = _videoData[i].title;
                _videoClips[i].url = ServiceLocator.Instance.GetWebVideosService().LoadVideo(_videoData[i].asset);
            }

            Index = 0;
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

    public struct RoomVideoData
    {
        public string title;
        public string url;
    }
}