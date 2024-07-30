using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static Define;

public class UI_InGamePopup : UI_Popup
{
    [SerializeField] private Image flashButton;
    [SerializeField] private Sprite onButton;
    [SerializeField] private Sprite offButton;
    [SerializeField] private Volume volume;
    [SerializeField] private Button shockButton;
    [SerializeField] private Image shock;
    [SerializeField] private Sprite shockImage;
    [SerializeField] private Sprite shockBackgroundImage;
    [SerializeField] private Slider coolTime;
    [SerializeField] private int elapsedTime;
    [SerializeField] private BatteryUse battery;
    [SerializeField] private Animatronics animatronics;

    private ShadowsMidtonesHighlights shadow;
    private Vignette vignette;
    public bool isFlashPressed { get; private set; }
    public bool isShockPressed { get; private set; }

    private Slider batterySlider;

    enum Texts
    {
        BatteryAmountText
    }

    enum Images
    {
        ShockImage,
        ShockCoolDownImage,
        FlashOnImage,
        FlashOffImage
    }

    enum Buttons
    {
        ShockAttackButton,
        FlashLightButton,
        CancleButton
    }

    enum Sliders
    {
        BatterySlider
    }

    Action _onClickFlashButton;
    Action _onClickShockButton;

    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        if (volume.profile.TryGet<Vignette>(out vignette) && volume.profile.TryGet<ShadowsMidtonesHighlights>(out shadow))
        {
            VignetteValueChange("off");
        }

        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));

        GetButton((int)Buttons.ShockAttackButton).gameObject.BindEvent(OnClickShockButton);
        GetButton((int)Buttons.FlashLightButton).gameObject.BindEvent(OnClickFlashButton);

        batterySlider = GetSlider((int)Sliders.BatterySlider);
        if (batterySlider != null)
        {
            batterySlider.interactable = false;
        }
        else
        {
            Debug.LogError("BatterySlider not found");
        }

        coolTime.interactable = false;
        isShockPressed = false;
        shock.sprite = shockImage;
        elapsedTime = 100;

        return true;
    }

    void Update()
    {
        if (battery.batteryAmount <= 0)
        {
            VignetteValueChange("off");
        }

        if (elapsedTime < 100)
        {
            shock.sprite = shockBackgroundImage;
            elapsedTime += 1;
            coolTime.value = elapsedTime;
        }

        if (elapsedTime == 100)
        {
            shock.sprite = shockImage;
        }
    }

    void OnClickShockButton()
    {
        if (battery.batteryAmount > 10 && elapsedTime == 100)
        {
            isShockPressed = true;
            if (animatronics.isJumpState && animatronics.IsVisibleInMonitor())
            {
                animatronics.HitElecParticle(true);
                animatronics.isHitElectronic = true;
            }
            else
            {
                animatronics.HitElecParticle(false);
            }
            elapsedTime = 0;
            coolTime.value = elapsedTime;
        }
        else if (elapsedTime < 100 && isShockPressed)
        {
            isShockPressed = !isShockPressed;
        }

        if (_onClickShockButton != null)
        {
            _onClickShockButton.Invoke();
        }
        Debug.Log("Shock button 클릭");
    }

    void OnClickFlashButton()
    {
        if (battery.batteryAmount > 0 && flashButton.sprite == offButton)
        {
            VignetteValueChange("on");
        }
        else
        {
            VignetteValueChange("off");
        }

        if (_onClickFlashButton != null)
        {
            _onClickFlashButton.Invoke();
        }
        Debug.Log("Flash button 클릭");
    }

    private void VignetteValueChange(string vignetteValue)
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

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using static Define;

//public class UI_InGamePopup : UI_Popup
//{
//    private ShockButton shock;
//    private FlashLightButton flash;
//    private Slider batterySlider;

//    enum Texts
//    {
//        BatteryAmountText
//    }

//    enum Images
//    {
//        ShockImage,
//        ShockCoolDownImage,
//        FlashOnImage,
//        FlashOffImage
//    }

//    enum Buttons
//    {
//        ShockAttackButton,
//        FlashLightButton,
//        CancleButton
//    }

//    enum Sliders
//    {
//        BatterySlider
//    }

//    Action _onClickFlashButton;
//    Action _onClickShockButton;

//    public override bool Init()
//    {
//        if(base.Init() == false)
//        {
//            return false;
//        }

//        shock = GetComponent<ShockButton>();
//        flash = GetComponent<FlashLightButton>();

//        BindButton(typeof(Buttons));
//        BindSlider(typeof(Sliders));

//        GetButton((int)Buttons.ShockAttackButton).gameObject.BindEvent(OnClickShockButton);
//        GetButton((int)Buttons.FlashLightButton).gameObject.BindEvent(OnClickFlashButton);

//        batterySlider = GetSlider((int)Sliders.BatterySlider);
//        if(batterySlider != null)
//        {
//            batterySlider.interactable = false;
//        }

//        return true;
//    }

//    void OnClickShockButton()
//    {
//        shock.ButtonClick();
//        if(_onClickShockButton != null)
//        {
//            _onClickShockButton.Invoke();
//        }
//    }

//    void OnClickFlashButton()
//    {
//        flash.ButtonClick();
//        if (_onClickFlashButton != null)
//        {
//            _onClickFlashButton.Invoke();
//        }
//    }
//}
