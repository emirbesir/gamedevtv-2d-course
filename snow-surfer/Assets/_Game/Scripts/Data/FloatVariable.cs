using UnityEngine;

[CreateAssetMenu(fileName = "New Float Variable", menuName = "ScriptableObjects/Float Variable", order = 1)]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float startingValue;
    public float Value;

    private void OnEnable()
    {
        ResetValue();
    }

    public void ResetValue()
    {
        Value = startingValue;
    }
}
