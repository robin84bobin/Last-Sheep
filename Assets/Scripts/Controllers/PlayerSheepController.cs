using Controllers;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerSheepController : BaseSheepController
{
    private Camera camera;
    public float turnDuration = 0.2f;
    public Transform plane;
    public float movingGap = 2f;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
    }

    protected override void MoveOnUpdate()
    {
        var moveVector = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
            Vector3 targetPoint = GetPlaneIntersection(plane, camera);
            targetPoint.Set(targetPoint.x, transform.position.y, targetPoint.z);
            transform.DOLookAt(targetPoint,turnDuration);
            var delta = (targetPoint - transform.position);
            if (delta.magnitude > movingGap)
            {
                moveVector = delta.normalized * speed;
            }
        }
        characterController.SimpleMove(moveVector);
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
