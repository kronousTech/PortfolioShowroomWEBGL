using KronosTech.Player;
using KronosTech.ShowroomGeneration.Room.Videoplayer;
using System.Collections;
using UnityEngine;

public class RoomVideoplayerAutoStoper : MonoBehaviour
{
    [SerializeField] private RoomVideosController _controller;

    private Transform _playerTF;
    private static float _stopDistance = 6f;

    private Coroutine _checkDistanceCoroutine;

    private void OnEnable()
    {
        _controller.OnPlay += () => _checkDistanceCoroutine = StartCoroutine(CheckDistance());
        _controller.OnPause += StopSearch;
        _controller.OnVideoChange += (index, name) => StopSearch();
    }
    private void OnDisable()
    {
        _controller.OnPlay -= () => _checkDistanceCoroutine = StartCoroutine(CheckDistance());
        _controller.OnPause -= StopSearch;
        _controller.OnVideoChange -= (index, name) => StopSearch();
    }
    private void Awake()
    {
        _playerTF = FindObjectOfType<FirstPersonController>().transform;
    }

    private IEnumerator CheckDistance()
    {
        var distance = Vector3.Distance(_playerTF.position, _controller.transform.position);

        while (distance < _stopDistance)
        {
            yield return null;

            distance = Vector3.Distance(_playerTF.position, _controller.transform.position);
        }

        _controller.ForcePause();
    }

    private void StopSearch()
    {
        if (_checkDistanceCoroutine != null) StopCoroutine(_checkDistanceCoroutine);
    }
}