using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BossAction
{
    [SerializeField]
    private AnimationInfo attackAnimationInfo;

    private Vector3 target;
    

    public AttackAction(Boss boss, Vector3 target) : base(boss)
    {
        isActionFinished = false;
        this.target = target;
    }

    public override void ExecuteAction()
    {
        float distance = Vector3.Distance(target, boss.transform.position);
        if (distance <= boss.ShootDistance)
        {
            boss.Weapon.Shoot(target - boss.transform.position);
            boss.AnimationController.Animate("Attack", AnimationController.AnimationType.Trigger);
            OnActionFinished();
        }
        isActionFinished = true;
    }

    public override bool IsFinished()
    {
        return isActionFinished;
    }

    public override void OnActionFinished()
    {
        isActionFinished = true;
    }
}
