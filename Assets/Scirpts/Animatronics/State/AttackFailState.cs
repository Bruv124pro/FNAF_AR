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
    }

    public void Update()
    {
        if (animatronics.HpCheck())
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
        }
        else
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.stopWorkState);
        }
    }

    public void Exit()
    {
    }

}