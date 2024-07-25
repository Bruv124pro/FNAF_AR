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
        animatronics.PlayAnimation("FreddyShutdown");
    }

    public void Update()
    {

    }

    public void Exit()
    {
    }
}