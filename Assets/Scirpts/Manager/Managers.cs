using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class Managers : MonoBehaviour
{
    public static Managers s_instance = null;
    public static Managers Instance { get { return s_instance; } }

    private static UIManager s_uiManager = new UIManager();
    private static ResourceManager s_resourceManager = new ResourceManager();

    public static UIManager UI { get { Init(); return s_uiManager; } }
    public static ResourceManager Resource { get { Init(); return s_resourceManager; } }

    //public static string GetText(int id)
    //{
    //    if (Managers.Data.Texts.TryGetValue(id, out TextData value) == false)
    //        return "";

    //    return value.kor.Replace("{userName}", Managers.Game.Name);
    //}

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject { name = "@Managers" };

            s_instance = Utils.GetOrAddComponent<Managers>(go);
            DontDestroyOnLoad(go);

            s_resourceManager.Init();

            Application.targetFrameRate = 60;
        }
    }
}
