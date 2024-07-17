using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }

    private const string DATA_PATH = "Data";
    private const string ANIMATRONICS_JSON = "Animatronic";

    public Dictionary<int , Animatronics> _animatronics = new Dictionary<int , Animatronics>();

    public class Animatronics
    {
        public int id;
        public string charName;
        public float minNoiseForce;
        public float maxNoiseForce;
        public float spawnDistance;
        public int minInitialPauseSecond;
        public int maxInitialPauseSecond;
        public int hp;
        public float jumpScareTime;
        public float minshockTime;
        public float maxshockTime;
        public int minCircleDegreesPerSecond;
        public int maxCircleDegreesPerSecond;
        public int circleMoveTime;
        public int minPauseSecond;
        public int maxPauseSecond;
        public int chanceToCharge;
        public int chanceToJumpScare;
        public int chanceToFeint;
        public int chargeTime;
        public int invisibleTime;
        public int cloackedTime;
        public int deCloackedTime;
        public int minRepositionAngleDegrees;
        public int maxRepositionAngleDegrees;
    }

    public struct AnimatronicsData
    {
        public Animatronics[] animatronics;
    }

    private void InitAnimatronicsData()
    {
        TextAsset animatronicsJson = Resources.Load<TextAsset>(Path.Combine(DATA_PATH, ANIMATRONICS_JSON));
        AnimatronicsData animatronicsList = JsonUtility.FromJson<AnimatronicsData>(animatronicsJson.text);

        foreach(var data in animatronicsList.animatronics)
        {
            var animatronics = data as Animatronics;
            _animatronics.Add(data.id, data);
            Debug.Log($"{data.id}, {data}");
        }
    }

    private void Awake()
    {   
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitAnimatronicsData();
        GetAnimatronicsData(1001);
    }

    public Animatronics GetAnimatronicsData(int id)
    {
        return _animatronics[id];
    }

}
