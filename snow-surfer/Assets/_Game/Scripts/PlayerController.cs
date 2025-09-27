using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torquePower = 1f;
    private InputAction moveAction;
    private Rigidbody2D rb;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        rb.AddTorque(-moveInput.x * torquePower);
    }
}
