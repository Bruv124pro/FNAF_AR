using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

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

    [SerializeField] public VisualEffect[] missParticle;
    [SerializeField] public VisualEffect[] succParticle;

    public bool isShockPressed;

    void Awake()
    {
        coolTime.interactable = false;
        isShockPressed = false;
        shock.sprite = shockImage;
        elapsedTime = 100;

        foreach (var effect in missParticle)
        {
            effect.transform.GetComponent<VisualEffect>();
        }
        foreach (var effect in succParticle)
        {
            effect.transform.GetComponent<VisualEffect>();
        }

        ElecEffectOff(true);
        ElecEffectOff(false);
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

    public void ShockButtonClick()
    {
        if (battery.batteryAmount > 10 && elapsedTime == 100)
        {
            isShockPressed = true;
            if (animatronics.isJumpState && animatronics.IsVisibleInMonitor())
            {
                HitElecParticle(true);
                animatronics.isHitElectronic = true;
                
            }
            else
            {
                HitElecParticle(false);
            }
            elapsedTime = 0;
            coolTime.value = elapsedTime;
        }

        else if (elapsedTime < 100 && isShockPressed)
        {
            isShockPressed = !isShockPressed;
        }
    }

    public void ElecEffectOn(bool isSuccAtack)
    {
        if (isSuccAtack)
        {
            foreach (VisualEffect p in succParticle)
            {
                p.Play();
            }
        }
        else
        {
            foreach (VisualEffect p in missParticle)
            {
                p.Play();
            }
        }

    }

    public void ElecEffectOff(bool isSuccAtack)
    {
        if (isSuccAtack)
        {
            foreach (VisualEffect p in succParticle)
            {
                p.Stop();
            }
        }
        else
        {
            foreach (VisualEffect p in missParticle)
            {
                p.Stop();
            }
        }
    }

    public void HitElecParticle(bool isSuccAtack)
    {
        ElecEffectOn(isSuccAtack);
        StartCoroutine(HitElecParticleFinish(isSuccAtack));
    }

    IEnumerator HitElecParticleFinish(bool isSuccAtack)
    {
        yield return new WaitForSeconds(1f);
        ElecEffectOff(isSuccAtack);
        animatronics.isJumpState = false;
    }
}
