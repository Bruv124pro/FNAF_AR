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

    void Update()
    {
        stateMachine.Update();
        animatronics.OnOffGlitchMaterial();
    }

    public StateMachine StateMachine => stateMachine;
}
