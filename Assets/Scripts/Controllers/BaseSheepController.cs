using System.Collections;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Animation))]
    public abstract class BaseSheepController : MonoBehaviour
    {
        protected Animation _animation;
        protected BaseSheepModel _model;
        public float runSpeed = 1f;
        public float speed = 0.2f;

        protected virtual void Awake()
        {
            _animation = GetComponent<Animation>();
        }

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
            if (transform.position.y < 2) Death();
        }

        private void OnDeath(BaseSheepModel model)
        {
            var clipName = "Death";
            var clip = _animation.GetClip(clipName);
            _animation.Play(clipName);
            StartCoroutine(OnDeathAnimComplete(clip.length));
        }

        public IEnumerator OnDeathAnimComplete(float clipLength)
        {
            yield return new WaitForSeconds(clipLength);
            Destroy(gameObject);
        }

        private void OnUpdate(BaseSheepModel model)
        {
            MoveOnUpdate();
        }

        protected abstract void MoveOnUpdate();

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            var character = hit.gameObject.GetComponent<CharacterController>();
            if (character == null) return;

            var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            character.SimpleMove(pushDir * speed);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "death") Death();
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