using UnityEngine;

public class SnowTrail : MonoBehaviour
{   
    private const string FLOOR_LAYER_NAME = "Floor";

    [Header("Snow Particles")]
    [SerializeField] private ParticleSystem snowParticles;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer(FLOOR_LAYER_NAME);

        if (collision.gameObject.layer == layerIndex)
        {
            snowParticles.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer(FLOOR_LAYER_NAME);

        if (collision.gameObject.layer == layerIndex)
        {
            snowParticles.Stop();
        }
    }
}
