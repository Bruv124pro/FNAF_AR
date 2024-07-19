using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicsController : MonoBehaviour
{
    private StateMachine sm;

    void Start()
    {
        sm = new StateMachine(this);

        sm.Initialize(sm.idleState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
