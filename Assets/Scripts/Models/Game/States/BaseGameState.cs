using Model.FSM;

public abstract class BaseGameState : BaseState<GameState>
{
    protected float Time { get; private set; }

    public BaseGameState(GameState name) : base(name)
    {
    }

    public virtual void UpdateTime(float time)
    {
        Time = time;
    }
}