using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SelectUIManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject animatronics;
    [SerializeField] private GameObject InGameAnimatronics;
    private Camera camera;
    private Camera mapCamera;
    private GameObject ui;
    private GameObject uiChild;
    private TextMeshProUGUI idText;
    private int id;
    public float currentDepth;
    private UniversalAdditionalCameraData cameraData;

    private void Start()
    {
        panel.SetActive(false);
        ui = GameObject.Find("UI");
        uiChild = ui.transform.GetChild(0).gameObject;
        idText = panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        camera = Camera.main;
        mapCamera = Camera.allCameras[1];
        cameraData = camera.GetComponent<UniversalAdditionalCameraData>();
        mapCamera.GetComponent<UniversalAdditionalCameraData>();
    }
    public void SelectAnimatronics(Button button)
    {
        panel.SetActive(true);
        animatronics.SetActive(true);
        ButtonID buttonID = button.GetComponent<ButtonID>();
        
        if (buttonID != null)
        {
            var animatronicsTable = DataManager.Instance.AnimatronicsTable[buttonID.id];
            idText.text = animatronicsTable.charName;
        }
    }

    public void EnCounterARView()
    {
        panel.SetActive(false);
        panel.transform.parent.gameObject.SetActive(false);
        animatronics.SetActive(false);
        uiChild.SetActive(true);
        mapCamera.gameObject.SetActive(false);
        InGameAnimatronics.SetActive(true);
        InGameAnimatronics.GetComponent<Animatronics>().GetId(id);
    }
}
