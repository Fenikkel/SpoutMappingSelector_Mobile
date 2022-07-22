using UnityEngine;

public class VibrationController : MonoBehaviour
{
    public void Vibrate(int milliseconds)//(long milliseconds)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Fenikkel.Vibrate(milliseconds);
#else
        Handheld.Vibrate();
#endif
    }

    public void VibratePattern(long[] pattern, int repeat)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Fenikkel.Vibrate(pattern, repeat);
#else
        Handheld.Vibrate();
#endif
    }
}
