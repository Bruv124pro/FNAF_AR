using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLightButton : MonoBehaviour
{
    [SerializeField] private Image flashButton;
    [SerializeField] private Sprite onButton;
    [SerializeField] private Sprite offButton;

    public void ButtonClick()
    {
        if (flashButton.sprite == offButton)
        {
            Debug.Log("ÇÃ·¡½Ã ÄÑÁü");
            flashButton.sprite = onButton;
        }
        else
        {
            Debug.Log("ÇÃ·¡½Ã ²¨Áü");
            flashButton.sprite = offButton;
        }
    }
}
