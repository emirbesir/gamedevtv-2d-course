using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LivesTextUI : MonoBehaviour
{
    // References
    private TMP_Text livesText;

    private void Awake()
    {
        livesText = GetComponent<TMP_Text>();
    }
    
    private void Start()
    {
        UpdateLivesText(GameSession.Instance.CurrentLives);
        GameSession.Instance.OnLivesChanged += UpdateLivesText;
    }

    private void OnDisable()
    {
        GameSession.Instance.OnLivesChanged -= UpdateLivesText;
    }

    private void UpdateLivesText(int lives)
    {
        livesText.text = $"Lives: {lives}";
    }
}
