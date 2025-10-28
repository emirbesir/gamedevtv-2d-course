using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed = 4f;
    [Header("References")]
    [SerializeField] private GameObject enemyVisual;

    private int moveDirection = 1;
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
        bool hitWall = Physics2D.Raycast(transform.position, Vector2.right * moveDirection, Constants.Enemy.RayLength, Constants.Layers.Ground);

        if (hitWall)
        {
            moveDirection *= -1;
            rend.flipX = moveDirection < 0;
        }
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed * moveDirection, 0f);
    }
}
