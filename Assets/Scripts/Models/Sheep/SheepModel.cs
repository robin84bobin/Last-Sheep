using Model;
using Model.FSM;

namespace Controllers
{
    public class SheepModel : BaseModel
    {
        public bool EnableMoving;

        private FSM<SheepState, BaseState<SheepState>> _fsm;
        public IStateMachine<SheepState> State => _fsm;
        
        public SheepModel()
        {
            _fsm = new FSM<SheepState, BaseState<SheepState>>();
            _fsm.Add(new SheepWalkState());
            _fsm.Add(new SheepGoToTagretState());
        }
        
        public override void Release()
        {
            _fsm.Release();
        }
    }

   public enum SheepState
    {
        Walk,
        GoToTagret
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