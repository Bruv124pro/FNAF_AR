using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics.HapticsUtility;

public class Animatronics : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private string charName;
    [SerializeField] private int minNoiseForce;
    [SerializeField] private int maxNoiseForce;
    [SerializeField] private int spawnDistance;
    [SerializeField] private int minInitialPauseSecond;
    [SerializeField] private int maxInitialPauseSecond;
    [SerializeField] private int hp;
    [SerializeField] private int jumpScareTime;
    [SerializeField] private int minshockTime;
    [SerializeField] private int maxshockTime;
    [SerializeField] private int minCircleDegreesPerSecond;
    [SerializeField] private int maxCircleDegreesPerSecond;
    [SerializeField] private int circleMoveTime;
    [SerializeField] private int minPauseSecond;
    [SerializeField] private int maxPauseSecond;
    [SerializeField] private int chanceToCharge;
    [SerializeField] private int chanceToJumpScare;
    [SerializeField] private int chanceToFeint;
    [SerializeField] private int chargeTime;
    [SerializeField] private int invisibleTime;
    [SerializeField] private int cloackedTime;
    [SerializeField] private int deCloackedTime;
    [SerializeField] private int minRepositionAngleDegrees;
    [SerializeField] private int maxRepositionAngleDegrees;

    private Animator animator;
    private bool isFinishCircleMove;
    private bool alreadyinit;

    public StateMachine StateMachine {  get; private set; }

    [SerializeField] private Material bodyShader;
    [SerializeField] private Material eyeShader;
    private float bodyAlpha;
    private float eyeAlpha;

    void Start()
    {
        animator = GetComponent<Animator>();
        AnimatronicsInit(id);

        bodyShader.SetFloat("_Alpha", 1);
        eyeShader.SetFloat("_Alpha", 0.5f);

        isFinishCircleMove = false;
        alreadyinit = false;
    }

    public bool ShouldCharge()
    {
        return Random.Range(0, 100) < chanceToCharge;
    }

    public bool ShouldJumpScare()
    {
        return Random.Range(0, 100) < chanceToJumpScare;
    }

    public bool HpCheck()
    {
        if(hp > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    public int WaitPauseSecond()
    {
        if (alreadyinit)
        {
            return Random.Range(minPauseSecond, maxPauseSecond);
        }
        else
        {
            alreadyinit = true;
            return Random.Range(minInitialPauseSecond, maxInitialPauseSecond);
        }
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
        Debug.Log($"animatronics.state : {state}");
        return state;
    }


    public void ShaderSetAlphaValue()
    {
        bodyAlpha = bodyShader.GetFloat("_Alpha");
        eyeAlpha = eyeShader.GetFloat("_Alpha");

        if (bodyAlpha > 0 || eyeAlpha > 0)
        {
            bodyAlpha -= 0.03f;
            eyeAlpha -= 0.03f;
            bodyShader.SetFloat("_Alpha", bodyAlpha);
            eyeShader.SetFloat("_Alpha", eyeAlpha);
        }
    }

    public void MoveCircle()
    {
        isFinishCircleMove = false;
        StartCoroutine(MoveCircleOneSecond());
    }

    IEnumerator MoveCircleOneSecond()
    {
        float elapsedTime = 0;
        int degree = RotateDegree(minCircleDegreesPerSecond, maxCircleDegreesPerSecond);

        while(elapsedTime < circleMoveTime)
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

    public int RotateDegree(int minDegrees, int maxDegrees)
    {
        return Random.Range(minDegrees, maxDegrees);
    }

    public void RotateReposition()
    {
        int degree = RotateDegree(minRepositionAngleDegrees, maxRepositionAngleDegrees);
        transform.RotateAround(Vector3.zero, Vector3.up, degree);
    }

}
