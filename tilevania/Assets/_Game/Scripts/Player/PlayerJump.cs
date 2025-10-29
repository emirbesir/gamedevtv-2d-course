using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;

    // Events
    public event Action OnPlayerJumped;

    // References
    private Rigidbody2D rb;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void OnJump()
    {
        if (!IsGrounded()) return;
        if (!playerHealth.IsAlive) return;

        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        OnPlayerJumped?.Invoke();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Constants.LayerMasks.Ground);
    }
}
