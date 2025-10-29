using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerClimb : MonoBehaviour
{
    [Header("Climb Settings")]
    [SerializeField] private float climbSpeed = 3f;

    // State
    private float moveInputY;
    private bool isClimbing;
    public bool IsClimbing => isClimbing;

    // References
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        if (!CanClimb())
        {
            isClimbing = false;
            rb.gravityScale = Constants.DefaultPlayerGravityScale;
            return;
        }
        
        ClimbLadders();
    }

    private void OnMove(InputValue value)
    {
        moveInputY = value.Get<Vector2>().y;
    }

    private bool CanClimb()
    {
        if (!playerHealth.IsAlive) return false;
        if (!col.IsTouchingLayers(Constants.LayerMasks.Climbing)) return false;

        return true;
    }

    private void ClimbLadders()
    {
        isClimbing = Mathf.Abs(moveInputY) > Mathf.Epsilon;

        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, moveInputY * climbSpeed);
        }
        else
        {
            rb.gravityScale = Constants.DefaultPlayerGravityScale;
        }
    }
}
