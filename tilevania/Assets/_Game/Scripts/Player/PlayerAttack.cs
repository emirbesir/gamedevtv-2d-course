using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpawnOffset = 0.5f;
    [SerializeField] private float attackCooldown = 0.5f;
    [Header("References")]
    [SerializeField] private GameObject playerVisual;

    // Events
    public event Action OnPlayerAttacked;

    // State
    private float lastAttackTime;

    // References
    private SpriteRenderer rend;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        lastAttackTime = -attackCooldown;

        rend = playerVisual.GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnAttack()
    {
        if (!CanAttack()) return;

        lastAttackTime = Time.time;
        InstantiateProjectile();

        OnPlayerAttacked?.Invoke();
    }

    private bool CanAttack()
    {
        if (!playerHealth.IsAlive) return false;
        if (Time.time < lastAttackTime + attackCooldown) return false;

        return true;
    }

    private void InstantiateProjectile()
    {
        int facingDirection = rend.flipX ? -1 : 1;
        Vector3 spawnPosition = transform.position + new Vector3(projectileSpawnOffset * facingDirection, 0f, 0f);
        GameObject projectileObject = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        if (projectileObject.TryGetComponent<Projectile>(out var projectile))
        {
            projectile.SetDirection(facingDirection);
        }
    }
}
