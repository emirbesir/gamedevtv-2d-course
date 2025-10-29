using System;
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
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [Header("Death Settings")]
    [SerializeField] private Vector2 deathKick;
    [SerializeField] private Color deathColor;
    [Header("References")]
    [SerializeField] private GameObject playerVisual;

    private Vector2 moveInput;
    private bool isMovingHorizontally;
    private bool isMovingVertically;
    private bool isTouchingLadder;
    private float gravityScaleAtStart;
    private bool isAlive;

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
        isAlive = true;
    }

    private void FixedUpdate()
    {
        Walk();
        FlipSprite();
        ClimbLadders();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive) return;
        if (collision.gameObject.layer == Constants.LayerIndices.EnemyIndex 
            || collision.gameObject.layer == Constants.LayerIndices.HazardsIndex)
        {
            Die();
        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Walk()
    {
        if (!isAlive) return;

        rb.linearVelocity = new Vector2(moveInput.x * walkSpeed, rb.linearVelocityY);

        isMovingHorizontally = Mathf.Abs(rb.linearVelocityX) > Mathf.Epsilon;
        anim.SetBool(Constants.PlayerAnimations.IsWalking, isMovingHorizontally);
    }
    
    private void FlipSprite()
    {
        if (!isAlive) return;

        if (isMovingHorizontally) rend.flipX = moveInput.x < 0f;
    }

    private void ClimbLadders()
    {
        if (!isAlive) return;

        isTouchingLadder = col.IsTouchingLayers(Constants.LayerMasks.Climbing);

        if (!isTouchingLadder)
        {
            rb.gravityScale = gravityScaleAtStart;
            anim.SetBool(Constants.PlayerAnimations.IsClimbing, false);
            return;
        }

        rb.gravityScale = 0f;

        rb.linearVelocity = new Vector2(rb.linearVelocityX, moveInput.y * climbSpeed);

        isMovingVertically = Mathf.Abs(rb.linearVelocityY) > Mathf.Epsilon;
        anim.SetBool(Constants.PlayerAnimations.IsClimbing, isMovingVertically);
    }

    private void OnJump()
    {
        if (!IsGrounded()) return;
        if (!isAlive) return;

        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Constants.LayerMasks.Ground);
    }

    private void Die()
    {
        isAlive = false;
        anim.SetTrigger(Constants.PlayerAnimations.Dying);
        rb.linearVelocity = deathKick;
        rend.color = deathColor;
        Time.timeScale = 0.5f;
    }
}
