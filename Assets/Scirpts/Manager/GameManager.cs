using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public Volume volume;
    public ShadowsMidtonesHighlights shadow;
    public Vignette vignette;

    public int BatteryAmount;
    public int ElapsedTime;
    public bool IsFlashPressed;
    public bool IsShockPressed;
    public bool IsBatteryCoroutineRunning;

    private float elapsedTimeBattery;
    private IEnumerator batteryEnumerator;

    public void Initialize()
    {
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ShadowsMidtonesHighlights>(out shadow);

        VignetteValueChange("off");

        BatteryAmount = 100;
        ElapsedTime = 100;
        batteryEnumerator = BatteryAmountDown();
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

                if (!IsBatteryCoroutineRunning && BatteryAmount > 0)
                {
                    StartCoroutine(batteryEnumerator);
                }
            }
            else if (vignetteValue == "off")
            {
                IsFlashPressed = false;
                vignette.intensity.value = 0.65f;
                shadow.shadows.SetValue(new Vector4Parameter(new Vector4(0, 0, 0, -0.3f)));

                if (IsBatteryCoroutineRunning)
                {
                    StopCoroutine(batteryEnumerator);
                    IsBatteryCoroutineRunning = false;
                }
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
            if (elapsedTimeBattery > 1f)
            {
                StartCoroutine(batteryEnumerator);
            }
        }
    }

    private IEnumerator BatteryAmountDown()
    {
        IsBatteryCoroutineRunning = true;
        elapsedTimeBattery = 0;
        while (IsFlashPressed && BatteryAmount > 0)
        {
            yield return new WaitForSeconds(1);
            BatteryAmount -= 1;
        }
        IsBatteryCoroutineRunning = false;
    }
}
