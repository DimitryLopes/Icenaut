using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiStateItemData", menuName = "Scriptable Objects/MultiStateItemData")]
public class MultiStateItemData : ItemData
{
    [SerializeField]
    public List<ItemState> States = new List<ItemState>();
}


