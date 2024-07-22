using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicsController : MonoBehaviour
{
    public Animatronics animatronics;
    private StateMachine stateMachine;

    void Start()
    {
        stateMachine = new StateMachine(this);
        stateMachine.Initialize(stateMachine.idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    public StateMachine StateMachine => stateMachine;
}
