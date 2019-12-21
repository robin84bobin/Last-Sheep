using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Animation))]
    public class PlatformController : MonoBehaviour
    {
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
            var bounds = _field.GetComponent<Renderer>().bounds;
            
            Vector3 point = Vector3.zero;
            //todo calc appear point using bounds
            //...
            transform.position = point;
            _animation.Play("Appear");
        }
        
        
        private void MoveDown()
        {
            _animation.Play("Down");
        }

        private void MoveUp()
        {
            _animation.Play("Up");
        }
    }
}