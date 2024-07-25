using UnityEngine;

public class SoundFeintState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public SoundFeintState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
        Debug.Log("SoundFeintState");
        controller.animatronics.PlaySoundFeint();
        controller.animatronics.OnSoundPlayFinished += OnSoundPlayFinished;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        animatronics.OnSoundPlayFinished -= OnSoundPlayFinished;
    }

    private void OnSoundPlayFinished()
    {
        Debug.Log($"OnSoundPlayFinished 실행");
        controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
    }

}