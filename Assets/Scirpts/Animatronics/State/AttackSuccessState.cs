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
        Debug.Log("AttackSuccessState");
        animatronics.PlayAnimation("FreddyJumpscare_Intro");
        Handheld.Vibrate();
        animatronics.gameObject.SetActive(false);
        animatronics.JumpScareObject.SetActive(true);
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}