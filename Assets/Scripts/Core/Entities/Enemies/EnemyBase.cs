using UnityEngine;

public class EnemyBase : LivingEntity
{
    [SerializeField]
    protected AnimationController animationController;
    [SerializeField]
    private AnimationInfo deathAnimationInfo;
    [SerializeField]
    private EnemyMovementAI movementAI;
    [SerializeField]
    private float contactDamage;

    public override void Die()
    {
        EventManager.OnEnemyDeath.Invoke(this);
        animationController.Animate(deathAnimationInfo);
        animationController.Animate("Idle", AnimationController.AnimationType.Bool, false);
        isAlive = false;
    }

    public override void OnDamageTaken(float damage)
    {
        base.OnDamageTaken(damage);
    }

    public virtual void Update()
    {
        if (isAlive)
        {
            movementAI.HandleMovement();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.OnDamageTaken(contactDamage);
        }
    }
}