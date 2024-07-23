using System;

[Serializable]

public enum StateType
{
    IdleState,
    ChargeState,
    JumpScareState,
    AttackSuccessState,
    AttackFailureState,
    StopWorkState,
    CircleMoveState,
    FeintState,
    SoundFeintState,
    InvisibleFeintState,
    Reposition
}

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

    public StateMachine(AnimatronicsController controller)
    {
        Animatronics animatronics = controller.animatronics;
        this.idleState = new IdleState (controller);
        this.chargeState = new ChargeState(controller);
        this.jumpScareState = new JumpScareState(controller);
        this.attackSuccessState = new AttackSuccessState(controller);
        this.attackFailState = new AttackFailState(controller);
        this.stopWorkState = new StopWorkState(controller);
        this.circleMoveState = new CircleMoveState(controller);
        this.feintState = new FeintState(controller);
        this.soundFeintState = new SoundFeintState(controller);
        this.invisibleFeintState = new InvisibleFeintState(controller);
        this.repositionState = new RepositionState(controller);
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

    public void Exit()
    {
        CurrentState?.Exit();
    }

    public IState GetState(string stateName)
    {
        return stateName switch
        {
            "idleState" => idleState,
            "chargeState" => chargeState,
            "jumpScareState" => jumpScareState,
            "attackSuccessState" => attackSuccessState,
            "attackFailState" => attackFailState,
            "stopWorkState" => stopWorkState,
            "circleMoveState" => circleMoveState,
            "feintState" => feintState,
            "soundFeintState" => soundFeintState,
            "invisibleFeintState" => invisibleFeintState,
            "repositionState" => repositionState
        };
    }
}
