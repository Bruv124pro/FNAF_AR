﻿using UnityEngine;

public class RepositionState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public RepositionState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
    }

    public void Update()
    {

    }

    public void Exit()
    {
        controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
    }
}