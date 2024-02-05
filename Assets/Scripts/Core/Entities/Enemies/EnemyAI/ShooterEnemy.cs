using UnityEngine;

public class ShooterEnemy : EnemyBase
{
    [SerializeField]
    private BaseGun weapon;
    [SerializeField]
    private float shootDistance;
    [SerializeField]
    private AnimationInfo attackAnimationInfo;

    private GameManager gameManager;

    public override void Start()
    {
        base.Start();
        gameManager = GameManager.Instance;
    }

    public override void Die()
    {
        base.Die();
    }

    public override void Update()
    {
        base.Update();
        if (isAlive)
        {
            float distance = Vector3.Distance(gameManager.CurrentPlayer.transform.position, transform.position);
            if (distance <= shootDistance)
            {
                weapon.Shoot(gameManager.CurrentPlayer.transform.position - transform.position);
                animationController.Animate(attackAnimationInfo);
            }
        }
    }
}
