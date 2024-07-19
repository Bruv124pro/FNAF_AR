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

    void Awake()
    {
        coolTime.interactable = false;
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

        if (elapsedTime == 100)
        {
            elapsedTime = 0;
            coolTime.value = elapsedTime;
        }

    }
}
