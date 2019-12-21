public class DownState : BaseGameState
{
    private float _timeEnd;
    private float _duration;

    public DownState(GameState name, float duration) : base(name)
    {
        _duration = duration;
    }

    public override void OnEnterState()
    {
        _timeEnd = UnityEngine.Time.time + _duration;
    }

    public override void OnExitState()
    {
        _timeEnd = 0;
    }

    public override void UpdateTime(float time)
    {
        base.UpdateTime(time);
        if (Time > _timeEnd)
        {
            _owner.SetState(GameState.Up);
        }
    }

}