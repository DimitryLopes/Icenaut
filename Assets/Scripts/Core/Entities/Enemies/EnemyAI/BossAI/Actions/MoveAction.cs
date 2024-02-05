using UnityEngine;
using DG.Tweening;

public class MoveAction : BossAction
{
    [SerializeField]
    private float maxMovementCooldown;

    private Vector3 targetPosition;

    [SerializeField, Header("animation")]
    protected AnimationInfo idleAnimationInfo;
    [SerializeField]
    protected AnimationInfo movingAnimationInfo;

   

    public MoveAction(Boss boss, Transform targetPosition) : base(boss)
    {
        isActionFinished = false;
        this.targetPosition = targetPosition.position;
    }

    public override void ExecuteAction()
    {
        boss.AnimationController.Animate("Walking", AnimationController.AnimationType.Bool);
        boss.AnimationController.Animate("Idle", AnimationController.AnimationType.Bool, false);
        boss.transform.DOLookAt(targetPosition, 1, AxisConstraint.Y);
        boss.transform.DOMove(targetPosition, boss.MoveSpeed).OnComplete(OnActionFinished);
    }

    public override void OnActionFinished()
    {
        isActionFinished = true;
        boss.NextIndex();
    }

    public override bool IsFinished()
    {
        return isActionFinished;
    }
}
