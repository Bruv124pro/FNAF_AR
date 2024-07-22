using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChargeState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;

    public ChargeState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
        animatronics.PlayAnimation("FreddyCharge");
    }

    public void Update()
    {
        animatronics.SetValue();

        if (animatronics.transform.position != new Vector3(0, 0, 1))
        {
            animatronics.transform.position += new Vector3(0, 0, 0.05f);
        }
    }

    public void Exit()
    {
    }
}