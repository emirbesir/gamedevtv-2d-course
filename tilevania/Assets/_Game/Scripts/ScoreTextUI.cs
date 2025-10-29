using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreTextUI : MonoBehaviour
{
    // References
    private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        UpdateScoreText(GameSession.Instance.CurrentScore);
        GameSession.Instance.OnScoreChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        GameSession.Instance.OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = $"Score: {score}";
    }
}
