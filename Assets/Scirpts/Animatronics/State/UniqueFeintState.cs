using UnityEngine;

public class UniqueFeintState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;

    public UniqueFeintState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }

    public void Enter()
    {
        animatronics.SetUniqueFeintShader();
        animatronics.OnUniqueFeintFinished += FinishUniqueFeint;
    }

    public void Update()
    {
        animatronics.UpdateUniqueFeintShader();
    }

    public void Exit()
    {
        animatronics.OnUniqueFeintFinished -= FinishUniqueFeint;
    }

    private void FinishUniqueFeint()
    {
        controller.StateMachine.TransitionTo(controller.StateMachine.repositionState);
    }

}
