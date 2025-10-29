using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerHealth : MonoBehaviour
{
    [Header("Death Settings")]
    [SerializeField] private Vector2 deathKick;
    [SerializeField] private Color deathColor;
    [Header("References")]
    [SerializeField] private GameObject playerVisual;

    // Events
    public event Action OnPlayerDied;

    // State
    private bool isAlive;
    public bool IsAlive => isAlive;

    // References
    private Rigidbody2D rb;
    private SpriteRenderer rend;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = playerVisual.GetComponent<SpriteRenderer>();

        isAlive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive) return;

        if (collision.gameObject.layer == Constants.LayerIndices.EnemyIndex
            || collision.gameObject.layer == Constants.LayerIndices.HazardsIndex)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        
        Time.timeScale = 0.5f;
        rb.linearVelocity = deathKick;
        rend.color = deathColor;

        OnPlayerDied?.Invoke();
    }
}
