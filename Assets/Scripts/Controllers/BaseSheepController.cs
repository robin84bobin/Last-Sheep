using UnityEngine;

namespace Controllers
{
    public abstract class BaseSheepController : MonoBehaviour
    {
        protected SheepModel _model;

        public void Init(SheepModel model)
        {
            _model = model;
        }

        protected virtual void Update()
        {
            if (!_model.EnableMoving)
            {
                return;
            }

            MoveOnUpdate();
        }

        protected abstract void MoveOnUpdate();
    }
}