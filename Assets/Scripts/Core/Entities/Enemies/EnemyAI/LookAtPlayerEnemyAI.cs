using DG.Tweening;

public class LookAtPlayerEnemyAI : EnemyMovementAI
{
    protected override void MovementState()
    {
        base.MovementState();
        animationController.Animate(idleAnimationInfo);
        transform.DOLookAt(GameManager.Instance.CurrentPlayer.transform.position, 0.25f, AxisConstraint.Y);
    }
}
