using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    [Header("Jump")]
    public float jumpHeight;
    public LayerMask groundLayers;

    [HideInInspector]
    public bool isRunning;
    [HideInInspector]
    public bool isJumping;
    [HideInInspector]
    public bool isFalling;
    public bool isGrounded;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(directionX * playerSpeed, rb.velocity.y);

        isRunning = rb.velocity.magnitude > 0;
        isJumping = rb.velocity.y > .1f;
        isFalling = rb.velocity.y < -.1f;
        isGrounded = IsGrounded();

        if (Input.GetButtonDown("Jump") & isGrounded)
        {
            Jump();
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    public bool IsGrounded() => boxCollider.IsTouchingLayers(groundLayers);
}
