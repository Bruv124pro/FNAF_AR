using System.Collections;
using UnityEngine;
public class JumpScareState : IState
{
    private Material bodyShader;
    private float alpha;

    private AnimatronicsController controller;
    private Animatronics animatronics;

    public JumpScareState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
        this.bodyShader = controller.bodyShader;
    }
    public void Enter()
    {
        animatronics.PlayAnimation("FreddyCharge 1");
        //controller.StateMachine.TransitionTo(controller.StateMachine.attackFailState);
        //controller.StateMachine.TransitionTo(controller.StateMachine.attackSuccessState);
    }

    public void Update()
    {
        alpha = bodyShader.GetFloat("_Alpha");
        animatronics.FloatSetting(alpha);
    }

    public void Exit()
    {
        controller.StateMachine.TransitionTo(controller.StateMachine.idleState);
    }
}