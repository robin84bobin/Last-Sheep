using Model;
using Model.FSM;

namespace Controllers
{
    public class BotSheepModel : BaseModel
    {
        public bool EnableMoving = true;

        private FSM<SheepState, BaseState<SheepState>> _fsm;
        public IStateMachine<SheepState> State => _fsm;
        
        public BotSheepModel()
        {
            _fsm = new FSM<SheepState, BaseState<SheepState>>();
            _fsm.Add(new SheepWalkState());
            _fsm.Add(new SheepGoToTagretState());
            _fsm.Add(new SheepDeathState());
        }
        
        public override void Release()
        {
            _fsm.Release();
        }

        public void Update()
        {
            //
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
        public SheepGoToTagretState() : base(SheepState.GoToTagret)
        {
        }

        public override void OnEnterState()
        {
            
        }

        public override void OnExitState()
        {
            
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