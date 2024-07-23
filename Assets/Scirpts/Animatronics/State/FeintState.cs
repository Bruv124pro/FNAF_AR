using UnityEngine;

public class FeintState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public FeintState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
    }

    public void Update()
    {
        controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
    }

    public void Exit()
    {
    }
}