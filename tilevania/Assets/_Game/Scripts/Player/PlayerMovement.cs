using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Walk Settings")]
    [SerializeField] private float walkSpeed = 6f;
    [Header("References")]
    [SerializeField] private GameObject playerVisual;

    // State
    private Vector2 moveInput;
    private bool isMovingHorizontally;
    public bool IsMovingHorizontally => isMovingHorizontally;

    // References
    private Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private SpriteRenderer rend;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();

        rend = playerVisual.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Walk();
        FlipSprite();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Walk()
    {
        if (!playerHealth.IsAlive) return;

        rb.linearVelocity = new Vector2(moveInput.x * walkSpeed, rb.linearVelocityY);

        isMovingHorizontally = Mathf.Abs(rb.linearVelocityX) > Mathf.Epsilon;
    }
    
    private void FlipSprite()
    {
        if (!playerHealth.IsAlive) return;

        if (isMovingHorizontally) rend.flipX = moveInput.x < 0f;
    }
}
