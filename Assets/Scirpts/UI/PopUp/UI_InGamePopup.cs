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
    [SerializeField] public Image flashButton;
    [SerializeField] public Sprite onButton;
    [SerializeField] public Sprite offButton;
    [SerializeField] public Volume volume;
    [SerializeField] public Button shockButton;
    [SerializeField] public Image shock;
    [SerializeField] public Sprite shockImage;
    [SerializeField] public Sprite shockBackgroundImage;
    [SerializeField] public Slider coolTime;
    [SerializeField] public int elapsedTime;
                     
    [SerializeField] public Slider batterySlider;
    [SerializeField] public Text batteryText;

    public int batteryAmount;

    public ShadowsMidtonesHighlights shadow;
    public Vignette vignette;
    public bool isFlashPressed { get; private set; }
    public bool isShockPressed { get; private set; }

    public bool isBatteryCoroutineRunning = false;
    public float elapsedTimeBattery = 0;
    public IEnumerator batteryEnumerator;

    enum Texts
    {
        BatteryAmountText
    }

    //enum Images
    //{
    //    ShockImage,
    //    ShockCoolDownImage,
    //    FlashOnImage,
    //    FlashOffImage
    //}

    enum Buttons
    {
        ShockAttackButton,
        FlashLightButton,
        CancelButton
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

        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ShadowsMidtonesHighlights>(out shadow);

        VignetteValueChange("off");

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

        return true;
    }

    private void Start()
    {
        coolTime.interactable = false;
        isShockPressed = false;
        shock.sprite = shockImage;

        elapsedTime = 100;
        batteryAmount = 100;
        batteryEnumerator = BatteryAmountDown();
    }

    void Update()
    {
        if (batteryAmount <= 0)
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

        if (batteryAmount > 0)
        {
            batterySlider.value = batteryAmount;
            batteryText.text = $"{batteryAmount}";
        }
        else
        {
            batteryAmount = 0;
            batterySlider.value = batteryAmount;
            batteryText.text = $"{batteryAmount}";
        }

        elapsedTimeBattery += Time.deltaTime;

        if (!isFlashPressed)
        {
            StopCoroutine(batteryEnumerator);
            isBatteryCoroutineRunning = false;
        }
    }

    void OnClickShockButton()
    {
        if (batteryAmount > 10 && elapsedTime == 100)
        {
            isShockPressed = true;
            elapsedTime = 0;
            coolTime.value = elapsedTime;
            ShockPressedCheck();
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
        if (batteryAmount > 0 && flashButton.sprite == offButton)
        {
            VignetteValueChange("on");
        }
        else
        {
            VignetteValueChange("off");
        }

        FlashPressedCheck();

        if (_onClickFlashButton != null)
        {
            _onClickFlashButton.Invoke();
        }
        Debug.Log("Flash button 클릭");
    }

    public void VignetteValueChange(string vignetteValue)
    {
        if (vignette && shadow)
        {
            if (vignetteValue == "on")
            {
                isFlashPressed = true;
                flashButton.sprite = onButton;

                vignette.intensity.value = 0.5f;
                shadow.shadows.SetValue(new Vector4Parameter(new Vector4(0, 0, 0, 0.25f)));

                if (!isBatteryCoroutineRunning && batteryAmount > 0)
                {
                    StartCoroutine(batteryEnumerator);
                }
            }
            else if (vignetteValue == "off")
            {
                isFlashPressed = false;
                flashButton.sprite = offButton;

                vignette.intensity.value = 0.65f;
                shadow.shadows.SetValue(new Vector4Parameter(new Vector4(0, 0, 0, -0.3f)));

                StopCoroutine(batteryEnumerator);
                isBatteryCoroutineRunning = false;
            }
        }
    }

    public void ShockPressedCheck()
    {
        if (isShockPressed && batteryAmount >= 10)
        {
            batteryAmount -= 10;
        }
    }

    public void FlashPressedCheck()
    {
        if (isFlashPressed && batteryAmount >= 3 && !isBatteryCoroutineRunning)
        {
            batteryAmount -= 3;
            if (elapsedTimeBattery > 1f)
            {
                StartCoroutine(batteryEnumerator);
            }
        }
    }

    IEnumerator BatteryAmountDown()
    {
        isBatteryCoroutineRunning = true;
        while (isFlashPressed && batteryAmount > 0)
        {
            yield return new WaitForSeconds(1);
            batteryAmount -= 1;
        }
        isBatteryCoroutineRunning = false;
        elapsedTimeBattery = 0;
    }
}