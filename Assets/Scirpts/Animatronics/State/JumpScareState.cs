using System.Collections;
using UnityEngine;
public class JumpScareState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;

    private float MinShockTime;
    private float MaxShockTime;
    private float elapsedTime;

    public JumpScareState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }

    public void Enter()
    {
        Debug.Log($"JumpScareState");
        Debug.Log($"화면에 비치는지{animatronics.IsVisibleInMonitor()}");
        MinShockTime = 0;
        Handheld.Vibrate();
        MaxShockTime = animatronics.InitmaxShockTime();
        animatronics.PlayAnimation("FreddyCharge 1");
        elapsedTime = 0;
        animatronics.ChangeGlitchBoolValue(false);
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;

        if (MinShockTime < elapsedTime)
        {
            animatronics.isJumpState = true;
            if (animatronics.isHitElectronic)
            {
                Debug.Log($"전기충격 받음 {animatronics.IsVisibleInMonitor()}");
                controller.StateMachine.TransitionTo(controller.StateMachine.attackFailState);
            }
        }

        if (elapsedTime > MaxShockTime)
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.attackSuccessState);
        }

    }

    public void Exit()
    {
        animatronics.OffGlitchMaterial();
        animatronics.isJumpState = false;
        animatronics.isHitElectronic = false;
        elapsedTime = 0;
    }
}