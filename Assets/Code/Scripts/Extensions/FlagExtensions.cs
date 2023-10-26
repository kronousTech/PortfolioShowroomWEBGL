using System;

public static class FlagExtensions 
{
    public static bool HasAny(this Enum me, Enum other)
    {
        return (Convert.ToInt32(me) & Convert.ToInt32(other)) != 0;
    }
}
