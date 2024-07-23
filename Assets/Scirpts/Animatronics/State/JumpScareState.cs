using System.Collections;
using UnityEngine;
public class JumpScareState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;

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

        if (MinShockTime < elapsedTime)
        {
            animatronics.ShockPress();
        }

        if (elapsedTime > MaxShockTime)
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.attackSuccessState);
        }

    }

    public void Exit()
    {
        elapsedTime = 0;
        Debug.Log("jumpExit");
    }
}