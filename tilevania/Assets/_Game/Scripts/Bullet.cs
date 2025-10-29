using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 10f;
    [Header("References")]
    [SerializeField] private GameObject bulletVisual;

    private int direction;
    private Rigidbody2D rb;
    private SpriteRenderer rend;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = bulletVisual.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed * Vector2.right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.LayerIndices.EnemyIndex)
        {
            Destroy(collision.gameObject);
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

    public void SetDirection(int dir)
    {
        direction = dir;
        rend.flipX = direction < 0;
    }
}
