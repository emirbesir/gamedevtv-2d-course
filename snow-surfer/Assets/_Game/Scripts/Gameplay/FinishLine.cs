using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    private const string PLAYER_LAYER_NAME = "Player";
    private const int LEVEL_FINISH_SCORE = 500;

    [Header("Finish Particles")]
    [SerializeField] private ParticleSystem finishParticles;

    [Header("Finish Settings")]
    [SerializeField] private float restartDelay;

    [Header("References")]
    [SerializeField] private ScoreManager scoreManager;

    private Coroutine levelFinishCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer(PLAYER_LAYER_NAME);

        if (collision.gameObject.layer == layerIndex && levelFinishCoroutine == null)
        {
            levelFinishCoroutine = StartCoroutine(LevelFinish());
        }
    }

    private IEnumerator LevelFinish()
    {
        scoreManager.AddScore(LEVEL_FINISH_SCORE);
        finishParticles.Play();

        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(0);
    }
}
