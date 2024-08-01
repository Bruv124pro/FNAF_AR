using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UI_SelectAnimatronics : UI_Popup
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject animatronics;
    [SerializeField] private GameObject InGameAnimatronics;
    private Camera camera;
    private Camera mapCamera;
    private TextMeshProUGUI idText;
    private int id;

    private string ARAnimatronicsPrefabPath = "Prefabs/ARAnimatronics/AR_";
    private string prefabPath = "Prefabs/PreViewAnimatronics/pre_";
    public Transform parentTransform;
    private GameObject preViewAnimatronics;
    private GameObject ARAnimatronics;
    ButtonID buttonID;
    AnimatronicsModel animatronicsTable;

    bool isDialog = false;

    private enum Buttons
    {
        FreddyFazbearButton,
        FreddyFrostbearButton,
        BalloonBoyButton
    }

    private enum GameObjects
    {
        ButtonCanvas,
        FreddyFazbear,
        FreddyFrostbear,
        BalloonBoy
    }

    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        idText = panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        buttonID = GetComponent<ButtonID>();

        camera = Camera.main;
        camera.gameObject.SetActive(false);
        mapCamera = Camera.allCameras[1];
        mapCamera.GetComponent<UniversalAdditionalCameraData>();

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.FreddyFazbearButton).gameObject.BindEvent(SelectAnimatronics);
        GetButton((int)Buttons.FreddyFrostbearButton).gameObject.BindEvent(SelectAnimatronics);
        GetButton((int)Buttons.BalloonBoyButton).gameObject.BindEvent(SelectAnimatronics);

        //GetObject((int)GameObjects.ButtonCanvas)

        return true;
    }

    public void SelectAnimatronics()
    {
        switch(buttonID.id)
        {
            case 1001:
                id = 1001;
                break;
            case 1002:
                id = 1002;
                break;
            case 1003:
                id = 1003;
                break;
        }

        animatronicsTable = DataManager.Instance.AnimatronicsTable[id];
        if (id != null)
        {
            idText.text = animatronicsTable.charName;
            GameObject prefab = Resources.Load<GameObject>(prefabPath + id);

            if (prefab != null)
            {
                preViewAnimatronics = Instantiate(prefab);

                if (parentTransform != null)
                {
                    preViewAnimatronics.transform.SetParent(parentTransform, false);
                }
            }

            if (isDialog)
            {
                Managers.UI.ShowPopupUI<UI_Dialog>().SetDialog(() => { UIChange(); }, $"{animatronicsTable.charName}", "EnCounter");
            }
        }
    }

    public void EnCounterARView()
    {
        GameObject prefab = Resources.Load<GameObject>(ARAnimatronicsPrefabPath + id);
        if (prefab != null)
        {
            ARAnimatronics = Instantiate(prefab);

            if (InGameAnimatronics.transform != null)
            {
                ARAnimatronics.transform.SetParent(InGameAnimatronics.transform, false);
            }
        }

        InGameAnimatronics.GetComponent<Animatronics>().GetId(id);
    }

    private void UIChange()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_InGamePopup>();
    }
}
