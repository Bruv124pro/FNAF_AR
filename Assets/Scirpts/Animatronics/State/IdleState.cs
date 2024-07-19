using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private AnimatronicsController animatronics;

    private bool isCharge =false;

    public IdleState(AnimatronicsController animatronics) { this.animatronics = animatronics; }

    public void Enter()
    {
        isCharge = true;
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }


}
