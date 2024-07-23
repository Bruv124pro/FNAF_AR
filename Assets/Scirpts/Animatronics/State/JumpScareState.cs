using System.Collections;
using UnityEngine;
public class JumpScareState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;

    private ShockButton shockButton;
    private int MinShockTime;
    private int MaxShockTime;
    private float elapsedTime;

    public JumpScareState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }

    public void Enter()
    {
        animatronics.PlayAnimation("FreddyCharge 1");
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;
        Debug.Log(elapsedTime);

        if (shockButton.isShockPressed)
        {
            if (MinShockTime < elapsedTime && elapsedTime < MaxShockTime)
            {
                controller.StateMachine.TransitionTo(controller.StateMachine.attackFailState);
            }
            else
            {
                controller.StateMachine.TransitionTo(controller.StateMachine.attackSuccessState);
                elapsedTime = 0;
            }
        }
        else
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.attackSuccessState);
            elapsedTime = 0;
        }
    }

    public void Exit()
    {
        if (animatronics.HpCheck())
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
        }

        else
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.attackFailState);
        }
    }
}