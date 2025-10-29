using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [Header("References")]
    [SerializeField] private GameObject enemyVisual;

    // State
    private int moveDirection = 1;
    
    // References
    private Rigidbody2D rb;
    private SpriteRenderer rend;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = enemyVisual.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckForWalls();
        Move();
    }

    private void CheckForWalls()
    {
        bool hitWall = Physics2D.Raycast(transform.position, Vector2.right * moveDirection, Constants.Enemy.RayLength, Constants.LayerMasks.Ground);

        if (hitWall)
        {
            ChangeDirection();
        }
    }

    private void Move()
    {
        rb.linearVelocity = moveDirection * moveSpeed * Vector2.right;
    }

    private void ChangeDirection()
    {
        moveDirection *= -1;
        rend.flipX = moveDirection < 0;
    }
}
