using Controllers;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BotSheepController : BaseSheepController
{
    public float speed;
    public float turnDuration = 0.2f;
    public Transform plane;
    public float movingGap = 2f;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    protected override void MoveOnUpdate()
    {
        var moveVector = Vector3.zero;
        //TODO some logic...
        characterController.SimpleMove(moveVector);
    }
}
