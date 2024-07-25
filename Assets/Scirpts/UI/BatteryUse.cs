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

    public int batteryAmount;
    private bool isBatteryCoroutineRunning = false;
    private float elapsedTime = 0;

    void Start()
    {
        batteryAmount = 100;
    }

    void Update()
    {
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

        elapsedTime += Time.deltaTime;

        if (!flash.isFlashPressed)
        {
            StopCoroutine(BatteryAmountDown());
            isBatteryCoroutineRunning = false;
        }
    }

    public void ShockPressedCheck()
    {
        if (shock.isShockPressed && batteryAmount > 10)
        {
            batteryAmount -= 10;
        }
    }

    public void FlashPressedCheck()
    {
        if (flash.isFlashPressed && batteryAmount >= 3 && !isBatteryCoroutineRunning)
        {
            batteryAmount -= 3;
            if (elapsedTime > 1)
            {
                StartCoroutine(BatteryAmountDown());
            }
        }
    }

    IEnumerator BatteryAmountDown()
    {
        isBatteryCoroutineRunning = true;
        while (flash.isFlashPressed && batteryAmount > 0)
        {
            yield return new WaitForSeconds(1);
            batteryAmount -= 1;
        }
        isBatteryCoroutineRunning = false;
        elapsedTime = 0;
    }
}
