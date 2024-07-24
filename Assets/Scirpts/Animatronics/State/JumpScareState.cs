using System.Collections;
using UnityEngine;
public class JumpScareState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;

    private float MinShockTime;
    private float MaxShockTime;
    private float elapsedTime;

    public JumpScareState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }

    public void Enter()
    {
        MinShockTime = 0;
        MaxShockTime = animatronics.InitmaxShockTime();
        animatronics.PlayAnimation("FreddyCharge 1");
        elapsedTime = 0;
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;

        if (MinShockTime < elapsedTime)
        {
            animatronics.isJumpState = true;
            if (animatronics.isHitElectronic)
            {
                controller.StateMachine.TransitionTo(controller.StateMachine.attackFailState);
                animatronics.isHitElectronic = false;
            }
        }

        if (elapsedTime > MaxShockTime)
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.attackSuccessState);
        }

    }

    public void Exit()
    {
        animatronics.isJumpState = false;
        elapsedTime = 0;
    }
}