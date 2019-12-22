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
    }

    protected override void MoveOnUpdate()
    {
        moveVector = transform.forward * speed;
        //TODO some logic...
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
