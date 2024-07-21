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
        Debug.Log("ChargeEnter");

        animatronics.PlayAnimation("FreddyCharge");

        if (animatronics.ShouldJumpScare())
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.jumpScareState);
        }
        else
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
        }
    }

    public void Update()
    {

    }

    public void Exit()
    {
    }
}