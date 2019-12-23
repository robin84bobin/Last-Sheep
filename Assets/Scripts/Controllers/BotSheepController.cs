using Controllers;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BotSheepController : BaseSheepController
{
    public float turnDuration = 0.2f;
    public Transform plane;
    public float movingGap = 2f;
    private CharacterController characterController;

    Vector3 moveVector;
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        _model.State.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(SheepState state)
    {
        if (state.Equals(SheepState.Walk))
        {
            var rotation = Quaternion.Euler(0f, Random.Range(0,360), 0f);
            transform.DORotate(rotation.eulerAngles, turnDuration);
            //transform.rotation = rotation;
        }
    }

    protected override void MoveOnUpdate()
    {
        if (_model.State.CurrentStateName.Equals(SheepState.Walk))
        {
            moveVector = transform.forward * speed;
        }
        else   
        if (_model.State.CurrentStateName.Equals(SheepState.GoToTagret))
        {
            Vector3 targetPoint = new Vector3(_model.TargetPosition.x, transform.position.y, _model.TargetPosition.z);
            transform.DOLookAt(targetPoint,turnDuration);
            moveVector = transform.forward * runSpeed;
        }
        
        characterController.SimpleMove(moveVector);
    }

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        
        if (other.gameObject.tag == "field bounds")
        {
            Vector3 newAngles = new Vector3(0f,transform.localRotation.eulerAngles.y - 180, 0f );
            transform.DOLocalRotate(newAngles, 0.2f);
        }
    }

}
