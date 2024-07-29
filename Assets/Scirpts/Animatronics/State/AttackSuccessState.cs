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
        animatronics.JumpScareObject.SetActive(true);

        animatronics.flashButton.interactable = false;
        animatronics.shockButton.interactable = false;

        animatronics.GameOverOverlay();
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}