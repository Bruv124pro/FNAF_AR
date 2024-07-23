using UnityEngine;
public class CircleMoveState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public CircleMoveState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    
    public void Enter()
    {
        controller.animatronics.MoveCircle();
    }

    public void Update()
    {
        if (controller.animatronics.IsFinishCircleMove())
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
        }
    }

    public void Exit()
    {
    }
}