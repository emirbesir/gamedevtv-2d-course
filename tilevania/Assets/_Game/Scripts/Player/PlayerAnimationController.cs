using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(PlayerClimb))]
public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerVisual;

    // References
    private Animator anim;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerJump playerJump;
    private PlayerClimb playerClimb;

    private void Awake()
    {
        anim = playerVisual.GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerJump = GetComponent<PlayerJump>();
        playerClimb = GetComponent<PlayerClimb>();
    }

    private void OnEnable()
    {
        playerHealth.OnPlayerDied += HandleDeathAnimation;
        playerAttack.OnPlayerAttacked += HandleAttackAnimation;
        playerJump.OnPlayerJumped += HandleJumpAnimation;
    }

    private void OnDisable()
    {
        playerHealth.OnPlayerDied -= HandleDeathAnimation;
        playerAttack.OnPlayerAttacked -= HandleAttackAnimation;
        playerJump.OnPlayerJumped -= HandleJumpAnimation;
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        anim.SetBool(Constants.PlayerAnimations.IsWalking, playerMovement.IsMovingHorizontally);
        anim.SetBool(Constants.PlayerAnimations.IsClimbing, playerClimb.IsClimbing);
    }

    private void HandleDeathAnimation()
    {
        anim.SetTrigger(Constants.PlayerAnimations.Dying);
    }

    private void HandleAttackAnimation()
    {
        anim.SetTrigger(Constants.PlayerAnimations.Attacking);
    }

    private void HandleJumpAnimation()
    {
        anim.SetTrigger(Constants.PlayerAnimations.Jumping);
    }
}
