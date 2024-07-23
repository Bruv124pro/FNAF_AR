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
        animatronics.ShaderSetAlphaValue();

        if (animatronics.transform.position != Camera.main.transform.position)
        {
            animatronics.transform.position -= Camera.main.transform.position;
            
        }
        else
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.jumpScareState);
        }
    }

    public void Exit()
    {
    }
}