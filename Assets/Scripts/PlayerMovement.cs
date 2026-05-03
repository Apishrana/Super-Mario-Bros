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

    private Rigidbody2D rb;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        InputSystem inputActions = new InputSystem();

        inputActions.Player.Enable();

        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.HighJump.performed += HighJump;
    }

    void Update()
    {
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(Speed * Input.GetAxis("Horizontal"), rb.linearVelocityY);
    }

    void HighJump(InputAction.CallbackContext context)
    {
        Debug.LogWarning(context);
        rb.linearVelocity += new Vector2(0, HighJumpVel);


    }
    void Jump(InputAction.CallbackContext context)
    {
        Debug.LogWarning(context);
        rb.linearVelocity += new Vector2(0, JumpVel);
    }
}
