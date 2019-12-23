using UnityEngine;

namespace Controllers
{
    public abstract class BaseSheepController : MonoBehaviour
    {
        public float speed = 0.2f;
        public float runSpeed = 1f;
        protected BaseSheepModel _model;

        public void Init(BaseSheepModel model)
        {
            _model = model;
            _model.State.OnStateChanged += OnStateChanged;
            _model.OnDeath += OnDeath;
            _model.TryKill += OnTryKill;
            _model.OnUpdate += OnUpdate;
        }

        protected abstract void OnStateChanged(SheepState state);

        private void OnTryKill(BaseSheepModel model)
        {
            if (transform.position.y < 2)
            {
                Death();
            }
        }

        private void OnDeath(BaseSheepModel model)
        {
            Destroy(gameObject);
        }

        //TODO remove model param
        private void OnUpdate(BaseSheepModel model)
        {
            MoveOnUpdate();
        }

        protected abstract void MoveOnUpdate();

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            var character = hit.gameObject.GetComponent<CharacterController>();
            if (character == null)
            {
                return;
            }

            var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            character.SimpleMove(pushDir * speed);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "death")
            {
                Death();
            }
        }

        private void Death()
        {
            if (_model != null)
            {
                _model.State.SetState(SheepState.Death);
                _model.Release();
            }
        }
    }
}