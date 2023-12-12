using UnityEngine;

public class Player : MonoBehaviour, IStateListener<OnGoingGameState>
{
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float shootingDelay = 5f;
    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private int groundLayerID;
    [SerializeField]
    private float groundCheckDistance = 0.2f;
    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField, Header("Animation")]
    private AnimationInfo runAnimationInfo;
    [SerializeField]
    private AnimationInfo idleAnimationInfo;
    [SerializeField]
    private AnimationInfo jumpAnimationInfo;
    [SerializeField]
    private AnimationInfo landedAnimationInfo;

    private AnimationManager animationManager;
    private bool isGrounded;

    public bool CanMove { get; private set; }
    public bool CanJump { get; private set; }
    public bool CanShoot { get; private set; }
    public bool CanAct { get; private set; }

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
