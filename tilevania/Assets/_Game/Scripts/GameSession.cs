using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [Header("Session Settings")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private float respawnDelay = 2f;
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;

    private int currentLives;

    public static GameSession Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentLives = startingLives;
    }

    private void Start()
    {
        FindAndSubscribeToPlayer();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        UnsubscribeFromPlayer();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndSubscribeToPlayer();
    }

    private void FindAndSubscribeToPlayer()
    {
        UnsubscribeFromPlayer();
        
        if (playerHealth == null)
        {
            playerHealth = FindFirstObjectByType<PlayerHealth>();
        }
        
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDied += HandlePlayerDeath;
        }
    }

    private void UnsubscribeFromPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDied -= HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath()
    {
        Time.timeScale = 0.5f;
        
        if (currentLives > 1)
        {
            StartCoroutine(ResetLevel(currentLives - 1, SceneManager.GetActiveScene().buildIndex));
        }
        else
        {
            StartCoroutine(ResetLevel(startingLives, 0));
        }
    }

    private IEnumerator ResetLevel(int lives, int sceneIndex)
    {
        yield return new WaitForSeconds(respawnDelay);
        currentLives = lives;
        SceneManager.LoadScene(sceneIndex);
        
        yield return null;
        FindAndSubscribeToPlayer();
        
        if (playerHealth != null)
        {
            playerHealth.Revive();
        }
        
        Time.timeScale = 1f;
    }
}
