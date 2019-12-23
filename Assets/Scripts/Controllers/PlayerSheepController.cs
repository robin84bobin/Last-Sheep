using Controllers;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerSheepController : BaseSheepController
{
    private Camera camera;
    private CharacterController characterController;
    public float movingGap = 2f;
    public Transform plane;
    public float turnDuration = 0.2f;

    protected override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
    }

    protected override void OnStateChanged(SheepState state)
    {
        //
    }

    protected override void MoveOnUpdate()
    {
        var moveVector = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
            var targetPoint = GetPlaneIntersection(plane, camera);
            targetPoint.Set(targetPoint.x, transform.position.y, targetPoint.z);
            transform.DOLookAt(targetPoint, turnDuration);
            var delta = targetPoint - transform.position;
            if (delta.magnitude > movingGap) moveVector = delta.normalized * speed;
        }

        characterController.SimpleMove(moveVector);
    }

    private Vector3 GetPlaneIntersection(Transform plane, Camera camera)
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        var delta = ray.origin.y - plane.position.y;
        var dirNorm = ray.direction / ray.direction.y;
        var intersection = ray.origin - dirNorm * delta;
        return intersection;
    }
}