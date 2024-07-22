using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChargeState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;

    private Material bodyShader;
    private float alpha;

    public ChargeState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
        this.bodyShader = controller.bodyShader;
    }
    public void Enter()
    {
        animatronics.PlayAnimation("FreddyCharge");
    }

    public void Update()
    {
        if (alpha > 0)
        {
            alpha = bodyShader.GetFloat("_Alpha");
            alpha -= 0.1f;
            bodyShader.SetFloat("_Alpha", alpha);
        }
    }

    public void Exit()
    {
    }
}