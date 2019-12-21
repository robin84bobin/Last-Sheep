using System;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Animation))]
    public class PlatformController : MonoBehaviour
    {
        private Animation _animation;
        private GameModel _gameModel;
        private PlatformModel _platformModel;
        
        public void Init(GameModel gameModel)
        {
            _gameModel = gameModel;
            _gameModel.Fsm.OnStateChanged += OnGameStateChanged;
        }

        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Up:
                    _animation.Play("Up");
                    break;
                case GameState.Down: _animation.Play("Down");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}