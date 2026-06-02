using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float JumpVel;
    [SerializeField]
    private float HighJumpVel;
    [SerializeField]
    private float RecoilVel;
    private float MoveAxes;
    private Rigidbody2D rb;
    private InputSystem inputActions;
    private HeadCheck headCheck;
    private bool isGrounded = true;
    private bool crouched = false;
    [SerializeField] private MoveCam Cam;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    public bool Grown = false;
    public bool Fire = false;
    [SerializeField] private Material fireMaterial;
    [SerializeField] private Material NormalMaterial;

    [SerializeField] private AnimationCurve slowCurve;
    private Coroutine slowToZeroCoroutine;
    private bool unKillable = false;

    public int coinCount;

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
        inputActions.Player.Crouch.started += ctx =>
        {
            if (Grown)
                crouched = true;
        };
        inputActions.Player.Crouch.canceled += ctx => crouched = false;
        inputActions.Player.Sprint.started += ctx =>
        {

            playerAnimate.Sprint();
            Speed = Speed / 0.75f;
        };
        inputActions.Player.Sprint.canceled += ctx =>
        {

            playerAnimate.StopSprint();
            Speed = Speed * 0.75f;
        };

        inputActions.Player.Move.canceled += ctx =>
        {
            if (slowToZeroCoroutine != null)
                StopCoroutine(slowToZeroCoroutine);

            slowToZeroCoroutine = StartCoroutine(slowToZero(0.3f));
        };

        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckRadius * 8, groundCheckRadius), 0f, groundLayer);

        if (!isGrounded && rb.linearVelocityY >= 0)
        {
            headCheck.checkHead(Grown);
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
        if (slowToZeroCoroutine != null)
        {
            StopCoroutine(slowToZeroCoroutine);
            slowToZeroCoroutine = null;
        }

        if (!crouched)
            MoveAxes = context.ReadValue<float>();
    }
    void EnterPipe(Pipe pipe)
    {
        transform.position = pipe.tpPos;
        Cam.CamMaxX = pipe.camMaxX;
        Cam.CamMinX = pipe.camMinX;
        Cam.CamX = pipe.camMinX;
        Cam.CamY = pipe.camY;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Mushroom"))
        {
            Destroy(other.gameObject);
            if (!Grown)
            {
                playerAnimate.grow();
                unKillable = true;
                playerAnimate.Unkillable(true);
                Grown = true;
                StartCoroutine(StopUnkillable(0.7f));
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Grown)
            {
                playerAnimate.shrink();
                Grown = false;
                Fire = false;
                sprite.material = NormalMaterial;
                unKillable = true;
                playerAnimate.Unkillable(true);
                StartCoroutine(StopUnkillable(1.333f));
            }
            if (!Grown && !unKillable)
            {
                playerAnimate.dead();
                Destroy(this);
            }
        }
    }

    IEnumerator StopUnkillable(float time)
    {
        yield return new WaitForSeconds(time);
        unKillable = false;
        playerAnimate.Unkillable(false);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pipe"))
        {
            Pipe pipe = other.transform.GetComponentInParent<Pipe>();
            if (crouched && pipe.pipeType == Pipe.PipeType.Vertical)
            {
                EnterPipe(pipe);
            }
            if (MoveAxes > 0.2f && pipe.pipeType == Pipe.PipeType.Horizontal)
            {
                EnterPipe(pipe);
            }
        }
        if (other.gameObject.CompareTag("Coins"))
        {
            Tilemap tilemap = other.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                Vector3Int cellPos = tilemap.WorldToCell(new Vector3(transform.position.x, transform.position.y - 1));

                if (tilemap.GetTile(cellPos) != null)
                {
                    tilemap.SetTile(cellPos, null);
                    coinCount += 1;
                }
                cellPos = tilemap.WorldToCell(new Vector3(transform.position.x + .5f, transform.position.y - 1));

                if (tilemap.GetTile(cellPos) != null)
                {
                    tilemap.SetTile(cellPos, null);
                    coinCount += 1;
                }
                cellPos = tilemap.WorldToCell(new Vector3(transform.position.x - .5f, transform.position.y - 1));

                if (tilemap.GetTile(cellPos) != null)
                {
                    tilemap.SetTile(cellPos, null);
                    coinCount += 1;
                }
                if (Grown)
                {
                    cellPos = tilemap.WorldToCell(new Vector3(transform.position.x, transform.position.y));
                    if (tilemap.GetTile(cellPos) != null)
                    {
                        tilemap.SetTile(cellPos, null);
                        coinCount += 1;
                    }
                    cellPos = tilemap.WorldToCell(new Vector3(transform.position.x + .5f, transform.position.y));
                    if (tilemap.GetTile(cellPos) != null)
                    {
                        tilemap.SetTile(cellPos, null);
                        coinCount += 1;
                    }
                    cellPos = tilemap.WorldToCell(new Vector3(transform.position.x - .5f, transform.position.y));
                    if (tilemap.GetTile(cellPos) != null)
                    {
                        tilemap.SetTile(cellPos, null);
                        coinCount += 1;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            try
            {
                other.transform.parent.GetComponent<GoombaController>().Kill();
                other.transform.GetComponent<BoxCollider2D>().enabled = false;
                rb.linearVelocityY = RecoilVel;

            }
            finally { }
        }
        if (other.gameObject.CompareTag("Flower"))
        {
            Fire = true;
            sprite.material = fireMaterial;
            Destroy(other.gameObject);
        }
    }
    private IEnumerator slowToZero(float dur)
    {
        float startValue = MoveAxes;
        float time = 0f;

        while (time < dur)
        {
            time += Time.deltaTime;
            MoveAxes = Mathf.Lerp(startValue, 0f, time / dur);
            yield return null;
        }
        MoveAxes = 0f;
        slowToZeroCoroutine = null;
    }
}
