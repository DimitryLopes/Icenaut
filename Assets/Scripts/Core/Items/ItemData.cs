using UnityEngine;

[CreateAssetMenu(fileName = "ItemData",menuName ="Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private Item itemPrefab;

    public ItemType Type => itemType;
    public Item Prefab => itemPrefab;
    public Sprite Icon => itemSprite;
}

public enum ItemType
{
    Coin,
    HealthPack,
}
