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
        Debug.Log("MoveEnter");
    }

    public void Update()
    {
        Debug.Log("MoveUpdate");
        controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
    }

    public void Exit()
    {
        Debug.Log("MoveExit");
    }
}