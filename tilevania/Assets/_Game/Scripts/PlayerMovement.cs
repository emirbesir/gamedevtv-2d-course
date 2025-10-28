using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Walk Settings")]
    [SerializeField] private float walkSpeed = 6f;
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 6f;
    [Header("Climb Settings")]
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private LayerMask climbingLayer;
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;
    [Header("References")]
    [SerializeField] private GameObject playerVisual;

    private Vector2 moveInput;
    private bool isMovingHorizontally;
    private bool isMovingVertically;
    private bool isTouchingLadder;
    private float gravityScaleAtStart;

    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private SpriteRenderer rend;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();

        rend = playerVisual.GetComponent<SpriteRenderer>();
        anim = playerVisual.GetComponent<Animator>();

        gravityScaleAtStart = rb.gravityScale;
    }

    private void FixedUpdate()
    {
        Walk();
        FlipSprite();
        ClimbLadders();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Walk()
    {
        rb.linearVelocity = new Vector2(moveInput.x * walkSpeed, rb.linearVelocityY);

        isMovingHorizontally = Mathf.Abs(rb.linearVelocityX) > Mathf.Epsilon;
        anim.SetBool(Constants.PlayerAnimations.IS_WALKING, isMovingHorizontally);
    }
    
    private void FlipSprite()
    {
        if (isMovingHorizontally) rend.flipX = moveInput.x < 0f;
    }

    private void ClimbLadders()
    {
        isTouchingLadder = col.IsTouchingLayers(climbingLayer);

        if (!isTouchingLadder)
        {
            rb.gravityScale = gravityScaleAtStart;
            anim.SetBool(Constants.PlayerAnimations.IS_CLIMBING, false);
            return;
        }

        rb.gravityScale = 0f;

        rb.linearVelocity = new Vector2(rb.linearVelocityX, moveInput.y * climbSpeed);

        isMovingVertically = Mathf.Abs(rb.linearVelocityY) > Mathf.Epsilon;
        anim.SetBool(Constants.PlayerAnimations.IS_CLIMBING, isMovingVertically);
    }

    private void OnJump()
    {
        if (!IsGrounded()) return;

        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
