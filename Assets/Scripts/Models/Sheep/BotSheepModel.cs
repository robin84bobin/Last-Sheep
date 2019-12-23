using Model.FSM;

namespace Controllers
{
    public class BotSheepModel : BaseSheepModel
    {
        public BotSheepModel()
        {
            _fsm.Add(new BaseSheepState(SheepState.Walk));
            _fsm.Add(new BaseSheepState(SheepState.GoToTagret));
            _fsm.Add(new BaseSheepState(SheepState.Stop));
            _fsm.Add(new BaseSheepState(SheepState.Death));
        }

        public void Walk()
        {
            _fsm.SetState(SheepState.Walk);
        }
    }

    public class BaseSheepState : BaseState<SheepState>
    {
        public BaseSheepState(SheepState name) : base(name)
        {
        }

        public override void OnEnterState()
        {
        }

        public override void OnExitState()
        {
        }
    }
}