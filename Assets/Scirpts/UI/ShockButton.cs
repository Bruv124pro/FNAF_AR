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
    [SerializeField] private Animatronics animatronics;

    public bool isShockPressed;

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
        if (battery.batteryAmount > 10 && elapsedTime == 100)
        {
            isShockPressed = true;
            if (animatronics.isJumpState)
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
    }
}
