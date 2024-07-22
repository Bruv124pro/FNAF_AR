using UnityEngine;

public class FeintState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;
    string state = "";

    public FeintState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
        state = animatronics.GoFeintToAnotherState();
        Debug.Log(state);
        IState nextState = controller.StateMachine.GetState(state);
        controller.StateMachine.TransitionTo(nextState);
    }

    public void Update()
    {
        Debug.Log("FeintUpdate");
        controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
    }

    public void Exit()
    {
        Debug.Log("FeintExit");
    }
}