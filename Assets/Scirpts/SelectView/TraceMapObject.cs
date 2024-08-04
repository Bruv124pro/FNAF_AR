using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraceMapObject : MonoBehaviour
{
    [SerializeField] public GameObject traceButton;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = traceButton.GetComponent<RectTransform>();
        StartCoroutine(MoveAnimatronicsButton());
    }

    void Update()
    {
        if (traceButton != null)
        {
            rectTransform.position = transform.position + Vector3.back;
            
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    IEnumerator MoveAnimatronicsButton()
    {
        while (true)
        {
            float ranX = Random.Range(-2f, 2f);
            float ranY = Random.Range(-2f, 2f);
            transform.position = new Vector3(transform.position.x + ranX, transform.position.y + ranY, transform.position.z);
            yield return new WaitForSeconds(4);
        }
    }
}
