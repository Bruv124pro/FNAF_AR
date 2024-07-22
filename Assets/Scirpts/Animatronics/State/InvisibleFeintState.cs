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
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}