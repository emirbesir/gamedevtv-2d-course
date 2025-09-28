using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    private const string PLAYER_LAYER_NAME = "Player";

    [Header("Finish Particles")]
    [SerializeField] private ParticleSystem finishParticles;
    [Header("Finish Settings")]
    [SerializeField] private float restartDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer(PLAYER_LAYER_NAME);

        if (collision.gameObject.layer == layerIndex)
        {
            StartCoroutine(RestartLevel());
        }
    }

    private IEnumerator RestartLevel()
    {   
        finishParticles.Play();
        
        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(0);
    }
}
