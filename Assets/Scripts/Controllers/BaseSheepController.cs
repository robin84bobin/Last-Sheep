using UnityEngine;

namespace Controllers
{
    public abstract class BaseSheepController : MonoBehaviour
    {
        public float speed = 0.2f;
        protected SheepModel _model;
        
        public void Init(SheepModel model)
        {
            _model = model;
        }

        public virtual void Update()
        {
            if (_model != null && !_model.EnableMoving){
                return;
            }

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
    }
}