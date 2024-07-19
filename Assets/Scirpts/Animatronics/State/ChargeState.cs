using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChargeState : IState
{
    private AnimatronicsController animatronics;

    public ChargeState(AnimatronicsController animatronics)
    {
        this.animatronics = animatronics;
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