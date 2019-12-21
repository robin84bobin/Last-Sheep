using System;

namespace Controllers
{
    public class PlatformModel : BaseModel
    {
        public event Action OnUp;
        public event Action OnDown;
        public event Action OnAppear;
        
        private readonly GameConfig _config;
        private readonly float _highlightPeriod;
        private float _timeToHighlight;

        public PlatformModel(GameConfig config)
        {
            _highlightPeriod = config.GetPlatformHighLightPeriod();
        }

        public void Down()
        {
            OnDown?.Invoke();
        }

        public void Up()
        {
            OnUp?.Invoke();
        }

        protected override void OnTimeUpdate()
        {
            if (Time >= _timeToHighlight)
            {
                OnAppear?.Invoke();
                _timeToHighlight = Time + _highlightPeriod;
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