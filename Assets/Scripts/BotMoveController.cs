using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BotMoveController : MonoBehaviour
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

    void Update()
    {
        var moveVector = Vector3.zero;
        //TODO some logic...
        characterController.SimpleMove(moveVector);
    }
}
