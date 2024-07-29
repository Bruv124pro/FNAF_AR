using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMap : MonoBehaviour
{
    private bool isDrag = false;
    private Vector2 startPos;
    private float initXRotation;

    public GameObject map;
    void Start()
    {
        initXRotation = transform.eulerAngles.x;
    }

    void Update()
    {
        if(Input.touchCount != 1)
        {
            isDrag = false;
            return;
        }
        Touch touch = Input.touches[0];

        if(touch.phase == TouchPhase.Began )
        {
            startPos = touch.position;
            isDrag = true;
        }
        else if(touch.phase == TouchPhase.Moved )
        {
            if(isDrag)
            {
                Vector2 deltaPos = touch.position - startPos;
                float rotationZ = deltaPos.x * 0.1f;

                Vector3 currentRotation = transform.eulerAngles;

                transform.eulerAngles = new Vector3(initXRotation, currentRotation.y, currentRotation.z - rotationZ);
                startPos = touch.position;
            }
        }
        else if( touch.phase == TouchPhase.Ended )
        {
            isDrag=false;
        }
    }
}
