using UnityEngine;
public class StopWorkState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public StopWorkState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
        Debug.Log("StopWorkState");
        animatronics.PlayAnimation("Shutdown");
        animatronics.flashButton.interactable = false;
        animatronics.shockButton.interactable = false;
    }

    public void Update()
    {

    }

    public void Exit()
    {
    }
}