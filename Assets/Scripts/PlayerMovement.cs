using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float JumpVel;
    [SerializeField]
    private float HighJumpVel;
    private float MoveAxes;
    private Rigidbody2D rb;
    private InputSystem inputActions;
    private bool isGrounded = true;

    void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        inputActions = new InputSystem();


        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.HighJump.performed += HighJump;
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Move.canceled += ctx => MoveAxes = 0;
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }
    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(MoveAxes * Speed, rb.linearVelocityY);
    }

    void HighJump(InputAction.CallbackContext context)
    {
        rb.linearVelocity += new Vector2(0, HighJumpVel);
    }
    void Jump(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;
        isGrounded = false;
        rb.linearVelocity += new Vector2(0, JumpVel);
    }

    void Move(InputAction.CallbackContext context)
    {
        MoveAxes = context.ReadValue<float>();
    }
}
