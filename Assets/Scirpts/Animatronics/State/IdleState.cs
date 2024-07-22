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
        Debug.Log("idleEnter");
        pauseSecond = animatronics.WaitInitialPauseSecond();
    }

    public void Update()
    {
        Debug.Log("idleUpdate");
        if (time > pauseSecond)
        {
            state = animatronics.GoIdleToAnotherState();
            IState nextState = controller.StateMachine.GetState(state);
            controller.StateMachine.TransitionTo(nextState);
        }
        else
        {
            time += Time.deltaTime;
        }
        Debug.Log($"state : {state}");
    }

    public void Exit()
    {
        Debug.Log("idleExit");
    }


}
