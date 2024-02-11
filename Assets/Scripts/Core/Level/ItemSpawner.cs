using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private ItemType type;
    
    public void Load()
    {
        Item item = ItemManager.Instance.GetAvailableItem(type);
        item.transform.position = transform.position;
    }
}
