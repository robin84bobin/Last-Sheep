using System;

namespace Controllers
{
    public class PlatformModel : BaseModel
    {
        public event Action OnUp;
        public event Action OnDown;
        public event Action OnHighlight;
        
        private readonly GameConfig _config;

        public PlatformModel(GameConfig config)
        {
            _config = config;
        }

        public void Down()
        {
            ResetHighlightTimer();
            OnDown?.Invoke();
        }

        public void Up()
        {
            OnUp?.Invoke();
        }

        private void ResetHighlightTimer()
        {
            throw new NotImplementedException();
        }

        protected override void OnTimeUpdate()
        {
            throw new NotImplementedException();
        }
    }
}