using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SelectUIManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject animatronics;
    [SerializeField] private GameObject InGameAnimatronics;
    [SerializeField] private GameObject ButtonSpawner;
    [SerializeField] private AudioClip[] soundClips;
    private AudioClip backgroundAudioClip;
    private AudioClip SelectAudioClip;
    private Camera camera;
    private Camera mapCamera;
    private GameObject ui;
    private GameObject uiChild;
    private TextMeshProUGUI idText;
    private int id;
    public float currentDepth;
    private UniversalAdditionalCameraData cameraData;

    private AudioSource audioSource;

    private string ARAnimatronicsPrefabPath = "Prefabs/ARAnimatronics/AR_";
    private string prefabPath = "Prefabs/PreViewAnimatronics/pre_";
    public Transform parentTransform;
    private GameObject preViewAnimatronics;
    private GameObject ARAnimatronics;

    [SerializeField] Material glitchMaterial;
    private Vignette vignette;
    [SerializeField] private Volume volume;

    [SerializeField] Material tagGlitchMaterial;
    private bool isMapView;

    private void Start()
    {
        glitchMaterial.SetFloat("_Force", 0);
        tagGlitchMaterial.SetInt("_On_Off", 0);
        panel.SetActive(false);
        ui = GameObject.Find("UI");
        uiChild = ui.transform.GetChild(0).gameObject;
        idText = panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        camera = Camera.main;
        mapCamera = Camera.allCameras[1];
        cameraData = camera.GetComponent<UniversalAdditionalCameraData>();
        mapCamera.GetComponent<UniversalAdditionalCameraData>();
        isMapView = true;

        soundClips = Resources.LoadAll<AudioClip>("Sound/background music");
        SetAudioClip(soundClips);
        audioSource = GetComponent<AudioSource>();
        PlayBackGroundSound(backgroundAudioClip);
    }

    private void PlayBackGroundSound(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SelectAnimatronics(Button button)
    {
        PlayBackGroundSound(SelectAudioClip);
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
                isMapView = false;

                if (parentTransform != null)
                {
                    preViewAnimatronics.transform.SetParent(parentTransform, false);
                }
            }
            Destroy(button.gameObject);
            ButtonSpawner.GetComponent<ButtonSpawner>().AddSpawnButton();
        }
    }

    public void EnCounterARView()
    {
        var animatronicsTable = DataManager.Instance.AnimatronicsTable[id];

        panel.SetActive(false);
        panel.transform.parent.gameObject.SetActive(false);
        Destroy(preViewAnimatronics);
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
        PlayBackGroundSound(backgroundAudioClip);
        Destroy(preViewAnimatronics);
    }
    private void SetAudioClip(AudioClip[] audioClips)
    {
        SelectAudioClip = audioClips[0];
        backgroundAudioClip = audioClips[1];
    }
}
