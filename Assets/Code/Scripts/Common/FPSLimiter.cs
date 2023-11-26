using UnityEngine;

public static class FPSLimiter
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LimitFPS()
    {
        Application.targetFrameRate = 60;
    }
}
