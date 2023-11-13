using KronosTech.ShowroomGeneration;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomsSelector : MonoBehaviour
{
    private readonly List<RoomInfoTags> _roomTags = new();

    public static event Action<List<GalleryRoom>> OnSelection;

    private void OnEnable()
    {
        RoomsHolder.OnInitialization += GetRoomTags;
        TagSelector.OnNewRequest += SelectNewRooms;
    }
    private void OnDisable()
    {
        RoomsHolder.OnInitialization -= GetRoomTags;
        TagSelector.OnNewRequest -= SelectNewRooms;
    }

    private void GetRoomTags(List<GalleryRoom> rooms)
    {
        foreach (var room in rooms)
        {
            _roomTags.Add(room.GetComponent<RoomInfoTags>());
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

        OnSelection?.Invoke(selectedRooms);
    }
}