using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 1f;

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0f);
        
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
