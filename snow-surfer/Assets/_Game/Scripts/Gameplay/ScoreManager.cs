using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{   
    [Header("Text Reference")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Score Data")]
    [SerializeField] private FloatVariable scoreVariable;

    [Header("Settings")]
    [SerializeField] private bool isEndScreen;

    private void Start()
    {
        if (!isEndScreen)
        {
            scoreVariable.ResetValue();
        }

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {scoreVariable.Value}";
    }

    public void AddScore(int scoreToAdd)
    {
        scoreVariable.Value += scoreToAdd;
        UpdateScoreText();
    }
}
