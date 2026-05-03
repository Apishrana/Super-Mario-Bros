using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float JumpForce;

    private Rigidbody2D rb;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(Speed * Input.GetAxis("Horizontal"), rb.linearVelocityY);
    }

    void OnJump()
    {
        rb.AddForceY(JumpForce, ForceMode2D.Impulse);
    }
}
