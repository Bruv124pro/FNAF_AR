using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGamePopup : UI_Popup
{
    [SerializeField] public Image flashButton;
    [SerializeField] public Sprite onButton;
    [SerializeField] public Sprite offButton;
    [SerializeField] public Button shockButton;
    [SerializeField] public Image shock;
    [SerializeField] public Sprite shockImage;
    [SerializeField] public Sprite shockBackgroundImage;
    [SerializeField] public Slider coolTime;
    [SerializeField] public Slider batterySlider;
    [SerializeField] public Text batteryText;
    private enum Texts
    {
        BatteryAmount
    }
    private enum Buttons
    {
        ShockButton,
        FlashLightButton
    }

    private enum Sliders
    {
        ShockCooltime,
        BatterySlider
    }

    private enum Images
    {
        onButton,
        offButton,
        shockImage,
        shockBackgroundImage,

    }
    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));

        GetButton((int)Buttons.ShockButton).gameObject.BindEvent(OnClickShockButton);
        GetButton((int)Buttons.FlashLightButton).gameObject.BindEvent(OnClickFlashButton);

        batterySlider = GetSlider((int)Sliders.BatterySlider);
        coolTime = GetSlider((int)Sliders.ShockCooltime);

        coolTime.interactable = false;
        
        shock = GetImage((int)Images.shockImage);

        if (batterySlider != null)
        {
            batterySlider.interactable = false;
        }
        else
        {
            Debug.LogError("BatterySlider not found");
        }

        return true;
    }

    void Update()
    {
        if (Managers.GameManager.BatteryAmount <= 0)
        {
            Managers.GameManager.VignetteValueChange("off");
        }

        if (Managers.GameManager.ElapsedTime < 100)
        {
            shock = GetImage((int)Images.shockBackgroundImage);
            Managers.GameManager.ElapsedTime += 1;
            coolTime.value = Managers.GameManager.ElapsedTime;
        }

        if (Managers.GameManager.ElapsedTime == 100)
        {
            shock = GetImage((int)Images.shockImage);
        }

        batterySlider.value = Managers.GameManager.BatteryAmount;
        GetText((int)Texts.BatteryAmount).text = $"{Managers.GameManager.BatteryAmount}";
    }

    void OnClickShockButton()
    {
        if (Managers.GameManager.BatteryAmount > 10 && Managers.GameManager.ElapsedTime == 100)
        {
            Managers.GameManager.IsShockPressed = true;
            Managers.GameManager.ElapsedTime = 0;
            coolTime.value = Managers.GameManager.ElapsedTime;
            Managers.GameManager.ShockPressedCheck();
        }
        else if (Managers.GameManager.ElapsedTime < 100 && Managers.GameManager.IsShockPressed)
        {
            Managers.GameManager.IsShockPressed = !Managers.GameManager.IsShockPressed;
        }
    }

    void OnClickFlashButton()
    {
        if (Managers.GameManager.BatteryAmount > 0 && flashButton.sprite == offButton)
        {
            Managers.GameManager.VignetteValueChange("on");
        }
        else
        {
            Managers.GameManager.VignetteValueChange("off");
        }

        Managers.GameManager.FlashPressedCheck();
    }

}
