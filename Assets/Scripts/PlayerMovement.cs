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
    private HeadCheck headCheck;
    private bool isGrounded = true;
    private bool colliding = true;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        headCheck = transform.GetComponent<HeadCheck>();
        inputActions = new InputSystem();


        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.HighJump.performed += HighJump;
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Move.canceled += ctx => MoveAxes = 0;
    }
    void Update()
    {
        isGrounded = colliding;

        if (!isGrounded)
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (!isGrounded)
        {
            headCheck.checkHead();
        }
    }
    // void OnDrawGizmosSelected()
    // {
    //     if (groundCheck == null) return;
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    // }

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

    void OnCollisionEnter2D(Collision2D other)
    {
        colliding = true;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        colliding = false;
    }
}
