using System;
using System.Collections;
using UnityEngine;
public class InvisibleFeintState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public InvisibleFeintState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
        Debug.Log("InvisibleFeintState");
        animatronics.PlayAnimation(controller.animatronics.selectVisibleAnimation());
        animatronics.SetVisible();
        animatronics.OnVisibleFinished += OnVisibleFinished;
    }

    public void Update()
    {
        if (animatronics.IsVisibleInMonitor())
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
        }
    }

    public void Exit()
    {
        animatronics.OnVisibleFinished -= OnVisibleFinished;
    }

    private void OnVisibleFinished()
    {
        controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
    }
}