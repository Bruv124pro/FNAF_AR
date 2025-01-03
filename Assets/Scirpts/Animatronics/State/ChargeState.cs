﻿using System.Collections;
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
        Debug.Log("ChargeState");
        animatronics.PlayAnimation("Charge");
        
        animatronics.ChargeToJumpScare += ChangeChargeState;

        animatronics.PlaySound(animatronics.ChargeAudioClip);

        animatronics.ChangeChargeToJumpScare();
    }

    public void Update()
    {
        animatronics.ShaderSetAlphaValue();
    }

    public void Exit()
    {
        animatronics.ChargeToJumpScare -= ChangeChargeState;
    }

    public void ChangeChargeState()
    {
        if (animatronics.ShouldJumpScare())
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.jumpScareState);
        }
        else
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
        }
    }
}