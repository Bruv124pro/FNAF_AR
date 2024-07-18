using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Compilation;
using UnityEngine;
using static DataManager;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private const string DATA_PATH = "Data";
    private const string ANIMATRONICS_JSON = "Animatronic";

    public Dictionary<int , AnimatronicsModel> AnimatronicsTable;

    private void Awake()
    {   
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }


    private void Init()
    {
        var json = Resources.Load<TextAsset>(Path.Combine(DATA_PATH, ANIMATRONICS_JSON));
        GameData gameData = JsonUtility.FromJson<GameData>(json.text);

        AnimatronicsTable = gameData.animatronics.ToDictionary(datum => datum.id);
    }
}
