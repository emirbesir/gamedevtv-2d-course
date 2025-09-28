using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    private const string PLAYER_LAYER_NAME = "Player";

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
        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(0);
    }
}
