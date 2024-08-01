using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Dialog : UI_Popup
{
    enum Texts
    {
        TitleText,
    }

    enum Images
    {
        RawImage
    }

    enum Buttons
    {
        Confirm
    }

    string _title;
    string _confirm;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Confirm).gameObject.BindEvent(OnClickYesButton);
        GetImage((int)Images.RawImage);
        GetText((int)Texts.TitleText).text = _title;

        return true;
    }

    public void SetDialog(Action onClickYesButton, string title, string confirm)
    {
        _onClickYesButton = onClickYesButton;
        _title = title;
        _confirm = confirm;
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
