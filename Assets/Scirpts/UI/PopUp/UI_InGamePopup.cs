using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGamePopup : UI_Popup
{
    Animatronics animatronics;
    private enum Texts
    {
        BatteryAmount
    }

    private enum Buttons
    {
        ShockButton,
        FlashLightButton,
        CancelButton
    }

    private enum Sliders
    {
        ShockCooltime,
        BatterySlider
    }

    bool isFlashButtonCilcked;

    private enum Images
    {
        onButton,
        ShockImage,
        ShockBackGroundImage,
    }
    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        //animatronics.ElecEffectOff(true);
        //animatronics.ElecEffectOff(false);

        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.ShockButton).gameObject.BindEvent(OnClickShockButton);
        GetButton((int)Buttons.FlashLightButton).gameObject.BindEvent(OnClickFlashButton);
        GetButton((int)Buttons.CancelButton).gameObject.BindEvent(OnClickCancleButton);
        GetImage((int)Images.onButton).gameObject.SetActive(false);

        GetSlider((int)Sliders.BatterySlider).value = 100;
        GetSlider((int)Sliders.ShockCooltime).value = 100;

        GetSlider((int)Sliders.BatterySlider).interactable = false;
        GetSlider((int)Sliders.ShockCooltime).interactable = false;

        return true;
    }

    void Update()
    {
        GetSlider((int)Sliders.BatterySlider).value = Managers.GameManager.BatteryAmount;
        GetText((int)Texts.BatteryAmount).text = $"{Managers.GameManager.BatteryAmount}";

        if (Managers.GameManager.BatteryAmount <= 0)
        {
            Managers.GameManager.VignetteValueChange("off");
        }

        if (Managers.GameManager.ElapsedTime < 100)
        {
            GetImage((int)Images.ShockImage).gameObject.SetActive(false);
            Managers.GameManager.ElapsedTime += 1;
            GetSlider((int)Sliders.ShockCooltime).value = Managers.GameManager.ElapsedTime;
        }

        if (Managers.GameManager.ElapsedTime == 100)
        {
            GetImage((int)Images.ShockImage).gameObject.SetActive(true);
        }

        if (Managers.GameManager.IsFlashPressed && Managers.GameManager.BatteryAmount > 0)
        {
            Managers.GameManager.batteryTimer += Time.deltaTime;
            Debug.Log(Managers.GameManager.batteryTimer);
            if (Managers.GameManager.batteryTimer >= Managers.GameManager.batteryDrainInterval)
            {
                Managers.GameManager.BatteryAmount -= 1;
                Managers.GameManager.batteryTimer = 0;
            }
        }
    }

    void OnClickShockButton()
    {
        if (Managers.GameManager.BatteryAmount > 10 && Managers.GameManager.ElapsedTime == 100)
        {
            Managers.GameManager.IsShockPressed = true;
            Managers.GameManager.ElapsedTime = 0;
            GetSlider((int)Sliders.ShockCooltime).value = Managers.GameManager.ElapsedTime;
            Managers.GameManager.ShockPressedCheck();
        }
        else if (Managers.GameManager.ElapsedTime < 100 && Managers.GameManager.IsShockPressed)
        {
            Managers.GameManager.IsShockPressed = !Managers.GameManager.IsShockPressed;
        }
    }

    void OnClickFlashButton()
    {
        if (Managers.GameManager.BatteryAmount > 0 && !isFlashButtonCilcked)
        {
            Managers.GameManager.VignetteValueChange("on");
            GetImage((int)Images.onButton).gameObject.SetActive(true);
        }

        else
        {
            Managers.GameManager.VignetteValueChange("off");
            GetImage((int)Images.onButton).gameObject.SetActive(false);
        }

        Managers.GameManager.FlashPressedCheck();
        isFlashButtonCilcked = !isFlashButtonCilcked;
    }

    void OnClickCancleButton()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_SelectAnimatronics>();
    }
}
