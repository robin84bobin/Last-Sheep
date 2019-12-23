using UnityEngine;

public class KillState : BaseGameState
{
    private float _timeEnd;
    private float _duration;
    
    public KillState(float duration) : base(GameState.Kill)
    {
        _duration = duration;
    }

    public override void OnEnterState()
    {
        _timeEnd = Time.time + _duration;
    }

    public override void OnExitState()
    {
        _timeEnd = 0;
    }

    public override void Update()
    {
        if (Time.time> _timeEnd)
        {
            _owner.SetState(GameState.Down);
        }
    }
}
public class DownState : BaseGameState
{
    private float _timeEnd;
    private float _duration;

    public DownState(float duration) : base(GameState.Down)
    {
        _duration = duration;
    }

    public override void OnEnterState()
    {
        _timeEnd = Time.time + _duration;
    }

    public override void OnExitState()
    {
        _timeEnd = 0;
    }

    public override void Update()
    {
        if (Time.time> _timeEnd)
        {
            _owner.SetState(GameState.Up);
        }
    }
}