using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler
{
    public Action OnClickHandler = null;
    public Action OnPressedHandler = null;

    bool _pressed = false;

    private void Update()
    {
        if(_pressed)
        {
            OnPressedHandler?.Invoke();
        }
    }

    public  void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke();
    }
}
