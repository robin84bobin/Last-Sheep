using Controllers;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BotSheepController : BaseSheepController
{
    private CharacterController _characterController;
    private Vector3 _moveVector;
    public float turnDuration = 0.2f;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
    }

    protected override void OnStateChanged(SheepState state)
    {
        if (state.Equals(SheepState.Walk))
        {
            var rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
            transform.DORotate(rotation.eulerAngles, turnDuration);
        }
    }

    protected override void MoveOnUpdate()
    {
        if (_model.State.CurrentStateName.Equals(SheepState.Stop))
        {
            _moveVector = Vector3.zero;
        }
        else if (_model.State.CurrentStateName.Equals(SheepState.Walk))
        {
            _moveVector = transform.forward * speed;
        }
        else if (_model.State.CurrentStateName.Equals(SheepState.GoToTagret))
        {
            var targetPoint = new Vector3(_model.TargetPosition.x, transform.position.y, _model.TargetPosition.z);
            transform.DOLookAt(targetPoint, turnDuration);
            _moveVector = transform.forward * runSpeed;
        }

        _characterController.SimpleMove(_moveVector);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.tag == "field bounds")
        {
            var newAngles = new Vector3(0f, transform.localRotation.eulerAngles.y - 180, 0f);
            transform.DOLocalRotate(newAngles, 0.2f);
        }
    }
}