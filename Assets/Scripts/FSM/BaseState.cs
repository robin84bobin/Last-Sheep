namespace Model.FSM
{
    public abstract class BaseState<TKey>
    {
        public BaseState(TKey name)
        {
            Name = name;
        }

        protected IStateMachine<TKey> _owner { get; private set; }
        public TKey Name { get; }

        public abstract void OnEnterState();
        public abstract void OnExitState();

        public void SetOwner(IStateMachine<TKey> owner)
        {
            _owner = owner;
        }

        public virtual void Release()
        {
            _owner = null;
        }
    }
}