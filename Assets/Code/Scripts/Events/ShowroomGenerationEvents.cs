using System;
using System.Collections.Generic;

public static class ShowroomGenerationEvents
{
    public static Action<List<GalleryRoom>> OnRoomsInitialization;
    public static Action<List<GalleryRoom>> OnRoomsSelection;
    public static Action<RoomTagFlags> OnNewRoomsRequest;
}
