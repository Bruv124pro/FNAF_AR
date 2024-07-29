using System.Collections;
using System.Collections.Generic;
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
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }

}
