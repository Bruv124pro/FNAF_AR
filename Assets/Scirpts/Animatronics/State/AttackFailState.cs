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
        Debug.Log("AttackFailState");
        animatronics.PlayAnimation("FreddyShocked");
        animatronics.HpDecrease();
        animatronics.HitAnimatronicsBodyParticle();
        if (animatronics.HpCheck())
        {
            animatronics.ChangeGlitchBoolValue(true);
            controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
        }
        else
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.stopWorkState);
        }

        animatronics.GameClearOverlay();
    }

    public void Update()
    {

    }

    public void Exit()
    {
    }

}