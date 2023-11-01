using System.Collections.Generic;
using UnityEngine;

public class RoomsSelector : MonoBehaviour
{
    private readonly List<RoomTags> _roomTags = new();

    private void OnEnable()
    {
        ShowroomGenerationEvents.OnRoomsInitialization += GetRoomTags;
        ShowroomGenerationEvents.OnNewRoomsRequest += SelectNewRooms;
    }
    private void OnDisable()
    {
        ShowroomGenerationEvents.OnRoomsInitialization -= GetRoomTags;
        ShowroomGenerationEvents.OnNewRoomsRequest -= SelectNewRooms;
    }

    private void GetRoomTags(List<GalleryRoom> rooms)
    {
        foreach (var room in rooms)
        {
            _roomTags.Add(room.GetComponent<RoomTags>());
        }
    }

    [ContextMenu("Initialize Selected Rooms")]
    private void SelectNewRooms(RoomTagFlags flags)
    {
        var selectedRooms = new List<GalleryRoom>();

        for (int i = 0; i < _roomTags.Count; i++)
        {
            if(_roomTags[i].Tags.HasAny(flags))
            {
                selectedRooms.Add(_roomTags[i].GetComponent<GalleryRoom>());
            }
        }

        ShowroomGenerationEvents.OnRoomsSelection?.Invoke(selectedRooms);
    }
}