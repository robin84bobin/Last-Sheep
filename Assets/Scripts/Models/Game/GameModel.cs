using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using UnityEngine;

public class GameModel : BaseModel
{
    private readonly GameConfig _config;

    public GameModel(GameConfig config)
    {
        _config = config;
        CreatePlatform();
        CreateSheep();
    }

    public PlatformModel platform { get; private set; }
    public BaseSheepModel playerSheepModel { get; private set; }
    public List<BaseSheepModel> botSheeps { get; private set; }
    public event Action<GameResult> GameOver;

    private void CreatePlatform()
    {
        platform = new PlatformModel(_config.Platform);
        platform.OnAppear += OnPlatformAppear;
        platform.OnUp += OnPlatformUp;
        platform.OnDown += OnPlatformDown;
    }

    private void CreateSheep()
    {
        playerSheepModel = new BaseSheepModel();
        playerSheepModel.OnDeath += OnPlayerDeath;

        botSheeps = new List<BaseSheepModel>(_config.defaultSheepsCount);
        for (var i = 0; i < _config.defaultSheepsCount; i++)
        {
            var botSheepModel = new BotSheepModel();
            botSheepModel.Walk();
            botSheeps.Add(botSheepModel);
        }
    }

    public void Start()
    {
        platform.Down();
        SetSheepState(SheepState.Walk);
    }

    public void Update()
    {
        platform.Update();
        playerSheepModel.Update();

        botSheeps.RemoveAll(sheep => sheep.isDead);
        foreach (var sheep in botSheeps) sheep.Update();
    }

    private void OnPlatformUp()
    {
        SetSheepState(SheepState.Stop);
    }

    private void OnPlatformAppear()
    {
        SetSheepState(SheepState.GoToTagret);
    }

    private void OnPlatformDown()
    {
        if (CheckWin()) GameOver?.Invoke(GameResult.Win);
    }

    private bool CheckWin()
    {
        return !playerSheepModel.isDead && botSheeps.Count() == 0;
    }

    private void OnPlayerDeath(BaseSheepModel player)
    {
        GameOver?.Invoke(GameResult.Lose);
    }

    public void SetSheepState(SheepState state)
    {
        foreach (var sheep in botSheeps) sheep.State.SetState(state);
    }

    public void Kill()
    {
        playerSheepModel.TryToKill();
        foreach (var sheep in botSheeps) sheep.TryToKill();
    }

    public void SetSheepTarget(Vector3 position)
    {
        foreach (var sheep in botSheeps) sheep.TargetPosition = position;
    }

    public override void Release()
    {
        playerSheepModel.Release();
        foreach (var sheep in botSheeps) sheep.Release();
        platform.Release();
    }
}

public enum GameResult
{
    Win,
    Lose
}