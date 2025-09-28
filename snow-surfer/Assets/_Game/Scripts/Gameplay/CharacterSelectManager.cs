using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private GameObject dinoSprite;
    [SerializeField] private GameObject frogSprite;

    [Header("UI Elements")]
    [SerializeField] private GameObject scoreScreen;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void BeginGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        scoreScreen.SetActive(true);
    }

    public void ChooseDino()
    {
        dinoSprite.SetActive(true);
        BeginGame();
    }

    public void ChooseFrog()
    {
        frogSprite.SetActive(true);
        BeginGame();
    }
}
