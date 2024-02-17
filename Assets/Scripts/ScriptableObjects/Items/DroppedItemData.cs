using UnityEngine;

[CreateAssetMenu(fileName ="Item Data",menuName = "Scriptable Objects/Item Data/Dropped Item Data")]
public class DroppedItemData : ItemData
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private float explosionForce;
    [SerializeField]
    private ItemType itemRewardType;


    public float Duration => duration;
    public float ExplosionForce => explosionForce;
    public ItemType ItemRewardType => itemRewardType;
}
