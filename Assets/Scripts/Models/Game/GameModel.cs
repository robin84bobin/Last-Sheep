using System;
using System.Collections.Generic;
using Controllers;
using Model;
using UnityEngine;

public enum GameState
{
    Up,
    Down,
    Kill
}

public class GameModel : BaseModel
{
    public event Action GameOver;
    
    private FSM<GameState, BaseGameState> fsm;
    public IStateMachine<GameState> State => fsm;
    
    private readonly GameConfig _config;
    public BaseSheepModel playerSheepModel;

    public PlatformModel platform { get; }
    public List<BaseSheepModel> botSheeps { get; private set; }
    
    public GameModel(GameConfig config)
    {
        _config = config;
        
        fsm = new FSM<GameState, BaseGameState>();
        fsm.Add(new UpState(_config.upStateDuration));
        fsm.Add(new DownState(_config.downStateDuration));
        fsm.Add(new KillState(_config.killStateDuration));
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
        playerSheepModel = new BaseSheepModel();
        playerSheepModel.OnDeath += OnPlayerDeath;
        
        botSheeps = new List<BaseSheepModel>(_config.defaultSheepsCount);
        for (int i = 0; i < _config.defaultSheepsCount; i++)
        {
            var botSheepModel = new BotSheepModel();
            botSheepModel.OnDeath += OnBotDeath;
            botSheeps.Add(botSheepModel);
        }
    }

    private void OnPlayerDeath(BaseSheepModel player)
    {
        GameOver?.Invoke();
    }

    private void OnBotDeath(BaseSheepModel botSheepModel)
    {
        botSheeps.Remove(botSheepModel);
    }

    private void SetSheepsState(SheepState state)
    {
        foreach (var sheep in botSheeps)
        {
            sheep.State.SetState(state);
        }
    }

    private void OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Kill:
                Kill();
                break;
            case GameState.Up:
                platform.Up();
                SetSheepsState(SheepState.GoToTagret);
                
                break;
            case GameState.Down:
                platform.Down();
                SetSheepsState(SheepState.Walk);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void Kill()
    {
        playerSheepModel.TryToKill();
        foreach (var sheep in botSheeps)
        {
            sheep.TryToKill();
        }
    }

    public void Update()
    {
        platform.Update();
        fsm.CurrentState.Update();
        
        playerSheepModel.Update();
        foreach (var sheep in botSheeps)
        {
            sheep.Update();
        }
    }

    public override void Release()
    {
        playerSheepModel.Release();
        foreach (var sheep in botSheeps)
        {
            sheep.Release();
        }
        platform.Release();
        fsm.Release();
    }

    public void SetSheepsTarget(Vector3 position)
    {
        foreach (var sheep in botSheeps)
        {
            sheep.TargetPosition = position;
        }
    }
}