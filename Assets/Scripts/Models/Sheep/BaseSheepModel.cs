using System;
using Model;
using Model.FSM;
using UnityEngine;

namespace Controllers
{
    public class BaseSheepModel : BaseModel
    {
        protected FSM<SheepState, BaseState<SheepState>> _fsm;

        public BaseSheepModel()
        {
            _fsm = new FSM<SheepState, BaseState<SheepState>>();
            _fsm.Add(new BaseSheepState(SheepState.Death));
            _fsm.OnStateChanged += OnStateChanged;
        }

        public bool isDead { get; protected set; }
        public IStateMachine<SheepState> State => _fsm;
        public Vector3 TargetPosition { get; set; }

        public event Action<BaseSheepModel> TryKill;
        public event Action<BaseSheepModel> OnDeath;
        public event Action<BaseSheepModel> OnUpdate;

        private void OnStateChanged(SheepState state)
        {
            if (state == SheepState.Death)
            {
                OnDeath?.Invoke(this);
                isDead = true;
            }
        }

        public void TryToKill()
        {
            TryKill?.Invoke(this);
        }

        public void Update()
        {
            OnUpdate?.Invoke(this);
        }

        public override void Release()
        {
            _fsm.Release();
            OnDeath = null;
            OnUpdate = null;
        }
    }
}