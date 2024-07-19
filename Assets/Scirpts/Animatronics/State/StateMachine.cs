using System;

[Serializable]
public class StateMachine
{
    public IState CurrentState {  get; private set; }

    public IdleState idleState;
    public ChargeState chargeState;
    public JumpScareState jumpScareState;
    public AttackSuccessState attackSuccessState;
    public AttackFailState attackFailState;
    public StopWorkState stopWorkState;
    public CircleMoveState circleMoveState;
    public FeintState feintState;
    public SoundFeintState soundFeintState;
    public InvisibleFeintState invisibleFeintState;
    public RepositionState repositionState;

    public StateMachine(AnimatronicsController animatronics)
    {
        this.idleState = new IdleState (animatronics);  
        this.chargeState = new ChargeState(animatronics);
        this.jumpScareState = new JumpScareState(animatronics);
        this.attackSuccessState = new AttackSuccessState(animatronics);
        this.attackFailState = new AttackFailState(animatronics);
        this.stopWorkState = new StopWorkState(animatronics);
        this.circleMoveState = new CircleMoveState(animatronics);
        this.feintState = new FeintState(animatronics);
        this.soundFeintState = new SoundFeintState(animatronics);
        this.invisibleFeintState = new InvisibleFeintState(animatronics);
        this.repositionState = new RepositionState(animatronics);
    }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.Update();
        }
    }
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }
}
