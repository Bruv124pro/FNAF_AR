using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatronics : MonoBehaviour
{
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

    public void Initialize(int id)
    {
        var AnimatronicsData = DataManager.instance.GetAnimatronicsData(id);
        charName = AnimatronicsData.charName;
        minNoiseForce = AnimatronicsData.minNoiseForce;
        maxNoiseForce = AnimatronicsData.maxNoiseForce;
        spawnDistance = AnimatronicsData.spawnDistance;
        minInitialPauseSecond = AnimatronicsData.minInitialPauseSecond;
        maxInitialPauseSecond = AnimatronicsData.maxInitialPauseSecond;
        hp = AnimatronicsData.hp;
        jumpScareTime = AnimatronicsData.jumpScareTime;
        minshockTime = AnimatronicsData.minshockTime;
        maxshockTime = AnimatronicsData.maxshockTime;
        minCircleDegreesPerSecond = AnimatronicsData.minCircleDegreesPerSecond;
        maxCircleDegreesPerSecond = AnimatronicsData.maxCircleDegreesPerSecond;
        circleMoveTime = AnimatronicsData.circleMoveTime;
        minPauseSecond = AnimatronicsData.minPauseSecond;
        maxPauseSecond = AnimatronicsData.maxPauseSecond;
        chanceToCharge = AnimatronicsData.chanceToCharge;
        chanceToJumpScare = AnimatronicsData.chanceToJumpScare;
        chanceToFeint = AnimatronicsData.chanceToFeint;
        chargeTime = AnimatronicsData.chargeTime;
        invisibleTime = AnimatronicsData.invisibleTime;
        cloackedTime = AnimatronicsData.cloackedTime;
        deCloackedTime = AnimatronicsData.deCloackedTime;
        minRepositionAngleDegrees = AnimatronicsData.minRepositionAngleDegrees;
        maxRepositionAngleDegrees = AnimatronicsData.maxRepositionAngleDegrees;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
