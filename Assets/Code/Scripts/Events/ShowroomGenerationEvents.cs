using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShowroomGenerationEvents
{
    public static Action<List<GameObject>> OnRoomsInitialization;
    public static Action<List<GameObject>> OnRoomsSelection;
    public static Action<RoomTagFlags> OnNewRoomsRequest;
}
