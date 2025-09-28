using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class CrashDetector : MonoBehaviour
{
    private const string FLOOR_LAYER_NAME = "Floor";

    [Header("Crash Particles")]
    [SerializeField] private ParticleSystem crashParticles;
    [Header("Crash Settings")]
    [SerializeField] private float restartDelay = 1f;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    
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
        playerController.DisableControls();
        crashParticles.Play();

        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(0);
    }
}
