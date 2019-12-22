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
            Vector3 point = Vector3.zero;
            //todo calc appear point using bounds
            //...
            transform.position = point;
            _animation.Play("Appear");

            GetComponent<Renderer>().material.shader = Shader.Find("Mobile/Outline");
        }
        
        
        private void MoveDown()
        {
            _animation.Play("Down");
        }

        private void MoveUp()
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Standart");
            _animation.Play("Up");
        }
    }
}