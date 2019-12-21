public abstract class BaseModel
{
    protected float Time { get; private set; }
    public void UpdateTime(float time)
    {
        Time = time;
        OnTimeUpdate();
    }

    protected abstract void OnTimeUpdate();

    public abstract void Release();
}