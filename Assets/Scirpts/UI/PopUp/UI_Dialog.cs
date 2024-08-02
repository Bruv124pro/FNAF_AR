using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Dialog : UI_Popup
{
    enum Texts
    {
        Name,
    }

    enum Images
    {
        RawImage
    }

    enum Buttons
    {
        EnCounter
    }

    string _name;
    string _encounter;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.EnCounter).gameObject.BindEvent(OnClickYesButton);
        GetImage((int)Images.RawImage);
        GetText((int)Texts.Name).text = _name;

        return true;
    }

    public void SetDialog(Action onClickYesButton, string name, string encounter)
    {
        _onClickYesButton = onClickYesButton;
        _name = name;
        _encounter = encounter;
    }

    Action _onClickYesButton;
    void OnClickYesButton()
    {
        Managers.UI.ClosePopupUI(this);
        if (_onClickYesButton != null)
            _onClickYesButton.Invoke();
    }

    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
