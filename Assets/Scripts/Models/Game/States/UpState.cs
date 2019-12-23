
using UnityEngine;

public class UpState : BaseGameState
{
    private float _timeEnd;
    private float _duration;

    public UpState(float duration) : base(GameState.Up)
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
        if (Time.time > _timeEnd)
        {
            _owner.SetState(GameState.Kill);
        }
    }
}