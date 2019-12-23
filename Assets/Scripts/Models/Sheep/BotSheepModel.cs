using System;
using Model;
using Model.FSM;

namespace Controllers
{
    public class BotSheepModel : BaseSheepModel
    {
        public BotSheepModel():base()
        {
            _fsm.Add(new SheepWalkState());
            _fsm.Add(new SheepGoToTagretState());
        }
    }

    public class BaseSheepModel : BaseModel
    {
        public event Action<BaseSheepModel> OnDeath;
        public event Action<BaseSheepModel> OnUpdate;
        
        protected FSM<SheepState, BaseState<SheepState>> _fsm;
        public IStateMachine<SheepState> State => _fsm;

        public BaseSheepModel()
        {
            _fsm = new FSM<SheepState, BaseState<SheepState>>();
            _fsm.Add(new SheepDeathState());
            _fsm.OnStateChanged += OnStateChanged;
        }
        
        private void OnStateChanged(SheepState state)
        {
            if (state ==  SheepState.Death){
                OnDeath?.Invoke(this);
            }
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

    public enum SheepState
    {
        Walk,
        GoToTagret,
        Death
    }

    public class SheepDeathState : BaseState<SheepState> {
        public SheepDeathState() : base(SheepState.Death) { }
        public override void OnEnterState() {
        }

        public override void OnExitState() {
        }
    }

    public class SheepGoToTagretState : BaseState<SheepState>{
        public SheepGoToTagretState() : base(SheepState.GoToTagret){
        }

        public override void OnEnterState(){
            
        }

        public override void OnExitState(){
            
        }
    }
    
    public class SheepWalkState : BaseState<SheepState>
    {
        public SheepWalkState() : base(SheepState.Walk)
        {
        }

        public override void OnEnterState()
        {
            //
        }

        public override void OnExitState()
        {
            //
        }
    }
}