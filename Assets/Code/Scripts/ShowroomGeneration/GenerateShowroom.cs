using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class GenerateShowroom : MonoBehaviour
    {
        [Header("Parents")]
        [SerializeField] private Transform _floorsParent;
        [SerializeField] private Transform _wallsParent;
        [SerializeField] private Transform _roomsParent;
        [Header("Prefabs")]
        [SerializeField] private GameObject _floorPrefab;
        [SerializeField] private GameObject _endWallPrefab;
        [SerializeField] private GameObject _noRoomWallPrefab;
        
        private const float ROW_LENGTH = 9;
        private const float ROW_WIDTH = 12;
        private const float ROOM_WIDTH = 7;

        private void OnEnable()
        {
            ShowroomGenerationEvents.OnRoomsSelection += GenerateRooms;
        }
        private void OnDisable()
        {
            ShowroomGenerationEvents.OnRoomsSelection -= GenerateRooms;
        }

        private void GenerateRooms(List<GameObject> rooms) 
        {
            _floorsParent.ClearChildren();
            _wallsParent.ClearChildren();
            _roomsParent.DisableChildren();
            
            var rowsCount = rooms.Count / 2 + rooms.Count % 2;
            var roomIndex = 0;

            // Generate Floors And
            for (int i = 0; i < rowsCount; i++)
            {
                var floor = Instantiate(_floorPrefab, _floorsParent);
                floor.transform.position = new Vector3(0,0, i * ROW_LENGTH);

                for (int r = -1; r < 2; r += 2)
                {
                    GameObject room;

                    if(roomIndex < rooms.Count && rooms[roomIndex] != null) 
                    {
                        room = rooms[roomIndex];
                        room.SetActive(true);
                    }
                    else 
                    {
                        room = Instantiate(_noRoomWallPrefab, _wallsParent);
                    }

                    room.transform.position = new Vector3(((ROW_WIDTH / 2f) + (ROOM_WIDTH / 2f)) * r, 0, (i) * ROW_LENGTH);
                    room.transform.localEulerAngles = new Vector3(0, r == -1 ? 0 : 180, 0);

                    roomIndex++;
                }
            }

            // Generate End wall
            var endWall = Instantiate(_endWallPrefab, _wallsParent);
            endWall.transform.position = new Vector3(0, 0, (rowsCount) * ROW_LENGTH);
        }
    }
}