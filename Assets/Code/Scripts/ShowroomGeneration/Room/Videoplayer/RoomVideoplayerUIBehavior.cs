using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.ShowroomGeneration.Room.Videoplayer
{
    public class RoomVideoplayerUIBehavior : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup _buttonsLayout;
        [SerializeField] private RoomVideosController _controller;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _indexDisplay;
        [SerializeField] private GameObject _loadingVideoGO;
        [SerializeField] private Slider _timebar;
        [SerializeField] private Button _buttonPlay;
        [SerializeField] private Button _buttonPause;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonNext;
        [SerializeField] private Button _buttonPrev;

        private void OnEnable()
        {
            _controller.OnInitialize += (count) => _indexDisplay.gameObject.SetActive(count > 1);
            _controller.OnInitialize += (count) => _buttonNext.gameObject.SetActive(count > 1);
            _controller.OnInitialize += (count) => _buttonPrev.gameObject.SetActive(count > 1);

            _controller.OnVideoChange += (count, title) => _indexDisplay.text = (count + 1).ToString();
            _controller.OnVideoChange += (count, title) => _title.text = title.ToString();

            _controller.OnPrepare += () => _buttonPlay.interactable = false;
            _controller.OnPrepare += () => _buttonPause.interactable = false;
            _controller.OnPrepare += () => _buttonRestart.interactable = false;
            _controller.OnPrepare += () => _loadingVideoGO.SetActive(true);
            _controller.OnPrepare += () => _buttonPlay.gameObject.SetActive(true);
            _controller.OnPrepare += () => _buttonPause.gameObject.SetActive(false);
            _controller.OnPrepare += () => _timebar.value = 0;

            _controller.OnPrepareCompleted += (length) => _buttonPlay.interactable = true;
            _controller.OnPrepareCompleted += (length) => _buttonPause.interactable = true;
            _controller.OnPrepareCompleted += (length) => _buttonRestart.interactable = true;
            _controller.OnPrepareCompleted += (length) => _loadingVideoGO.SetActive(false);
            _controller.OnPrepareCompleted += (length) => _timebar.maxValue = (float)length;

            _controller.OnPlaying += (value) => _timebar.value = (float)value;

            _controller.OnPlayInput += () => _buttonPlay.interactable = false;
            _controller.OnPlayInput += () => _buttonPause.interactable = false;
            _controller.OnPlayInput += () => _buttonRestart.interactable = false;

            _controller.OnPlay += () => _buttonPlay.interactable = true;
            _controller.OnPlay += () => _buttonPause.interactable = true;
            _controller.OnPlay += () => _buttonRestart.interactable = true;
            _controller.OnPlay += () => _buttonPlay.gameObject.SetActive(false);
            _controller.OnPlay += () => _buttonPause.gameObject.SetActive(true);

            _controller.OnPause += () => _buttonPlay.interactable = true;
            _controller.OnPause += () => _buttonPause.interactable = true;
            _controller.OnPause += () => _buttonRestart.interactable = true;
            _controller.OnPause += () => _buttonPlay.gameObject.SetActive(true);
            _controller.OnPause += () => _buttonPause.gameObject.SetActive(false);

            _controller.OnRestartInput += () => _buttonPlay.interactable = false;
            _controller.OnRestartInput += () => _buttonPause.interactable = false;
            _controller.OnRestartInput += () => _buttonRestart.interactable = false;
            _controller.OnRestartInput += () => _timebar.value = 0.0f;

            _controller.OnRestart += () => _buttonPlay.interactable = true;
            _controller.OnRestart += () => _buttonPause.interactable = true;
            _controller.OnRestart += () => _buttonRestart.interactable = true;
            _controller.OnRestart += () => _buttonPlay.gameObject.SetActive(true);
            _controller.OnRestart += () => _buttonPause.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            _controller.OnInitialize -= (count) => _indexDisplay.gameObject.SetActive(count > 1);
            _controller.OnInitialize -= (count) => _buttonNext.gameObject.SetActive(count > 1);
            _controller.OnInitialize -= (count) => _buttonPrev.gameObject.SetActive(count > 1);

            _controller.OnVideoChange -= (count, title) => _indexDisplay.text = (count - 1).ToString();
            _controller.OnVideoChange -= (count, title) => _title.text = title.ToString();

            _controller.OnPrepare -= () => _buttonPlay.interactable = false;
            _controller.OnPrepare -= () => _buttonPause.interactable = false;
            _controller.OnPrepare -= () => _buttonRestart.interactable = false;
            _controller.OnPrepare -= () => _loadingVideoGO.SetActive(true);
            _controller.OnPrepare -= () => _buttonPlay.gameObject.SetActive(true);
            _controller.OnPrepare -= () => _buttonPause.gameObject.SetActive(false);
            _controller.OnPrepare -= () => _timebar.value = 0;

            _controller.OnPrepareCompleted -= (length) => _buttonPlay.interactable = true;
            _controller.OnPrepareCompleted -= (length) => _buttonPause.interactable = true;
            _controller.OnPrepareCompleted -= (length) => _buttonRestart.interactable = true;
            _controller.OnPrepareCompleted -= (length) => _loadingVideoGO.SetActive(false);
            _controller.OnPrepareCompleted -= (length) => _timebar.maxValue = (float)length;

            _controller.OnPlaying -= (value) => _timebar.value = (float)value;

            _controller.OnPlayInput -= () => _buttonPlay.interactable = false;
            _controller.OnPlayInput -= () => _buttonPause.interactable = false;
            _controller.OnPlayInput -= () => _buttonRestart.interactable = false;

            _controller.OnPlay -= () => _buttonPlay.interactable = true;
            _controller.OnPlay -= () => _buttonPause.interactable = true;
            _controller.OnPlay -= () => _buttonRestart.interactable = true;
            _controller.OnPlay -= () => _buttonPlay.gameObject.SetActive(false);
            _controller.OnPlay -= () => _buttonPause.gameObject.SetActive(true);

            _controller.OnPause -= () => _buttonPlay.interactable = true;
            _controller.OnPause -= () => _buttonPause.interactable = true;
            _controller.OnPause -= () => _buttonRestart.interactable = true;
            _controller.OnPause -= () => _buttonPlay.gameObject.SetActive(true);
            _controller.OnPause -= () => _buttonPause.gameObject.SetActive(false);

            _controller.OnRestartInput -= () => _buttonPlay.interactable = false;
            _controller.OnRestartInput -= () => _buttonPause.interactable = false;
            _controller.OnRestartInput -= () => _buttonRestart.interactable = false;
            _controller.OnRestartInput -= () => _timebar.value = 0.0f;

            _controller.OnRestart -= () => _buttonPlay.interactable = true;
            _controller.OnRestart -= () => _buttonPause.interactable = true;
            _controller.OnRestart -= () => _buttonRestart.interactable = true;
            _controller.OnRestart -= () => _buttonPlay.gameObject.SetActive(true);
            _controller.OnRestart -= () => _buttonPause.gameObject.SetActive(false);
        }
    }
}