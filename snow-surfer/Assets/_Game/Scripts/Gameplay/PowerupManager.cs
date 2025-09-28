using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private const string PLAYER_LAYER_NAME = "Player";

    [Header("Powerup Config")]
    [SerializeField] private PowerupSO powerup;

    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SpriteRenderer powerupSprite;

    private float timeLeft;

    private void Start()
    {
        timeLeft = powerup.Duration;
    }

    private void Update()
    {
        CountdownTimer();
    }

    private void CountdownTimer()
    {
        if (powerupSprite.enabled == false)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    playerController.DeactivatePowerup(powerup);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer(PLAYER_LAYER_NAME);
        if (collision.gameObject.layer == layerIndex && powerupSprite.enabled)
        {
            playerController.ApplyPowerup(powerup);
            powerupSprite.enabled = false;
        }
    }
}
