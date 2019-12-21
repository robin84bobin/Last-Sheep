using Model.FSM;

public abstract class BaseGameState : BaseState<GameState>
{

    public abstract void Update();
    
    public BaseGameState(GameState name) : base(name)
    {
    }
}