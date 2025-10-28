using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 6f;
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;
    [Header("References")]
    [SerializeField] private GameObject playerVisual;

    private Vector2 moveInput;
    private bool isMovingHorizontally;

    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = playerVisual.GetComponent<SpriteRenderer>();
        anim = playerVisual.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);
        isMovingHorizontally = Mathf.Abs(rb.linearVelocityX) > Mathf.Epsilon;

        if (isMovingHorizontally) rend.flipX = moveInput.x < 0f;

        anim.SetBool(Constants.PlayerAnimations.IS_WALKING, isMovingHorizontally);
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
