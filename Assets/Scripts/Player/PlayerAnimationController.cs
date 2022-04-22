using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private PlayerController playerController;
    private PlayerBehaviour playerBehaviour;
    private Rigidbody2D rb;
    private bool playerFlipX;

    private enum MovementState
    {
        IDLE,
        RUNNING,
        JUMPING,
        FALLING
    }

    private MovementState movementState = MovementState.IDLE;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerBehaviour = GetComponent<PlayerBehaviour>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (rb.velocity.x < 0) playerFlipX = true;
        else if (rb.velocity.x > 0) playerFlipX = false;

        if (playerController.isRunning && playerController.isGrounded)
            movementState = MovementState.RUNNING;
        else if(playerController.isJumping)
            movementState = MovementState.JUMPING;
        else if (playerController.isFalling)
            movementState = MovementState.FALLING;
        else
            movementState = MovementState.IDLE;

        spriteRenderer.flipX = playerFlipX;

        animator.SetInteger("state", (int)movementState);
        animator.SetBool("isTakingDamage", playerBehaviour.isInvinsible);
    }
}
