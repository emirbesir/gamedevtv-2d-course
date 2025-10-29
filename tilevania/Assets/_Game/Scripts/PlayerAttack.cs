using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackCooldown = 0.5f;
    [Header("References")]
    [SerializeField] private GameObject playerVisual;

    // State
    private float lastAttackTime;

    // References
    private SpriteRenderer rend;
    private Animator anim;

    private void Awake()
    {
        lastAttackTime = -attackCooldown;
        
        rend = playerVisual.GetComponent<SpriteRenderer>();
        anim = playerVisual.GetComponent<Animator>();
    }

    private void OnAttack()
    {
        if (!CanAttack()) return;

        lastAttackTime = Time.time;
        InstantiateProjectile();
        
        anim.SetTrigger(Constants.PlayerAnimations.Attacking);
    }

    private bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    private void InstantiateProjectile()
    {
        int facingDirection = rend.flipX ? -1 : 1;
        Vector3 spawnPosition = transform.position + new Vector3(0.5f * facingDirection, 0f, 0f);
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDirection(facingDirection);
    }
}
