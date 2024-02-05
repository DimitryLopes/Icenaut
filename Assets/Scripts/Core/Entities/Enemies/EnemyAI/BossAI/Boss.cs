using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float attackCooldown = 5f;
    [SerializeField]
    private float shootDistance;
    [SerializeField]
    private BaseGun weapon;
    [SerializeField]
    private List<Transform> moveSpots;

    private BossAction currentAction;
    private BossAction lastAction;

    private bool canAttack = true;
    public BaseGun Weapon => weapon;
    public float ShootDistance => shootDistance;

    public AnimationController AnimationController => animationController;
    public float MoveSpeed => moveSpeed;

    private int currentPositionIndex;

    public override void Update()
    {
        if (isAlive)
        {
            // Check and perform actions based on conditions
            CheckAndPerformActions();
        }
    }

    private void CheckAndPerformActions()
    {
        if (currentAction == null || currentAction.IsFinished())
        {
            // Try to find a new action if the current one is finished or null
            lastAction = currentAction;
            currentAction = FindValidAction();
            // Perform the current action
            if (currentAction != null)
            {
                currentAction.ExecuteAction();
            }
        }
    }

    private BossAction FindValidAction()
    {
        bool shouldAttack = false;
        if (lastAction != null)
        {
            shouldAttack = lastAction is MoveAction ? true : false;
        }

        // Check conditions and return the corresponding action
        if (shouldAttack == false)
        {
            return new MoveAction(this, moveSpots[currentPositionIndex]);
        }
        else if(canAttack)
        {
            StartAttackCooldown();
            return new AttackAction(this, GameManager.Instance.CurrentPlayer.transform.position);
        }

        return null; // If no valid action is found
    }

    public void NextIndex()
    {
        currentPositionIndex++;
        if (currentPositionIndex == moveSpots.Count)
        {
            currentPositionIndex = 0;
        }
    }

    public void StartAttackCooldown()
    {
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        float timer = 0;
        while (timer < attackCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        canAttack = true;
    }
}
