using UnityEngine;

public class Delivery : MonoBehaviour
{
    private bool hasPackage = false;
    private ParticleSystem deliveryParticles;

    private void Awake()
    {
        deliveryParticles = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Pickup") && !hasPackage)
        {
            hasPackage = true;
            deliveryParticles.Play();
            Destroy(col.gameObject);
        }
        if (col.CompareTag("Customer") && hasPackage)
        {
            hasPackage = false;
            deliveryParticles.Stop();
            Destroy(col.gameObject);
        }
    }
}
