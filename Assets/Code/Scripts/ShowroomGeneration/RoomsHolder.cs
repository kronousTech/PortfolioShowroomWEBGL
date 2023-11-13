using System;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class RoomsHolder : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        [SerializeField] private List<GalleryRoom> _roomPrefabs;

        private readonly List<GalleryRoom> _initializedRooms = new();

        public static event Action<List<GalleryRoom>> OnInitialization;

        private void Start()
        {
            for (int i = 0; i < _roomPrefabs.Count; i++)
            {
                var room = Instantiate(_roomPrefabs[i], _parent);
                room.ToggleVisibility(false);

                _initializedRooms.Add(room);
            }

            OnInitialization?.Invoke(_initializedRooms);
        }
    }
}
