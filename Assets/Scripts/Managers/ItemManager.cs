using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private Transform itemsContainer;
    [SerializeField]
    private List<ItemData> itemDataBase;

    public static ItemManager Instance;

    public Dictionary<ItemType, ItemSavedData> ItemSavedData = new Dictionary<ItemType, ItemSavedData>();
    public Dictionary<ItemType, List<Item>> InstantiatedItems = new Dictionary<ItemType, List<Item>>();
    [HideInInspector]
    public List<ItemData> ItemDataBase => itemDataBase;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        SetUpDictionaries();
    }

    private void Start()
    {
        EventManager.OnItemActivated.AddListener(OnItemActivated);
        EventManager.OnItemDeactivated.AddListener(OnItemDeactivated);
    }


    private void OnItemActivated(Item item)
    {
        List<Item> itemList = InstantiatedItems[item.Data.Type];
        if (!itemList.Contains(item))
        {
            itemList.Add(item);
        }
        else
        {
            Debug.Log(item.name + " Was in the instantiated list already. How did it get in here??");
        }
    }

    private void OnItemDeactivated(Item item)
    {
        List<Item> itemList = InstantiatedItems[item.Data.Type];
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
        }
        else
        {
            Debug.Log(item.name + " Was not in the instantiated list. How did it get in the game??");
        }
    }

    private void SetUpDictionaries()
    {
        foreach(ItemData data in itemDataBase)
        {
            InstantiatedItems[data.Type] = new List<Item>();
            ItemSavedData[data.Type] = new ItemSavedData(data.Type);
        }
    }

    #region Getters
    public int GetItemAmount(ItemType type)
    {
        int amount = ItemSavedData[type].ItemAmount;
        return amount;
    }

    private Item GetItemPrefab(ItemType type)
    {
        foreach (ItemData data in itemDataBase)
        {
            if (data.Type == type)
            {
                return data.Prefab;
            }
        }
        return null;
    }

    public Item GetAvailableItem(ItemType type)
    {
        List<Item> itemList = InstantiatedItems[type];
        foreach(Item item in itemList)
        {
            if (!item.IsActive)
            {
                item.Activate();
                return item;
            }
        }

        Item newItem = Instantiate(GetItemPrefab(type), itemsContainer);
        newItem.Activate();

        return newItem;
    }
    #endregion

    public void ResetItemSavedData()
    {
        foreach(ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            ItemSavedData[type].ClearValues();
        }
    }

    public void ChangeItemAmount(ItemType type, int amount)
    {
        ItemSavedData[type].ChangeItemAmount(amount);
        int newAmount = ItemSavedData[type].ItemAmount;

        EventManager.OnItemAmountChanged.Invoke(type, newAmount);
    }
}

public class ItemSavedData
{
    public ItemType Type { get; private set; }
    public int ItemAmount { get; private set; }

    public ItemSavedData (ItemType type)
    {
        Type = type;
        ItemAmount = 0;
    }

    public void ClearValues()
    {
        ItemAmount = 0;
    }

    public void ChangeItemAmount(int amount)
    {
        ItemAmount += amount;
    }
}

