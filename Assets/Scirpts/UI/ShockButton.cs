using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShockButton : MonoBehaviour
{
    [SerializeField] private Button shockButton;
    [SerializeField] private Image shock;
    [SerializeField] private Sprite shockImage;
    [SerializeField] private Sprite shockBackgroundImage;
    [SerializeField] private Slider coolTime;

    [SerializeField] private int elapsedTime;

    [SerializeField] private BatteryUse battery;
    public bool isShockPressed { get; private set; }

    void Awake()
    {
        coolTime.interactable = false;
        isShockPressed = false;
        shock.sprite = shockImage;
        elapsedTime = 100;
    }

    void Update()
    {
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

    public void ButtonClick()
    {
        if (battery.batteryAmount > 0 && elapsedTime == 100)
        {
            isShockPressed = true;
            elapsedTime = 0;
            coolTime.value = elapsedTime;
        }

        else if (elapsedTime < 100 && isShockPressed)
        {
            isShockPressed = !isShockPressed;
        }
    }
}
