public abstract class BaseModel
{
    public void UpdateTime(float time)
    {
        Time = time;
        OnTimeUpdate();
    }

    protected abstract void OnTimeUpdate();

    public float Time { get; private set; }
}