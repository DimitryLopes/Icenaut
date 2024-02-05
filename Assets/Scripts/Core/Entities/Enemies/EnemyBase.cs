using System.Collections;
using UnityEngine;

public class EnemyBase : LivingEntity
{
    [SerializeField]
    protected AnimationController animationController;
    [SerializeField]
    private AnimationInfo deathAnimationInfo;
    [SerializeField]
    private EnemyMovementAI movementAI;

    public override void Die()
    {
        EventManager.OnEnemyDeath.Invoke(this);
        animationController.Animate(deathAnimationInfo);
        animationController.Animate("Idle", AnimationController.AnimationType.Bool, false);
        isAlive = false;
    }

    public virtual void Update()
    {
        if (isAlive)
        {
            movementAI.HandleMovement();
        }
    }
}