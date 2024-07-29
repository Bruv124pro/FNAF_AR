using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_InGamePopup : UI_Popup
{
    private ShockButton shock;
    private FlashLightButton flash;
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
        if(base.Init() == false)
        {
            return false;
        }

        shock = GetComponent<ShockButton>();
        flash = GetComponent<FlashLightButton>();

        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));

        GetButton((int)Buttons.ShockAttackButton).gameObject.BindEvent(OnClickShockButton);
        GetButton((int)Buttons.FlashLightButton).gameObject.BindEvent(OnClickFlashButton);

        batterySlider = GetSlider((int)Sliders.BatterySlider);
        if(batterySlider != null)
        {
            batterySlider.interactable = false;
        }

        return true;
    }

    void OnClickShockButton()
    {
        shock.ButtonClick();
        if(_onClickShockButton != null)
        {
            _onClickShockButton.Invoke();
        }
    }

    void OnClickFlashButton()
    {
        flash.ButtonClick();
        if (_onClickFlashButton != null)
        {
            _onClickFlashButton.Invoke();
        }
    }
}
