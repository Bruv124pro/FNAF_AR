using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.VFX;

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
    [SerializeField] private int chanceToUniqueFeint;
    [SerializeField] private int chargeTime;
    [SerializeField] private int invisibleTime;
    [SerializeField] private int cloackedTime;
    [SerializeField] private int deCloackedTime;
    [SerializeField] private int minRepositionAngleDegrees;
    [SerializeField] private int maxRepositionAngleDegrees;

    private Animator animator;
    private bool isFinishCircleMove;
    private bool useGlitch;
    private bool alreadyinit;
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    [SerializeField] public VisualEffect[] hitParticles;

    [SerializeField] public VisualEffect[] missParticle;
    [SerializeField] public VisualEffect[] succParticle;

    public Camera camera;

    public Material glitchMaterial;

    public event Action OnSoundPlayFinished;
    public event Action OnVisibleFinished;
    public event Action ShockButtonPressed;
    public event Action ChargeToJumpScare;

    public string[] visibleAnimationNames = { "Glimpse1", "Glimpse2", "Glimpse3" };

    public StateMachine StateMachine { get; private set; }

    [SerializeField] private Material bodyShader;
    [SerializeField] private Material eyeShader;
    private float bodyAlpha;
    private float eyeAlpha;

    public bool isJumpState;
    public bool isHitElectronic;

    public GameObject JumpScareObject;

    public Button flashButton;
    public Button shockButton;

    public Canvas gameResultCanvas;
    public Text gameResultText;
    
    private void Awake()
    {
        if (id == null)
        {
            id = 1004;
        }
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        AnimatronicsInit(id);
    }

    void Start()
    {
        ShaderAlpahValueInitalize();

        isFinishCircleMove = false;
        alreadyinit = false;
        isJumpState = false;
        isHitElectronic = false;
        useGlitch = true;

        foreach (var effect in hitParticles)
        {
            effect.transform.GetComponent<VisualEffect>();
        }

        foreach (var effect in missParticle)
        {
            effect.transform.GetComponent<VisualEffect>();
        }
        foreach (var effect in succParticle)
        {
            effect.transform.GetComponent<VisualEffect>();
        }

        HitParticleOff();
        ElecEffectOff(true);
        ElecEffectOff(false);
    }
    public void SetVisible()
    {
        bodyShader.SetFloat("_Alpha", 0);
        eyeShader.SetFloat("_Alpha", 0f);
        eyeShader.SetInt("_IsVisible", 1);
        StartCoroutine(SetInvisible());
    }

    IEnumerator SetInvisible()
    {
        yield return new WaitForSeconds(invisibleTime);
        ShaderAlpahValueInitalize();
        OnVisibleFinished?.Invoke();
    }

    public void ShockPress()
    {
        StartCoroutine(ShockPressCheck());
    }

    IEnumerator ShockPressCheck()
    {
        yield return new WaitForSeconds(minshockTime);
        ShockButtonPressed?.Invoke();
    }
    public bool ShouldCharge()
    {
        return UnityEngine.Random.Range(0, 100) < chanceToCharge;
    }

    public bool ShouldJumpScare()
    {
        return UnityEngine.Random.Range(0, 100) < chanceToJumpScare;
    }

    public void ChangeChargeToJumpScare()
    {
        StartCoroutine(WaitForChange());
    }

    IEnumerator WaitForChange()
    {
        yield return new WaitForSeconds(chargeTime);

        ChargeToJumpScare?.Invoke();
    }

    public bool HpCheck()
    {
        if (hp > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HpDecrease()
    {
        if (hp > 0)
        {
            hp--;
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
        chanceToUniqueFeint = animatronicsTable.chanceToUniqueFeint;
        chargeTime = animatronicsTable.chargeTime;
        invisibleTime = animatronicsTable.invisibleTime;
        cloackedTime = animatronicsTable.cloackedTime;
        deCloackedTime = animatronicsTable.deCloackedTime;
        minRepositionAngleDegrees = animatronicsTable.minRepositionAngleDegrees;
        maxRepositionAngleDegrees = animatronicsTable.maxRepositionAngleDegrees;
    }

    public void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.Play(animationName);
        }
    }

    public int WaitPauseSecond()
    {
        if (alreadyinit)
        {
            return UnityEngine.Random.Range(minPauseSecond, maxPauseSecond);
        }
        else
        {
            alreadyinit = true;
            return UnityEngine.Random.Range(minInitialPauseSecond, maxInitialPauseSecond);
        }
    }

    public string GoIdleToAnotherState()
    {
        int ran = UnityEngine.Random.Range(0, 100);
        string state = "";

        if (ran < chanceToCharge)
        {
            state = "chargeState";
        }
        else if (ran < chanceToCharge + chanceToFeint)
        {
            state = "feintState";
        }
        else if (ran >= chanceToCharge + chanceToFeint)
        {
            state = "circleMoveState";
        }
        return state;
    }

    public string GoFeintToAnotherState()
    {
        int ran = UnityEngine.Random.Range(0, 100);
        string state = "";


        if (ran <= chanceToUniqueFeint)
        {
            state = "uniqueFeintState";
        }
        else if (ran > chanceToUniqueFeint && ran <= 60)
        {
            state = "soundFeintState";
        }
        else if (ran > 60)
        {
            state = "invisibleFeintState";
        }

        return state;
    }

    public void ShaderSetAlphaValue()
    {
        bodyAlpha = bodyShader.GetFloat("_Alpha");
        eyeAlpha = eyeShader.GetFloat("_Alpha");
        eyeShader.SetInt("_IsVisible", 1);
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

        while (elapsedTime < circleMoveTime)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, degree);
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
        return UnityEngine.Random.Range(minDegrees, maxDegrees);

    }

    public void RotateReposition()
    {
        int degree = RotateDegree(minRepositionAngleDegrees, maxRepositionAngleDegrees);
        transform.RotateAround(Vector3.zero, Vector3.up, degree);
    }

    public void PlaySoundFeint()
    {
        int ran = UnityEngine.Random.Range(0, audioClips.Length);
        AudioClip clip = audioClips[ran];
        StartCoroutine(CheckSoundPlayFinished(clip.length));
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    IEnumerator CheckSoundPlayFinished(float length)
    {
        yield return new WaitForSeconds(length);
        OnSoundPlayFinished?.Invoke();
    }

    public bool IsFindVisibleAnimatronics()
    {
        bool isVisible = IsVisibleInMonitor();

        return isVisible;
    }

    public bool IsVisibleInMonitor()
    {
        Vector3 viewportPoint = camera.WorldToViewportPoint(transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
    }

    public string selectVisibleAnimation()
    {
        int ran = UnityEngine.Random.Range(0, visibleAnimationNames.Length);
        string visibleanimName = visibleAnimationNames[ran];

        return visibleanimName;
    }

    public int ChargeTimeCheck()
    {
        return chargeTime;
    }

    public void OnOffGlitchMaterial()
    {
        if (IsFindVisibleAnimatronics())
        {
            Debug.Log($"±Û¸®Ä¡ {useGlitch}");
            if (useGlitch)
            {
                glitchMaterial.SetFloat("_Force", 3);
            }
        }
        else
        {
            glitchMaterial.SetFloat("_Force", 0);
        }
    }
    
    public void OffGlitchMaterial()
    {
        glitchMaterial.SetFloat("_Force", 0);
    }

    public bool IsGlitchUse()
    {
        return useGlitch;
    }

    public void ChangeGlitchBoolValue(bool _useGlitch)
    {
        useGlitch = _useGlitch;
    }

    public void ShaderAlpahValueInitalize()
    {
        bodyShader.SetFloat("_Alpha", 1f);
        eyeShader.SetFloat("_Alpha", 1f);
        eyeShader.SetInt("_IsVisible", 0);
    }

    public void HitElecParticle(bool isSuccAtack)
    {
        ElecEffectOn(isSuccAtack);
        StartCoroutine(HitElecParticleFinish(isSuccAtack));
    }

    IEnumerator HitElecParticleFinish(bool isSuccAtack)
    {
        yield return new WaitForSeconds(1f);
        ElecEffectOff(isSuccAtack);
        isJumpState = false;
    }

    public float InitmaxShockTime()
    {
        return (float)maxshockTime / 10;
    }

    private void ElecEffectOn(bool isSuccAtack)
    {
        if (isSuccAtack)
        {
            foreach (VisualEffect p in succParticle)
            {
                p.Play();
            }
        }
        else
        {
            foreach (VisualEffect p in missParticle)
            {
                p.Play();
            }
        }

    }

    private void ElecEffectOff(bool isSuccAtack)
    {
        if (isSuccAtack)
        {
            foreach (VisualEffect p in succParticle)
            {
                p.Stop();
            }
        }
        else
        {
            foreach (VisualEffect p in missParticle)
            {
                p.Stop();
            }
        }
    }
    
    private void HitParticleOn()
    {
        foreach (VisualEffect p in hitParticles)
        {
            p.Play();
        }
    }

    IEnumerator HitParticleOnToOff()
    {
        yield return new WaitForSeconds(1f);
        foreach (VisualEffect p in hitParticles)
        {
            p.Stop();
        }
    }

    private void HitParticleOff()
    {
        foreach (VisualEffect p in hitParticles)
        {
            p.Stop();
        }
    }
    
    public void HitAnimatronicsBodyParticle()
    {
        HitParticleOn();
        StartCoroutine(HitParticleOnToOff());
    }

    public void GetId(int _id)
    {
        id = _id;
    }

    public string SetName()
    {
        return charName;
    }

    public void GameOverOverlay()
    {
        StartCoroutine(GameOverPanelSetActive());
    }
    
    public void GameClearOverlay()
    {
        StartCoroutine(GameClearPanelSetActive());
    }

    IEnumerator GameOverPanelSetActive()
    {
        yield return new WaitForSeconds(3f);

        gameResultCanvas.gameObject.SetActive(true);
        gameResultText.text = "YOU LOST";
        gameResultText.color = Color.red;
    }
    
    IEnumerator GameClearPanelSetActive()
    {
        yield return new WaitForSeconds(3f);

        gameResultCanvas.gameObject.SetActive(true);
        gameResultText.text = "YOU WIN";
        gameResultText.color = Color.white;
    }
}
