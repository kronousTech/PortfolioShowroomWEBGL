using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Player.Interactions
{
    public class PlayerGraphicRay : MonoBehaviour
    {
        private EventSystem _eventSystem;
        private PointerEventData _pointerEventData;
        private Transform _player;

        [SerializeField] private float _rayDistance; 
        [SerializeField] private List<GameObject> _uiElements = new List<GameObject>();

        public event Action<List<GameObject>> OnElementsUpdate;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _eventSystem = FindObjectOfType<EventSystem>();
            _pointerEventData = new PointerEventData(_eventSystem);
        }

        private void Start()
        {
            _pointerEventData.position = new Vector2(Screen.width / 2, Screen.height / 2);
        }

        private void Update()
        {
            List<RaycastResult> results = new List<RaycastResult>();

            _eventSystem.RaycastAll(_pointerEventData, results);

            _uiElements.Clear();

            foreach (var item in results)
            {
                if(Vector3.Distance(item.gameObject.transform.position, _player.position) < _rayDistance)
                {
                    _uiElements.Add(item.gameObject);
                }
            }

            OnElementsUpdate?.Invoke(_uiElements);
        }
    }
}