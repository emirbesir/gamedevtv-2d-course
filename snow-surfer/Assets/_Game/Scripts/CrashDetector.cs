using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CrashDetector : MonoBehaviour
{
    private const string FLOOR_LAYER_NAME = "Floor";

    [Header("Crash Particles")]
    [SerializeField] private ParticleSystem crashParticles;
    [Header("Crash Settings")]
    [SerializeField] private float restartDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer(FLOOR_LAYER_NAME);

        if (collision.gameObject.layer == layerIndex)
        {
            StartCoroutine(RestartLevel());
        }
    }

    private IEnumerator RestartLevel()
    {   
        crashParticles.Play();
        
        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(0);
    }
}
