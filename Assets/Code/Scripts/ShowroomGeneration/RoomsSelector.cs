using KronosTech.Loaders;
using KronosTech.ShowroomGeneration;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class RoomsSelector
{
    private static readonly List<RoomInfoTags> _roomTags = new();

    public static event Action<List<GalleryRoom>> OnSelection;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        LoaderRooms.OnRoomsInstantiated += GetComponentRoomTags;
        TagSelector.OnNewRequest += SelectNewRooms;
    }

    private static void GetComponentRoomTags(List<GalleryRoom> rooms)
    {
        foreach (var room in rooms)
        {
            _roomTags.Add(room.GetComponent<RoomInfoTags>());
        }
    }
    private static void SelectNewRooms(RoomTagFlags flags)
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