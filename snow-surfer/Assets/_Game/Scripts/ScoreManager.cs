using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{   
    [Header("Text Reference")]
    [SerializeField] private TMP_Text scoreText;

    private int currentScore;

    private void Start()
    {
        currentScore = 0;
        UpdateScoreText();    
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {currentScore}";
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        UpdateScoreText();
    }
}
