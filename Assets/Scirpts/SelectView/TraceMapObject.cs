using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraceMapObject : MonoBehaviour
{
    [SerializeField] private GameObject traceButton;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = traceButton.GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.position = transform.position;
    }
}
