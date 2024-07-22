using UnityEngine;
public class InvisibleFeintState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public InvisibleFeintState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
        animatronics.PlayAnimation(controller.animatronics.selectVisibleAnimation());
        controller.animatronics.SetVisible();
        controller.animatronics.OnVisibleFinished += OnVisibleFinished;
    }

    public void Update()
    {
        if (controller.animatronics.IsFindVisibleAnimatronics())
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
        }
    }

    public void Exit()
    {

    }

    private void OnVisibleFinished()
    {
        controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
    }
}