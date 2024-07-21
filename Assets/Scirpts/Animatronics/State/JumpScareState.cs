using UnityEngine;
public class JumpScareState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public JumpScareState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
        animatronics.PlayAnimation("FreddyCharge 1");
        controller.StateMachine.TransitionTo(controller.StateMachine.attackFailState);
        //controller.StateMachine.TransitionTo(controller.StateMachine.attackSuccessState);
    }

    public void Update()
    {

    }

    public void Exit()
    {
    }

}