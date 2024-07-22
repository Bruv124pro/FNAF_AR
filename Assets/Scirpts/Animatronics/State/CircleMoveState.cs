using UnityEngine;
public class CircleMoveState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public CircleMoveState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}