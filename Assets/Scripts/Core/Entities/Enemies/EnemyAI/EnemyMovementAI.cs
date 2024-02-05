using UnityEngine;

public class EnemyMovementAI : MonoBehaviour
{
    [SerializeField]
    protected AnimationController animationController;
    [SerializeField]
    protected float movementSpeed;

    [SerializeField, Header("animation")]
    protected AnimationInfo idleAnimationInfo;
    [SerializeField]
    protected AnimationInfo movingAnimationInfo;

    private EnemyMovementState currentState;
    public virtual void HandleMovement()
    {
        switch (currentState)
        {
            case EnemyMovementState.Moving:
                MovementState();
                break;
            case EnemyMovementState.Idle:
                IdleState();
                break;
        }
    }

    protected virtual void IdleState()
    {

    }

    protected virtual void MovementState()
    {

    }
}

public enum EnemyMovementState
{
    Moving,
    Idle,
}
