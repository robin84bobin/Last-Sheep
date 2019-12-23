using UnityEngine;

namespace Controllers
{
    public abstract class BaseSheepController : MonoBehaviour
    {
        public float speed = 0.2f;
        protected BaseSheepModel _model;
        
        public void Init(BaseSheepModel model)
        {
            _model = model;
            _model.OnDeath += OnDeath;
            _model.OnUpdate += OnUpdate;
        }

        private void OnDeath(BaseSheepModel model)
        {
            Destroy(gameObject);
        }

        //TODO remove model param
        private void OnUpdate(BaseSheepModel model)
        {
            /*if (_model != null && !_model.EnableMoving){
                return;
            }*/

            MoveOnUpdate();
        }

        protected abstract void MoveOnUpdate();

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            var character = hit.gameObject.GetComponent<CharacterController>();
            if (character == null){
                return;
            }
            var pushDir = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);
            character.SimpleMove(pushDir * speed);
        }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "death") {
                if (_model != null) {
                    _model.State.SetState(SheepState.Death);
                    _model.Release();
                }
            }
        }

    }
}