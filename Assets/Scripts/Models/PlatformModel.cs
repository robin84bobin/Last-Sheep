using System;
using UnityEngine;

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
        public float DecreaseFactor { get; }

        public PlatformModel(GameConfig config)
        {
            _highlightPeriod = config.GetPlatformHighLightPeriod();
            DecreaseFactor = config.Platform.decreaseCapacityFactor;
        }

        public void Down()
        {
            OnDown?.Invoke();
        }

        public void Up()
        {
            OnUp?.Invoke();
        }

        public void Update()
        {
            if (_timeToHighlight > 0 && Time.time >= _timeToHighlight)
            {
                OnAppear?.Invoke();
                _timeToHighlight = Time.time + _highlightPeriod;
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