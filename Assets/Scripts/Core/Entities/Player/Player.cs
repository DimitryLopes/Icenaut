using UnityEngine;

public class Player : LivingEntity, IStateListener<OnGoingGameState>
{
    private const string HORIZONTAL_AXIS_KEY = "Horizontal";
    private const string VERTICAL_AXIS_KEY = "Vertical";

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
    private Collider collider;


    [SerializeField, Header("ground check")]
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
    [SerializeField]
    private AnimationInfo dieAnimationInfo;

    private AnimationManager animationManager;
    private float currentStamina = 5;
    private bool isGrounded;

    private BaseGun currentWeapon;
    private BaseGun sideWeapon;

    public bool CanMove { get; private set; }
    public bool CanJump { get; private set; }
    public bool CanShoot { get; private set; }
    public bool CanAct { get; private set; }

    public float CurrentStamina => currentStamina;
    public float MaxStamina => maxStamina;

    public override void Start()
    {
        base.Start();
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
        float horizontalInput = Input.GetAxis(HORIZONTAL_AXIS_KEY);
        float verticalInput = Input.GetAxis(VERTICAL_AXIS_KEY);

        // Calculate movement direction based on camera orientation
        Vector3 cameraForward = GameManager.Instance.MainCamera.transform.forward;
        Vector3 cameraRight = GameManager.Instance.MainCamera.transform.right;

        // Ignore vertical component to avoid moving up/down
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = horizontalInput * cameraRight + cameraForward * verticalInput;
        moveDirection *= movementSpeed;
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
            UIManager.Instance.UpdatePlayerBar(UIManager.UIPlayerBarType.Stamina, currentStamina, maxStamina);
        }
        else
        {
            animationManager.PlayerAnimationController.Animate(sprintAnimationInfo, false);
            if (currentStamina < maxStamina)
            {
                currentStamina += Time.deltaTime * staminaRegenerationRate;
                UIManager.Instance.UpdatePlayerBar(UIManager.UIPlayerBarType.Stamina, currentStamina, maxStamina);
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
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Shoot(transform.forward);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }
    }
    #endregion

    #region State
    public void RegisterState<T>() where T : OnGoingGameState
    {
        Debug.Log("Eu n�o sei o que fazer apartir daqui, mas refleti");
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

    private void ChangeWeapon()
    {
        BaseGun primaryHolder = currentWeapon;
        currentWeapon = sideWeapon;
        sideWeapon = primaryHolder;
        EventManager.OnWeaponChanged.Invoke(currentWeapon);
    }

    public void SetWeapons(BaseGun primary, BaseGun secondary)
    {
        currentWeapon = secondary;
        sideWeapon = primary;

        ChangeWeapon();
    }

    public override void OnDamageTaken(float damage)
    {
        if (isAlive)
        {
            base.OnDamageTaken(damage);
            EventManager.OnPlayerHit.Invoke(this);
            UIManager.Instance.UpdatePlayerBar(UIManager.UIPlayerBarType.Health, health, maxHealth);
        }
    }

    public override void Die()
    {
        animationManager.PlayerAnimationController.Animate(dieAnimationInfo);
        DisableActing();
        EventManager.OnPlayerDeath.Invoke(this);
        animationManager.PlayerAnimationController.Animate(idleAnimationInfo, false);
        animationManager.PlayerAnimationController.Animate(runAnimationInfo, false);
        isAlive = false;
    }

    public override void Respawn()
    {
        base.Respawn();
        EnableActing();
        UIManager.Instance.UpdatePlayerBar(UIManager.UIPlayerBarType.Health, health, maxHealth);
        if (animationManager != null)
        {
            animationManager.PlayerAnimationController.Animate(idleAnimationInfo, true);
        }
    }
}