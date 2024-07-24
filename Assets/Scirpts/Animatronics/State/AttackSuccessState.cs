using UnityEngine;
public class AttackSuccessState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public AttackSuccessState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
        animatronics.PlayAnimation("FreddyJumpscare_Intro");
        Handheld.Vibrate();
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}