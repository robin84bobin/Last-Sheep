using System;
using System.Collections.Generic;
using Controllers;
using Model;

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
    
    public PlatformModel platform { get; }
    public List<SheepModel> sheeps { get; private set; }
    
    public GameModel(GameConfig config)
    {
        _config = config;
        
        fsm = new FSM<GameState, BaseGameState>();
        fsm.Add(new UpState(GameState.Up, _config.upStateDuration));
        fsm.Add(new DownState(GameState.Down, _config.downStateDuration));
        fsm.OnStateChanged += OnStateChanged;
        
        platform = new PlatformModel(_config);
        platform.OnAppear += OnPlatformAppear;

        CreateSheeps();
        
        fsm.SetState(GameState.Down);
    }

    private void OnPlatformAppear()
    {
        SetSheepsState(SheepState.GoToTagret);
    }

    private void CreateSheeps()
    {
        sheeps = new List<SheepModel>(_config.defaultSheepsCount);
        for (int i = 0; i < _config.defaultSheepsCount; i++)
        {
            sheeps.Add(new SheepModel());
        }
    }

    private void SetSheepsState(SheepState state)
    {
        foreach (var sheep in sheeps)
        {
            sheep.State.SetState(state);
        }
    }

    private void OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Up:
                platform.Up();
                break;
            case GameState.Down:
                platform.Down();
                SetSheepsState(SheepState.Walk);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void Update()
    {
        platform.Update();
        fsm.CurrentState.Update();
        
        foreach (var sheep in sheeps)
        {
            sheep.Update();
        }
    }

    public override void Release()
    {
        platform.Release();
        fsm.Release();
    }
}