using Controllers;
using Model.FSM;
using UnityEngine;

public class BasePlatformState : BaseState<PlatformState>
{
    private readonly float _duration;
    private readonly PlatformState _nextState;
    private float _timeEnd;

    public BasePlatformState(PlatformState state, PlatformState nextState, float duration) : base(state)
    {
        _duration = duration;
        _nextState = nextState;
    }

    public override void OnEnterState()
    {
        _timeEnd = Time.time + _duration;
    }

    public override void OnExitState()
    {
        _timeEnd = 0;
    }

    public void Update()
    {
        if (Time.time > _timeEnd) _owner.SetState(_nextState);
    }
}