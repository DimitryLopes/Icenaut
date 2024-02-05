using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveBetweenPointsEnemyAI : EnemyMovementAI
{
    [SerializeField]
    private float maxMovementCooldown;
    [SerializeField]
    private List<Transform> positions;

    private int currentPositionIndex;
    private float movementCooldown;

    protected override void MovementState()
    {
        if (!DOTween.IsTweening(transform) && movementCooldown >= maxMovementCooldown)
        {
            animationController.Animate(movingAnimationInfo);
            animationController.Animate(idleAnimationInfo, false);
            transform.DOLookAt(positions[currentPositionIndex].position, 1, AxisConstraint.Y);
            transform.DOMove(positions[currentPositionIndex].position, movementSpeed).OnComplete(NextIndex);
        }
        else
        {
            animationController.Animate(idleAnimationInfo);
            animationController.Animate(movingAnimationInfo, false);
            movementCooldown += Time.deltaTime;
        }
    }

    private void NextIndex()
    {
        currentPositionIndex++;
        movementCooldown = 0;
        if (currentPositionIndex == positions.Count)
        {
            currentPositionIndex = 0;
        }
    }


}
