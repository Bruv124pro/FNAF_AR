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

    private string ARAnimatronicsPrefabPath = "Prefabs/ARAnimatronics/AR_";
    private string prefabPath = "Prefabs/PreViewAnimatronics/pre_";
    public Transform parentTransform;
    private GameObject preViewAnimatronics;
    private GameObject ARAnimatronics;

    [SerializeField] Material glitchMaterial;

    private void Start()
    {
        glitchMaterial.SetFloat("_Force", 0);
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
        panel.transform.parent.gameObject.SetActive(true);
        panel.SetActive(true);
        ButtonID buttonID = button.GetComponent<ButtonID>();
        
        if (buttonID != null)
        {
            var animatronicsTable = DataManager.Instance.AnimatronicsTable[buttonID.id];
            idText.text = animatronicsTable.charName;
            GameObject prefab = Resources.Load<GameObject>(prefabPath + buttonID.id);
            id = buttonID.id;

            Debug.Log($"{prefabPath + id}");

            if (prefab != null)
            {
                preViewAnimatronics = Instantiate(prefab);

                if (parentTransform != null)
                {
                    preViewAnimatronics.transform.SetParent(parentTransform, false);
                }
            }
        }
    }

    public void EnCounterARView()
    {
        var animatronicsTable = DataManager.Instance.AnimatronicsTable[id];

        panel.SetActive(false);
        panel.transform.parent.gameObject.SetActive(false);
        Destroy(preViewAnimatronics);
        //preViewAnimatronics.SetActive(false);
        uiChild.SetActive(true);
        mapCamera.gameObject.SetActive(false);
        InGameAnimatronics.SetActive(true);

        GameObject prefab = Resources.Load<GameObject>(ARAnimatronicsPrefabPath + id);
        Debug.Log($"{prefab}");
        if (prefab != null)
        {
            ARAnimatronics = Instantiate(prefab);

            if (InGameAnimatronics.transform != null)
            {
                ARAnimatronics.transform.SetParent(InGameAnimatronics.transform, false);
            }
        }
        InGameAnimatronics.GetComponent<Animatronics>().GetId(id);
        InGameAnimatronics.GetComponent<Animatronics>().ReStart();
    }

    public void OnJammerButtonCliecked()
    {
        panel.SetActive(false);

        Destroy(preViewAnimatronics);
    }
}
