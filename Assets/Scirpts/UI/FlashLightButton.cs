using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private ShadowsMidtonesHighlights shadow;

    private Vignette vignette;

    private void Awake()
    {
        if (volume.profile.TryGet<Vignette>(out vignette) && volume.profile.TryGet<ShadowsMidtonesHighlights>(out shadow))
        {
            vignette.intensity.value = 0.6f;
            shadow.shadows.SetValue(new Vector4Parameter(new Vector4(0, 0, 0, -0.6f)));
        }
    }

    public void ButtonClick()
    {
        if (flashButton.sprite == offButton)
        {
            flashButton.sprite = onButton;
            if (volume.profile.TryGet<Vignette>(out vignette) && volume.profile.TryGet<ShadowsMidtonesHighlights>(out shadow))
            {
                vignette.intensity.value = 0.45f;
                shadow.shadows.SetValue(new Vector4Parameter(new Vector4(0, 0, 0, 0.2f)));
            }
        }
        else
        {
            flashButton.sprite = offButton;

            if (volume.profile.TryGet<Vignette>(out vignette) && volume.profile.TryGet<ShadowsMidtonesHighlights>(out shadow))
            {
                vignette.intensity.value = 0.6f;
                shadow.shadows.SetValue(new Vector4Parameter(new Vector4(0, 0, 0, -0.6f)));
            }
        }
    }
}
