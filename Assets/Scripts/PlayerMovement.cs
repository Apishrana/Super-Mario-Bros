using System.Threading.Tasks;
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
    private bool crouched = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    PlayerAnimate playerAnimate;

    void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        headCheck = transform.GetComponent<HeadCheck>();
        playerAnimate = transform.GetComponent<PlayerAnimate>();
        inputActions = new InputSystem();


        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.HighJump.performed += HighJump;
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Crouch.started += ctx => crouched = true;
        inputActions.Player.Crouch.canceled += ctx => crouched = false;
        inputActions.Player.Move.canceled += ctx => MoveAxes = 0;
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckRadius * 8, groundCheckRadius), 0f, groundLayer);

        if (!isGrounded)
        {
            headCheck.checkHead();
        }
        if (isGrounded)
        {
            playerAnimate.StopJump();
        }
        else
        {
            playerAnimate.Jump();
        }
        if (Mathf.Abs(MoveAxes) > 0.2f)
        {
            playerAnimate.Walk();
            if (MoveAxes < 0)
                sprite.flipX = true;
            else
                sprite.flipX = false;
        }
        else
        {
            playerAnimate.StopWalk();
        }
        if (crouched)
        {
            playerAnimate.Crouch();
        }
        else
        {
            playerAnimate.StopCrouch();
        }
    }

    // void OnDrawGizmosSelected()
    // {
    //     if (groundCheck == null) return;
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(groundCheck.position, new Vector3(groundCheckRadius * 8, groundCheckRadius, 0));
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
        if (!crouched)
            MoveAxes = context.ReadValue<float>();

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Mushroom"))
        {
            Destroy(other.gameObject);
            Task task = playerAnimate.grow();

        }

    }
}
