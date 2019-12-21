using Controllers;
using Model;
using Model.FSM;

public enum GameState
{
    Up,
    Down
}



public class GameModel : BaseModel
{
    private FSM<GameState, BaseGameState> fsm;
    public IStateMachine<GameState> Fsm => fsm;
    
    private readonly GameConfig _config;
    
    public PlatformModel platform { get; private set; }
    
    public GameModel(GameConfig config)
    {
        _config = config;
        
        fsm = new FSM<GameState, BaseGameState>();
        fsm.Add(new UpState(GameState.Up, _config.upStateDuration));
        fsm.Add(new DownState(GameState.Down, _config.downStateDuration));
        
        platform = new PlatformModel(_config);
    }

    protected override void OnTimeUpdate()
    {
        platform.UpdateTime(Time);
        fsm.CurrentState.UpdateTime(Time);
    }
}