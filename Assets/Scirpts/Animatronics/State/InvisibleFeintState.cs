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
        Debug.Log("InvisibleFeintState 진입");
        controller.animatronics.SetVisible();
        controller.animatronics.OnVisibleFinished += OnVisibleFinished;
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }

    private void OnVisibleFinished()
    {
        Debug.Log($"OnVisibleFinished 실행");
        controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
    }
}