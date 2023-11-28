using System;

[Flags]
public enum RoomTagFlags
{
    VR = 1 << 1,
    AR = 1 << 2,
    WebGL = 1 << 3,
    Product = 1 << 4,
    Game = 1 << 5,
    Mobile = 1 << 6,
    Upwork = 1 << 7
}
