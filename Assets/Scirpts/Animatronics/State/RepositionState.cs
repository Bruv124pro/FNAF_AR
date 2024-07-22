using UnityEngine;

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
        controller.animatronics.RotateReposition();
        controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
    }

    public void Update()
    {
        // 위치를 바꾸는 코드
        controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
    }

    public void Exit()
    {
        //controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
    }
}