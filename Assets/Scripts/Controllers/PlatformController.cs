using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    [RequireComponent(typeof(Animation))]
    public class PlatformController : MonoBehaviour
    {
        private Animation _animation;
        private GameObject _field;
        private PlatformModel _platformModel;

        private Tweener _tweener;
        public event Action OnMoveUpComplete;
        public event Action OnMoveDownComplete;
        public event Action OnAppear;

        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        public void Init(PlatformModel platformModel, GameObject field)
        {
            _platformModel = platformModel;
            _platformModel.OnUp += MoveUp;
            _platformModel.OnDown += MoveDown;
            _platformModel.OnAppear += Appear;

            _field = field;
        }

        private void Appear()
        {
            var point = Vector3.zero;
            var defaultScale = transform.localScale;

            var scaleX = transform.localScale.x * _platformModel.DecreaseFactor;
            var scaleZ = transform.localScale.z * _platformModel.DecreaseFactor;
            transform.localScale = new Vector3(scaleX, transform.localScale.y, scaleZ);
            if (transform.localScale.x * transform.localScale.z < 1) transform.localScale = defaultScale;

            // calc appear point using bounds
            var boundsField = _field.GetComponent<Renderer>().bounds;
            var boundsPlatform = GetComponent<Renderer>().bounds;

            var maxX = boundsField.max.x - boundsPlatform.size.x * .5f;
            var minX = boundsField.min.x + boundsPlatform.size.x * .5f;
            var x = Random.Range(minX, maxX);

            var maxZ = boundsField.max.z - boundsPlatform.size.z * .5f;
            var minZ = boundsField.min.z + boundsPlatform.size.z * .5f;
            var z = Random.Range(minZ, maxZ);
            point = new Vector3(x, 0.0001f, z);

            transform.position = point;
            _animation.Play("Appear");

            OnAppear?.Invoke();
        }

        private void MoveDown()
        {
            var downPosition = new Vector3(transform.position.x, -0.1f, transform.position.z);
            _tweener?.Kill();
            _tweener = transform.DOMove(downPosition, 0.3f);
            _tweener.onComplete = () => OnMoveDownComplete?.Invoke();
        }

        private void MoveUp()
        {
            _animation.Stop("Appear");
            var upPosition = new Vector3(transform.position.x, 1f, transform.position.z);
            _tweener?.Kill();
            _tweener = transform.DOMove(upPosition, 1f);
            _tweener.onComplete = () => OnMoveUpComplete?.Invoke();
        }

        public void Release()
        {
            _tweener?.Kill();
            OnMoveDownComplete = null;
            OnMoveUpComplete = null;
            OnAppear = null;
        }
    }
}