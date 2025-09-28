using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private const int FLIP_SCORE = 100;

    [Header("Movement Settings")]
    [SerializeField] private float torquePower;

    [Header("Boost Settings")]
    [SerializeField] private SurfaceEffector2D surfaceEffector;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float boostedMoveSpeed;

    [Header("References")]
    [SerializeField] private ScoreManager scoreManager;

    private InputAction moveAction;
    private Rigidbody2D rb;
    private bool CanControlPlayer;
    private float previousRotation;
    private float totalRotation;
    private int flipCount;


    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        CanControlPlayer = true;
        flipCount = 0;
    }

    private void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        if (CanControlPlayer)
        {
            MovePlayer(moveInput.x);
            BoostPlayer(moveInput.y);
            CalculateFlips();
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

    private void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);

        if (totalRotation >= 360 || totalRotation <= -360)
        {
            totalRotation = 0;
            flipCount++;
            scoreManager.AddScore(FLIP_SCORE);
        }

        previousRotation = currentRotation;
    }

    public void DisableControls()
    {
        CanControlPlayer = false;
    }
}
