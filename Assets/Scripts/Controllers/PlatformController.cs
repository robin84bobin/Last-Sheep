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
            
            var defaultScale = transform.localScale;

            transform.localScale *= _platformModel.DecreaseFactor;
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
            point = new Vector3(x,0f,z);
            //...
            
            transform.position = point;
            _animation.Play("Appear");

            //GetComponent<Renderer>().material.shader = Shader.Find("Mobile/Outline");
        }
        
        
        private void MoveDown()
        {
            _animation.Play("Down");
        }

        private void MoveUp()
        {
            //GetComponent<Renderer>().material.shader = Shader.Find("Standart");
            _animation.Play("Up");
        }
    }
}