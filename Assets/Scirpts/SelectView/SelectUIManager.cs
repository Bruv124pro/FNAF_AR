using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectUIManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject animatronics;

    private TextMeshProUGUI idText;
    private int id;

    private void Start()
    {
        panel.SetActive(false);
        
        idText = panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void SelectAnimatronics(Button button)
    {
        panel.SetActive(true);
        
        ButtonID buttonID = button.GetComponent<ButtonID>();
        Debug.Log(idText.text);
        Debug.Log($"버튼 데이터 들어가는지 확인 {buttonID} {buttonID.id}");
        id = buttonID.id;
        if(buttonID != null)
        {
            idText.text = "Id test" + buttonID.id;
        }
    }

    public void EnCounterARView()
    {
        panel.SetActive(false);
        panel.transform.parent.gameObject.SetActive(false);
        animatronics.SetActive(true);

        animatronics.GetComponent<Animatronics>().GetId(id);

    }
}
