using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUse : MonoBehaviour
{
    [SerializeField] FlashLightButton flash;
    [SerializeField] ShockButton shock;

    [SerializeField] private Slider batterySlider;
    [SerializeField] private Text batteryText;

    public int batteryAmount { get; private set; }

    void Start()
    {
        batteryAmount = 100;
    }

    void Update()
    {
        batterySlider.value = batteryAmount;
        batteryText.text = $"{batteryAmount}";
    }

    public void ShockPressedCheck()
    {
        if (shock.isShockPressed && batteryAmount > 0)
        {
            batteryAmount -= 10;
        }
    }
    
    public void FlashPressedCheck()
    {
        if (flash.isFlashPressed && batteryAmount > 0)
        {
            batteryAmount -= 3;
            StartCoroutine(BatteryAmountDown());
        }
    }

    IEnumerator BatteryAmountDown()
    {
        while (batteryAmount > 0 && flash.isFlashPressed)
        {
            yield return new WaitForSeconds(1);
            batteryAmount -= 1;
        }
    }
}
