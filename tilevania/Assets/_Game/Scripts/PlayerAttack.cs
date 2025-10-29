using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackCooldown = 0.5f;
    [Header("References")]
    [SerializeField] private GameObject playerVisual;

    private float lastAttackTime;
    private SpriteRenderer rend;
    private Animator anim;

    private void Awake()
    {
        rend = playerVisual.GetComponent<SpriteRenderer>();
        anim = playerVisual.GetComponent<Animator>();
        lastAttackTime = -attackCooldown;
    }

    private void OnAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        lastAttackTime = Time.time;
        
        int facingDirection = rend.flipX ? -1 : 1;
        Vector3 attackPoint = new Vector2(transform.position.x + (0.5f * facingDirection), transform.position.y);
        GameObject bullet = Instantiate(bulletPrefab, attackPoint, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(facingDirection);

        anim.SetTrigger(Constants.PlayerAnimations.Attacking);
    }
}
