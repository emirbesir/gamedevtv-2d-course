using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoinPickup : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip coinPickupSFX;
    [Header("References")]
    [SerializeField] private GameObject coinVisual;

    // State
    private bool hasBeenCollected;

    // References
    private AudioSource audioSource;
    private SpriteRenderer rend;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rend = coinVisual.GetComponent<SpriteRenderer>();

        hasBeenCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.LayerIndices.PlayerIndex && !hasBeenCollected)
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        hasBeenCollected = true;
        rend.enabled = false;
        audioSource.PlayOneShot(coinPickupSFX);
        Destroy(gameObject, coinPickupSFX.length);
    }
}
