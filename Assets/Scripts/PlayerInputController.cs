using DG.Tweening;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private Camera camera;
    public float speed;
    public float turnDuration = 0.2f;
    public Transform plane;
    private float movingGap = 0.5f;

    private void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 targetPoint = GetPlaneIntersection(plane, camera);
            transform.LookAt(targetPoint);
            var delta = (targetPoint - transform.position);
            if (delta.magnitude <= movingGap)
            {
                return;
            }
            var moveVector = delta.normalized * speed;
            transform.position += moveVector;
        }
    }
    
    Vector3 GetPlaneIntersection(Transform plane, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        float delta = ray.origin.y - plane.position.y;
        Vector3 dirNorm = ray.direction / ray.direction.y;
        Vector3 intersection = ray.origin - dirNorm * delta;
        return intersection;
    }
}
