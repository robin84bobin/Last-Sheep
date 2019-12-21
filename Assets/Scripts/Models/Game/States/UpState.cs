
public class UpState : BaseGameState
{
    private float _timeEnd;
    private float _duration;

    public UpState(GameState name, float duration) : base(name)
    {
        _duration = duration;
    }

    public override void OnEnterState()
    {
        _timeEnd = Time + _duration;
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
            _owner.SetState(GameState.Down);
        }
    }
}