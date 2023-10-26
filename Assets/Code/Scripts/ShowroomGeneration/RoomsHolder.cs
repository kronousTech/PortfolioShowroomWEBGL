using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class RoomsHolder : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        [SerializeField] private List<GameObject> _roomPrefabs;

        private List<GameObject> _initializedRooms = new();

        private void Start()
        {
            for (int i = 0; i < _roomPrefabs.Count; i++)
            {
                var room = Instantiate(_roomPrefabs[i], _parent);
                room.SetActive(false);

                _initializedRooms.Add(room);
            }

            ShowroomGenerationEvents.OnRoomsInitialization?.Invoke(_initializedRooms);
        }
    }
}
