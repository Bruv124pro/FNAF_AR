using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;
    public float time = 0;

    public IdleState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }

    public void Enter()
    {
    }

    public void Update()
    {
        if(time > 2)
        {
            time = 0;
            if (animatronics.ShouldCharge())
            {
                controller.StateMachine.TransitionTo(controller.StateMachine.chargeState);
            }
            else
            {
                controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
            }
        }
        else
        {
            time += Time.deltaTime;
        }
    }

    public void Exit()
    {
    }


}
