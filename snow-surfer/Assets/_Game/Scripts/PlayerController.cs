using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float torquePower = 1f;

    [Header("Boost Settings")]
    [SerializeField] private SurfaceEffector2D surfaceEffector;
    [SerializeField] private float baseMoveSpeed = 18f;
    [SerializeField] private float boostedMoveSpeed = 24f;

    private bool CanControlPlayer = true;
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
        if (CanControlPlayer)
        {
            MovePlayer(moveInput.x);
            BoostPlayer(moveInput.y);
        }
    }

    private void MovePlayer(float xInput)
    {
        rb.AddTorque(-xInput * torquePower);
    }

    private void BoostPlayer(float yInput)
    {
        if (yInput > 0)
        {
            surfaceEffector.speed = boostedMoveSpeed;
        }
        else
        {
            surfaceEffector.speed = baseMoveSpeed;
        }
    }

    public void DisableControls()
    {
        CanControlPlayer = false;
    }
}
