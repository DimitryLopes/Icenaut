using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : LivingEntity
{
    [SerializeField]
    private int rewardAmount;
    [SerializeField]
    private ItemData itemRewardData;
    [SerializeField]
    private Transform dropPoint;

    public override void Die()
    {
        GiveItemReward();
        gameObject.SetActive(false);
    }

    private void GiveItemReward()
    {
        for(int i = 0; i < rewardAmount; i++)
        {
            DroppedItem item = ItemManager.Instance.GetAvailableDroppedItem();
            item.Drop(dropPoint.position);
        }
    }
}
