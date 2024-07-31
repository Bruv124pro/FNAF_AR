using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_GameOverPopup : UI_Popup
{
    enum Texts
    {
        GameOverText
    }

    public override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        GetText((int)Texts.GameOverText).gameObject.SetActive(true);

        return true;
    }
}
