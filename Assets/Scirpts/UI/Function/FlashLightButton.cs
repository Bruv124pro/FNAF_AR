using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class FlashLightButton : MonoBehaviour
{
    [SerializeField] private Image flashButton;
    [SerializeField] private Sprite onButton;
    [SerializeField] private Sprite offButton;
    [SerializeField] private Volume volume;

    private ShadowsMidtonesHighlights shadow;
    private Vignette vignette;
    public bool isFlashPressed { get; private set; }

    [SerializeField] private BatteryUse battery;

    private void Awake()
    {
        VignetteValueChange("off");
    }

    private void Update()
    {
        if (battery.batteryAmount <= 0)
        {
            VignetteValueChange("off");
        }
    }

    public void FlashButtonClick()
    {
        if (battery.batteryAmount > 0 && flashButton.sprite == offButton)
        {
            VignetteValueChange("on");
        }

        else
        {
            VignetteValueChange("off");
        }
    }

    private void VignetteValueChange(string vignetteValue)
    {

        if (volume.profile.TryGet<Vignette>(out vignette) && volume.profile.TryGet<ShadowsMidtonesHighlights>(out shadow))
        {
            if (vignetteValue == "on")
            {
                isFlashPressed = true;
                flashButton.sprite = onButton;

                vignette.intensity.value = 0.5f;
                shadow.shadows.SetValue(new Vector4Parameter(new Vector4(0, 0, 0, 0.25f)));
            }
            else if (vignetteValue == "off")
            {
                isFlashPressed = false;
                flashButton.sprite = offButton;

                vignette.intensity.value = 0.65f;
                shadow.shadows.SetValue(new Vector4Parameter(new Vector4(0, 0, 0, -0.3f)));
            }
        }
    }
}
