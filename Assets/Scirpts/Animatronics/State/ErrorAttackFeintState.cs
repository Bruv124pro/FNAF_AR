using UnityEngine;

public class ErrorAttackFeintState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;
    private float elapsedTime;

    public ErrorAttackFeintState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }

    public void Enter()
    {
        elapsedTime = 0;
        animatronics.PlayAnimation("Haywire");
        animatronics.SetVisible();
        animatronics.PlaySound(animatronics.hayWireAudioClip);
        animatronics.OnVisibleFinished += OnErrorAttackFinished;
    }

    public void Update()
    {
        if (animatronics.IsVisibleInMonitor())
        {
            elapsedTime += Time.deltaTime;
        }

        if(elapsedTime > 1f)
        {
            controller.StateMachine.TransitionTo(controller.StateMachine.attackSuccessState);
        }
    }

    public void Exit()
    {
        elapsedTime = 0f;
        animatronics.OnVisibleFinished -= OnErrorAttackFinished;
    }

    public void OnErrorAttackFinished()
    {
        controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
    }


}
