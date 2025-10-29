using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float lifetime = 10f;
    [Header("References")]
    [SerializeField] private GameObject projectileVisual;

    // State
    private int projectileDirection;

    // References
    private Rigidbody2D rb;
    private SpriteRenderer rend;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = projectileVisual.GetComponent<SpriteRenderer>();
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.LayerIndices.EnemyIndex)
        {
            if (collision.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Constants.LayerIndices.GroundIndex)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        rb.linearVelocity = projectileDirection * projectileSpeed * Vector2.right;
    }

    public void SetDirection(int dir)
    {
        projectileDirection = dir;
        rend.flipX = projectileDirection < 0;
    }
}
