using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup", menuName = "ScriptableObjects/Powerup", order = 1)]
public class PowerupSO : ScriptableObject
{
    [Header("Powerup Settings")]
    [SerializeField] private string powerupName;
    [SerializeField] private float valueChange;
    [SerializeField] private float duration;

    public string PowerupName => powerupName;
    public float ValueChange => valueChange;
    public float Duration => duration;
}
