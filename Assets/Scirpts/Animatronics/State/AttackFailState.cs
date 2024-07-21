using UnityEngine;
public class AttackFailState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public AttackFailState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
        animatronics.PlayAnimation("FreddyShocked");
        controller.StateMachine.TransitionTo(controller.StateMachine.stopWorkState);
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }

}