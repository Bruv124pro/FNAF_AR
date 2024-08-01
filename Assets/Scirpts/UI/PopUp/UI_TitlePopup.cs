using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitlePopup : UI_Popup
{
    [SerializeField] Sprite sprite;
    [SerializeField] Image image;
    [SerializeField] Material material;

    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        image.color = new Color(255, 255, 255, 0);
        gameObject.SetActive(true);

        return true;
    }

    private void Start()
    {
        image.sprite = sprite;
        FadeStart();
    }

    void FadeStart()
    {
        StartCoroutine(Fade());
        material.SetFloat("_Force", 0);
    }

    IEnumerator Fade()
    {
        float startAlpha = 0;
        while (startAlpha < 1.0f)
        {
            startAlpha += 0.1f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(255, 255, 255, startAlpha);
        }
        StartCoroutine(GlitchPlay());
    }

    IEnumerator GlitchPlay()
    {
        material.SetFloat("_Force", 100);
        yield return new WaitForSeconds(0.5f);
        material.SetFloat("_Force", 0);
        yield return new WaitForSeconds(2f);
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_SelectAnimatronics>();
    }
}
