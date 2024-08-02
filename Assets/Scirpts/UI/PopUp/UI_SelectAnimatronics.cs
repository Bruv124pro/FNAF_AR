using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class UI_SelectAnimatronics : UI_Popup
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject animatronics;
    [SerializeField] private GameObject InGameAnimatronics;
    [SerializeField] private ButtonID[] buttonID;
    private Camera camera;
    private Camera mapCamera;
    private TextMeshProUGUI idText;
    private int id;

    private string ARAnimatronicsPrefabPath = "Prefabs/ARAnimatronics/AR_";
    private string prefabPath = "Prefabs/PreViewAnimatronics/pre_";
    public Transform parentTransform;
    private GameObject preViewAnimatronics;
    private GameObject ARAnimatronics;

    bool isDialog = false;

    private enum Buttons
    {
        FreddyFazbearButton,
        FreddyFrostbearButton,
        BalloonBoyButton
    }

    private enum GameObjects
    {
        SelectCanvas,
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

        camera = Camera.main;
        camera.gameObject.SetActive(false);
        mapCamera = Camera.allCameras[1];
        mapCamera.GetComponent<UniversalAdditionalCameraData>();

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.FreddyFazbearButton).gameObject.BindEvent(() => SelectAnimatronics(1001));
        GetButton((int)Buttons.FreddyFrostbearButton).gameObject.BindEvent(() => SelectAnimatronics(1002));
        GetButton((int)Buttons.BalloonBoyButton).gameObject.BindEvent(() => SelectAnimatronics(1003));
        GetObject((int)GameObjects.SelectCanvas).gameObject.SetActive(false);

        return true;
    }

    public void SelectAnimatronics(int buttonID)
    {
        panel.SetActive(true);

        ButtonID selectedButtonID = null;
        foreach (var button in this.buttonID)
        {
            if (button.id == buttonID)
            {
                selectedButtonID = button;
                Debug.Log($"button : {button}");
                break;
            }
        }
        Debug.Log($"selectedButtonID : {selectedButtonID.id}");
        if (selectedButtonID != null)
        {
            var animatronicsTable = DataManager.Instance.AnimatronicsTable[selectedButtonID.id];
            idText.text = animatronicsTable.charName;
            GameObject prefab = Resources.Load<GameObject>(prefabPath + selectedButtonID.id);
            id = selectedButtonID.id;

            Debug.Log($"{prefabPath + id}");

            if (prefab != null)
            {
                preViewAnimatronics = Instantiate(prefab);

                if (parentTransform != null)
                {
                    preViewAnimatronics.transform.SetParent(parentTransform, false);
                    isDialog = true;
                }

                if (isDialog)
                {
                    Managers.UI.ShowPopupUI<UI_Dialog>().SetDialog(() => { UIChange(); }, $"{idText}", "EnCounter");
                }
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
        camera.gameObject.SetActive(true);

        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_InGamePopup>();
    }
}
