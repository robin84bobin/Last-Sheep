using System;
using Model;

namespace Controllers
{
    public class PlatformModel : BaseModel
    {
        private readonly PlatformConfig _config;
        private float _timeToHighlight;
        private readonly FSM<PlatformState, BasePlatformState> fsm;

        public PlatformModel(PlatformConfig config)
        {
            _config = config;

            fsm = new FSM<PlatformState, BasePlatformState>();
            /*fsm.Add(new UpState(_config.upStateDuration));
            fsm.Add(new DownState(_config.downStateDuration));*/
            fsm.Add(new BasePlatformState(PlatformState.Up, PlatformState.Down, _config.upStateDuration));
            fsm.Add(new BasePlatformState(PlatformState.Down, PlatformState.Appear, _config.downStateDuration));
            fsm.Add(new BasePlatformState(PlatformState.Appear, PlatformState.Up, _config.highlightDuration));

            fsm.OnStateChanged += OnStateChanged;
        }

        public float DecreaseFactor => _config.decreaseFactor;
        //public IStateMachine<PlatformState> State => fsm;

        public event Action OnUp;
        public event Action OnDown;
        public event Action OnAppear;

        public void Update()
        {
            fsm.CurrentState.Update();
        }

        public void Down()
        {
            fsm.SetState(PlatformState.Down);
        }

        private void OnStateChanged(PlatformState state)
        {
            switch (state)
            {
                case PlatformState.Appear:
                    OnAppear?.Invoke();
                    break;
                case PlatformState.Up:
                    OnUp?.Invoke();
                    break;
                case PlatformState.Down:
                    OnDown?.Invoke();
                    break;
            }
        }

        public override void Release()
        {
            OnUp = null;
            OnDown = null;
            OnAppear = null;
        }
    }
}