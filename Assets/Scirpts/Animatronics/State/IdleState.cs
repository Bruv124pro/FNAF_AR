using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;
    public float pauseSecond = 0;
    float time = 0;
    string state = "";
    public IdleState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }

    public void Enter()
    {
        animatronics.ChangeGlitchBoolValue(true);
        animatronics.PlayAnimation("Idle");
        pauseSecond = animatronics.WaitPauseSecond();
        animatronics.PlayAnimation("FreddyJumpscareFinalPose");
    }

    public void Update()
    {
        if (time > pauseSecond)
        {
            time = 0;
            state = animatronics.GoIdleToAnotherState();
            Debug.Log($"state´Â {state}");
            IState nextState = controller.StateMachine.GetState(state);
            //controller.StateMachine.TransitionTo(nextState);
            controller.StateMachine.TransitionTo(controller.StateMachine.chargeState);
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
