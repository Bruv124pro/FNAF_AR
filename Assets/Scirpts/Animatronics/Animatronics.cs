using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatronics : MonoBehaviour
{
    [SerializeField]private int id;
    [SerializeField]private string charName;
    [SerializeField]private int minNoiseForce;
    [SerializeField]private int maxNoiseForce;
    [SerializeField]private int spawnDistance;
    [SerializeField]private int minInitialPauseSecond;
    [SerializeField]private int maxInitialPauseSecond;
    [SerializeField]private int hp;
    [SerializeField]private int jumpScareTime;
    [SerializeField]private int minshockTime;
    [SerializeField]private int maxshockTime;
    [SerializeField]private int minCircleDegreesPerSecond;
    [SerializeField]private int maxCircleDegreesPerSecond;
    [SerializeField]private int circleMoveTime;
    [SerializeField]private int minPauseSecond;
    [SerializeField]private int maxPauseSecond;
    [SerializeField]private int chanceToCharge;
    [SerializeField]private int chanceToJumpScare;
    [SerializeField]private int chanceToFeint;
    [SerializeField]private int chargeTime;
    [SerializeField]private int invisibleTime;
    [SerializeField]private int cloackedTime;
    [SerializeField]private int deCloackedTime;
    [SerializeField]private int minRepositionAngleDegrees;
    [SerializeField]private int maxRepositionAngleDegrees;

    private Animator animator;
    private bool isFinishCircleMove;

    public StateMachine StateMachine {  get; private set; }


    void Start()
    {
        animator = GetComponent<Animator>();
        AnimatronicsInit(id);
        isFinishCircleMove = false;
    }

    public bool ShouldCharge()
    {
        return Random.Range(0, 100) < chanceToCharge;
    }

    public bool ShouldJumpScare()
    {
        return Random.Range(0, 100) < chanceToJumpScare;
    }


    public void AnimatronicsInit(int _id)
    {
        var animatronicsTable = DataManager.Instance.AnimatronicsTable[_id];
        charName = animatronicsTable.charName;
        minNoiseForce = animatronicsTable.minNoiseForce;
        maxNoiseForce = animatronicsTable.maxNoiseForce;
        spawnDistance = animatronicsTable.spawnDistance;
        minInitialPauseSecond = animatronicsTable.minInitialPauseSecond;
        maxInitialPauseSecond = animatronicsTable.maxInitialPauseSecond;
        hp = animatronicsTable.hp;
        jumpScareTime = animatronicsTable.jumpScareTime;
        minshockTime = animatronicsTable.minShockTime;
        maxshockTime = animatronicsTable.maxShockTime;
        minCircleDegreesPerSecond = animatronicsTable.minCircleDegreesPerSecond;
        maxCircleDegreesPerSecond = animatronicsTable.maxCircleDegreesPerSecond;
        circleMoveTime = animatronicsTable.circleMoveTime;
        minPauseSecond = animatronicsTable.minPauseSecond;
        maxPauseSecond = animatronicsTable.maxPauseSecond;
        chanceToCharge = animatronicsTable.chanceToCharge;
        chanceToJumpScare = animatronicsTable.chanceToJumpScare;
        chanceToFeint = animatronicsTable.chanceToFeint;
        chargeTime = animatronicsTable.chargeTime;
        invisibleTime = animatronicsTable.invisibleTime;
        cloackedTime = animatronicsTable.cloackedTime;
        deCloackedTime = animatronicsTable.deCloackedTime;
        minRepositionAngleDegrees = animatronicsTable.minRepositionAngleDegrees;
        maxRepositionAngleDegrees = animatronicsTable.maxRepositionAngleDegrees;
    }

    public void PlayAnimation(string animationName)
    {
        if(animator != null)
        {
            animator.Play(animationName);
        }
    }

    public int WaitInitialPauseSecond()
    {
        return Random.Range(minInitialPauseSecond, maxInitialPauseSecond);
    }

    public string GoIdleToAnotherState()
    {
        int ran = Random.Range(0, 100);
        string state = "";

        if(ran < chanceToCharge)
        {
            state = "chargeState";
        }
        else if(ran < chanceToCharge + chanceToFeint)
        {
            state = "feintState";
        }
        else if(ran >= chanceToCharge + chanceToFeint)
        {
            state = "circleMoveState";
        }

        return state;
    }

    public void MoveCircle()
    {
        isFinishCircleMove = false;
        StartCoroutine(MoveCircleOneSecond());
    }

    IEnumerator MoveCircleOneSecond()
    {
        float elapsedTime = 0;
        int degree = RotateDegree();

        while(elapsedTime < 15)
        {
            transform.RotateAround(Vector3.zero , Vector3.up, degree);
            yield return new WaitForSeconds(1f);
            elapsedTime++;
        }
        isFinishCircleMove = true;

    }

    public bool IsFinishCircleMove()
    {
        if (isFinishCircleMove)
        {
            isFinishCircleMove = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int RotateDegree()
    {
        return Random.Range(minCircleDegreesPerSecond, maxCircleDegreesPerSecond);

    }


}
