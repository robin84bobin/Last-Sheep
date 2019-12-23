using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    [RequireComponent(typeof(Animation))]
    public class PlatformController : MonoBehaviour
    {
        public event Action OnAppear;
        
        private Animation _animation;
        private PlatformModel _platformModel;
        private GameObject _field;

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
            Vector3 point = Vector3.zero;
            
            var defaultScale = transform.localScale;

            var scaleX = transform.localScale.x * _platformModel.DecreaseFactor;
            var scaleZ = transform.localScale.z * _platformModel.DecreaseFactor;
            transform.localScale = new Vector3(scaleX, transform.localScale.y, scaleZ);
            if (transform.localScale.x * transform.localScale.z < 1) {
                transform.localScale = defaultScale;
            }
            
            //todo calc appear point using bounds
            var boundsField = _field.GetComponent<Renderer>().bounds;
            var boundsPlatform = GetComponent<Renderer>().bounds;

            float maxX = boundsField.max.x - boundsPlatform.size.x * .5f;
            float minX = boundsField.min.x + boundsPlatform.size.x * .5f;
            var x = Random.Range(minX, maxX);
            
            float maxZ = boundsField.max.z - boundsPlatform.size.z * .5f;
            float minZ = boundsField.min.z + boundsPlatform.size.z * .5f;
            var z = Random.Range(minZ, maxZ);
            point = new Vector3(x,0.0001f,z);
            //...
            
            transform.position = point;
            _animation.Play("Appear");
            
            OnAppear?.Invoke();
        }
        
        private void MoveDown()
        {
            Vector3 downPosition = new Vector3(transform.position.x, -0.1f, transform.position.z);
            transform.DOMove(downPosition, 0.3f);
        }

        private void MoveUp()
        {
            Vector3 upPosition = new Vector3(transform.position.x, 1f, transform.position.z);
            transform.DOMove(upPosition, 1f);
        }

    }
}