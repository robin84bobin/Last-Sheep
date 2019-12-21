using System;
using System.Collections.Generic;
using Model.FSM;
using UnityEngine;

namespace Model
{
    public interface IStateMachine<TStateName>
    {
        event Action<TStateName> OnStateChanged;
        void SetState(TStateName key, bool restartCurrentState = false);
    }

    public class FSM<TStateName, TState> : IStateMachine<TStateName> where TState : BaseState<TStateName>
    {
        public event Action<TStateName> OnStateChanged;
        public TState CurrentState { get; private set; }
        private Dictionary<TStateName, TState> _states = new Dictionary<TStateName, TState>();

        public void Add(TState state)
        {
            if (!_states.ContainsKey(state.Name))
            {
                _states.Add(state.Name, state);
            }

            _states[state.Name].SetOwner(this);
        }

        public void Remove(TStateName key)
        {
            _states[key].Release();
            _states.Remove(key);
        }

        public void SetState(TStateName key, bool restartCurrentState = false)
        {
            Debug.Log("setState: " + key);
            
            if (!restartCurrentState && CurrentState != null && CurrentState.Name.Equals(key))
                return;
            
            if (CurrentState != null)
            {
                CurrentState.OnExitState();
            }

            CurrentState = _states[key];
            CurrentState.OnEnterState();

            OnStateChanged?.Invoke(key);
        }

        public void Release()
        {
            var keys = new List<TStateName>(_states.Keys);
            foreach (var key in keys)
            {
                Remove(key);
            }
        }
    }

}


