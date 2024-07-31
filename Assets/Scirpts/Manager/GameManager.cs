using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Volume volume;
    public ShadowsMidtonesHighlights shadow;
    public Vignette vignette;

    public int BatteryAmount { get; set; }
    public int ElapsedTime { get; set; }
    public bool IsFlashPressed { get; set; }
    public bool IsShockPressed { get; set; }
    public bool IsBatteryCoroutineRunning { get; private set; }

    public float batteryTimer = 0;
    public float batteryDrainInterval = 1.0f;

    public void Init()
    {
        volume = FindObjectOfType<Volume>();

        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ShadowsMidtonesHighlights>(out shadow);

        IsFlashPressed = false;
        IsShockPressed = false;

        BatteryAmount = 100;
        ElapsedTime = 100;

        VignetteValueChange("off");
    }

    private void Update()
    {

    }

    public void VignetteValueChange(string vignetteValue)
    {
        if (vignette && shadow)
        {
            if (vignetteValue == "on")
            {
                IsFlashPressed = true;
                vignette.intensity.value = 0.5f;
                shadow.shadows.SetValue(new Vector4Parameter(new Vector4(0, 0, 0, 0.25f)));
            }
            else if (vignetteValue == "off")
            {
                IsFlashPressed = false;
                vignette.intensity.value = 0.65f;
                shadow.shadows.SetValue(new Vector4Parameter(new Vector4(0, 0, 0, -0.3f)));
            }
        }
    }

    public void ShockPressedCheck()
    {
        if (IsShockPressed && BatteryAmount >= 10)
        {
            BatteryAmount -= 10;
        }
    }

    public void FlashPressedCheck()
    {
        if (IsFlashPressed && BatteryAmount >= 3 && !IsBatteryCoroutineRunning)
        {
            BatteryAmount -= 3;
        }
    }
}
