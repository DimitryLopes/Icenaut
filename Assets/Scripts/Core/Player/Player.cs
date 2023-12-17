using UnityEngine;

public class Player : MonoBehaviour, IStateListener<OnGoingGameState>
{
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float sprintingSpeedMultiplier = 1.3f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float maxStamina = 5;
    [SerializeField]
    private float staminaConsumptionRate = 1f;
    [SerializeField]
    private float staminaRegenerationRate = 0.8f;
    [SerializeField]
    private float shootingDelay = 5f;

    [SerializeField]
    private int groundLayerID;
    [SerializeField]
    private float groundCheckDistance = 0.2f;
    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField, Header("Animation")]
    private AnimationInfo runAnimationInfo;
    [SerializeField]
    private AnimationInfo sprintAnimationInfo;
    [SerializeField]
    private AnimationInfo idleAnimationInfo;
    [SerializeField]
    private AnimationInfo jumpAnimationInfo;
    [SerializeField]
    private AnimationInfo landedAnimationInfo;

    private AnimationManager animationManager;
    private float currentStamina = 5;
    private bool isGrounded;

    public bool CanMove { get; private set; }
    public bool CanJump { get; private set; }
    public bool CanShoot { get; private set; }
    public bool CanAct { get; private set; }

    public float CurrentStamina => currentStamina;
    public float MaxStamina => maxStamina;

    private void Start()
    {
        animationManager = AnimationManager.Instance;
    }

    #region Gameplay
    private void Update()
    {
        if (CanAct)
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, whatIsGround);
            animationManager.PlayerAnimationController.Animate(landedAnimationInfo, isGrounded);
            if (CanMove)
            {
                HandleMovement();
            }
            if (CanJump)
            {
                HandleJump();
            }
            if (CanShoot)
            {
                HandleShooting();
            }
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput) * movementSpeed;
        moveDirection = HandleSprinting(moveDirection);
        if (horizontalInput != 0 || verticalInput != 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            animationManager.PlayerAnimationController.Animate(runAnimationInfo);
            animationManager.PlayerAnimationController.Animate(idleAnimationInfo, false);
        }
        else
        {
            animationManager.PlayerAnimationController.Animate(runAnimationInfo, false);
            animationManager.PlayerAnimationController.Animate(idleAnimationInfo);
        }

        moveDirection.y = rigidbody.velocity.y;
        rigidbody.velocity = moveDirection;
    }

    private Vector3 HandleSprinting(Vector3 movementDirection)
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            movementDirection *= sprintingSpeedMultiplier;

            animationManager.PlayerAnimationController.Animate(sprintAnimationInfo);
            currentStamina -= Time.deltaTime * staminaConsumptionRate;
        }
        else
        {
            animationManager.PlayerAnimationController.Animate(sprintAnimationInfo, false);
            if (currentStamina < maxStamina)
            {
                currentStamina += Time.deltaTime * staminaRegenerationRate;
            }
        }

        return movementDirection;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red, 1);
            if (isGrounded)
            {
                animationManager.PlayerAnimationController.Animate(jumpAnimationInfo);
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void HandleShooting()
    {

    }

    #endregion

    #region State
    public void RegisterState<T>() where T : OnGoingGameState
    {
        Debug.Log("Eu não sei o que fazer apartir daqui, mas refleti");
    }

    public void EnableActing()
    {
        ToggleActing(true, true);
    }

    public void DisableActing()
    {
        ToggleActing(false, true);
    }


    #endregion

    #region Actions Toggle
    public void ToggleActing(bool value, bool toggleEverything = false)
    {
        CanAct = value;
        if (toggleEverything)
        {
            ToggleMoving(value);
            ToggleJumpping(value);
            ToggleShooting(value);
        }
    }
    public void ToggleMoving(bool value)
    {
        CanMove = value;
    }
    public void ToggleJumpping(bool value)
    {
        CanJump = value;
    }
    public void ToggleShooting(bool value)
    {
        CanShoot = value;
    }
    #endregion
}
